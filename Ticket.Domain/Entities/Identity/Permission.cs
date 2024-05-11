using System.Text.Json.Serialization;
using Ticket.Domain.Commons;

namespace Ticket.Domain.Entities.Identity
{

    public class Permission : BaseAuditableEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Role>? Roles { get; set; }
    }
}