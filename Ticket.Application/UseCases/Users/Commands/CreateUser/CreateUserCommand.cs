using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Common.Exceptions;
using Ticket.Application.Common.Interfaces;
using Ticket.Domain.Entities.Identity;

namespace Ticket.Application.UseCases.Common.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<Guid>
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid[]? RoleIds { get; set; }


    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IApplicationDbContext context, IMapper mapper)
               => (_context, _mapper) = (context, mapper);

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            if (_context.Users.Any(x => x.Username == request.Username))
                throw new AlreadyExistsException(nameof(User), request.Username);



            var roles = await _context.Roles.ToListAsync(cancellationToken);



            var userRole = new List<Role>();
            if (request?.RoleIds?.Length > 0)
                roles.ForEach(role =>
                {
                    if (request.RoleIds.Any(id => id == role.Id))
                        userRole.Add(role);
                });


            var user = _mapper.Map<User>(request);

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;


        }
    }
}
