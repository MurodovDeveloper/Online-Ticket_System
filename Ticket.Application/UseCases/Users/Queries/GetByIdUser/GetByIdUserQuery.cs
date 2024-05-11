using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Common.Exceptions;
using Ticket.Application.Common.Interfaces;
using Ticket.Domain.Entities.Identity;

namespace Ticket.Application.UseCases.Common.Queries.GetByIdUser
{
    public record GetByIdUserQuery(Guid Id) : IRequest<UserResponse>;


    public class GetByIdUserResponse : IRequestHandler<GetByIdUserQuery, UserResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdUserResponse(IApplicationDbContext context, IMapper mapper)
               => (_context, _mapper) = (context, mapper);

        public async Task<UserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var foundUser = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);
            if (foundUser is null)
                throw new NotFoundException(nameof(User), request.Id);
            var result = _mapper.Map<UserResponse>(foundUser);
            return result;
        }
    }
}
