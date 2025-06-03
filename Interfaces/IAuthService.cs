using Presentation.Models;

namespace Presentation.Interfaces;

public interface IAuthService
{
    Task<AuthServiceResult> SignUpAsync(SignUpRequest request);
    Task<AuthServiceResult> SignInAsync(SignInRequest request);
}