using Core.DTOs.Auth.Login;
using Core.DTOs.Auth.Register;

namespace Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthLoginResponse?> RegisterUser(AuthRegistrationRequest request);
        Task<AuthLoginResponse?> LoginUser(AuthLoginRequest request);
    }
}
