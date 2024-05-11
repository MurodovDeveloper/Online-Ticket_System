using Ticket.Domain.Entities.Identity;

namespace Ticket.Application.UseCases.Common.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Phone {  get; set; }

        public List<Role>? Roles {  get; set; }
    }
}
