using AutoMapper;
using Ticket.Application.Common.Interfaces;
using Ticket.Application.Common.JWT.Interfaces;
using Ticket.Application.UseCases;
using Ticket.Domain.Entities.Identity;

namespace Ticket.Application.Common.JWT.Services
{
    public class RefreshToken : IUserRefreshToken
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RefreshToken(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async ValueTask<UserRefreshToken> AddOrUpdateRefreshToken(UserRefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            var foundRefreshtoken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserName == refreshToken.UserName, cancellationToken);
            if (foundRefreshtoken is null)
            {
                await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return refreshToken;
            }
            else
            {
                foundRefreshtoken.RefreshToken = refreshToken.RefreshToken;
                foundRefreshtoken.ExpiresTime = refreshToken.ExpiresTime;
                _context.RefreshTokens.Update(foundRefreshtoken);
                await _context.SaveChangesAsync(cancellationToken);
                return refreshToken;
            }
        }

        public async ValueTask<UserResponse> AuthenAsync(LoginUserCommand user)
        {
            string hashPassword = user.Password.GetHashedString();
            User? founUser = await _context.Users.SingleOrDefaultAsync(x => x.Username == user.Username && x.Password == hashPassword);
            if (founUser is null)
            {
                return null;
            }

            var userResponse = _mapper.Map<UserResponse>(founUser);

            //var userResponse = new UserResponse
            //{
            //    Id = founUser.Id,
            //    FullName = founUser.FullName,
            //    Phone = founUser.Phone,
            //    Username = founUser.Username,
            //    Roles = founUser.Roles,

            //};

            return userResponse;
        }

        public async ValueTask<bool> DeleteUserRefreshTokens(string username, string refreshToken, CancellationToken cancellationToken = default)
        {
            var foundRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserName == username && x.RefreshToken == refreshToken);
            _context.RefreshTokens.Remove(foundRefreshToken);
            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }

        public async ValueTask<UserRefreshToken> GetSavedRefreshTokens(string username, string refreshtoken)
        {
            var foundRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserName == username && x.RefreshToken == refreshtoken);
            return foundRefreshToken;
        }
    }
}
