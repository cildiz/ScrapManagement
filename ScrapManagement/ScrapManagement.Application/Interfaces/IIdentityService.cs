using AspNetCoreHero.Results;
using ScrapManagement.Application.DTOs.Identity;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress);

        Task<Result<string>> RegisterAsync(RegisterRequest request, string origin);

        Task<Result<string>> ConfirmEmailAsync(string userId, string code);

        Task<Result<string>> ResetPassword(ResetPasswordRequest model);
    }
}