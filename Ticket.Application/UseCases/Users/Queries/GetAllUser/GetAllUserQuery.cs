using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Common.Interfaces;

namespace Ticket.Application.UseCases.Common.Queries.GetAllUser
{
    public record GetAllUserQuery : IRequest<List<UserResponse>>;
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllUserQueryHandler(IApplicationDbContext context, IMapper mapper)
                => (_context, _mapper) = (context, mapper);

        public async Task<List<UserResponse>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var allUser = await _context.Users.ToListAsync(cancellationToken);
            var result = _mapper.Map<List<UserResponse>>(allUser);
            return result;
        }
    }
}
