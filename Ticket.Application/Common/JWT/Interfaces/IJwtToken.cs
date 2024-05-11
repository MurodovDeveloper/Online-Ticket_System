using System.Security.Claims;
using Ticket.Application.Common.JWT.Models;
using Ticket.Domain.Entities.Identity;

namespace Ticket.Application.Common.JWT.Interfaces
{
    public interface IJwtToken
    {
        ValueTask<TokenResponse> CreateTokenAsync(string userName, string UserId, ICollection<Role> roles, CancellationToken cancellationToken = default);
        ValueTask<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
        ValueTask<string> GenerateRefreshTokenAsync(string userName);
    }
}
