using Microsoft.AspNetCore.Identity;
using Presentation.Entities;
using Presentation.Extensions;
using Presentation.Interfaces;
using Presentation.Models;

namespace Presentation.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly IAuthRepository _repository;

    public AuthService(
        UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager,
        IAuthRepository repository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _repository = repository;
    }

    public async Task<AuthServiceResult> SignUpAsync(SignUpRequest request)
    {
        try
        {
            if (request.Password != request.ConfirmPassword)
                return new AuthServiceResult { Succeeded = false, Error = "Passwords do not match" };

            var existingUser = await _repository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new AuthServiceResult { Succeeded = false, Error = "Email already exists" };
            }

            var user = request.MapToEntity();
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthServiceResult { Succeeded = false, Error = errorMessage };
            }

            return new AuthServiceResult { Succeeded = true };
        }
        catch (Exception ex)
        {
            return new AuthServiceResult { Succeeded = false, Error = "Server Error" + ex.Message };
        }
    }

    public async Task<AuthServiceResult> SignInAsync(SignInRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return new AuthServiceResult { Succeeded = false, Error = "Email does not exist" };
        
        var result =  await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            return new AuthServiceResult { Succeeded = false, Error = "Invalid email or password" };
        
        return new AuthServiceResult { Succeeded = true };
    }
}