using Microsoft.AspNetCore.Http;
using ScrapManagement.Application.Interfaces.Shared;
using System.Security.Claims;

namespace ScrapManagement.Web.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier) == null ? null : httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
            Username = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name) == null ? null : httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name).Value;
        }

        public string UserId { get; }
        public string Username { get; }
    }
}