using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.UseCases;
using Ticket.Domain.Entities.Identity;

namespace Ticket.Application.Common.JWT.Interfaces
{
    public interface IUserRefreshToken
    {
        ValueTask<UserRefreshToken> AddOrUpdateRefreshToken(UserRefreshToken refreshToken, CancellationToken cancellationToken = default);
        ValueTask<UserResponse> AuthenAsync(LoginUserCommand user);
        ValueTask<bool> DeleteUserRefreshTokens(string username, string refreshToken, CancellationToken cancellationToken = default);
        ValueTask<UserRefreshToken> GetSavedRefreshTokens(string username, string refreshtoken);
    }
}
