using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Entities;
using Presentation.Interfaces;
using Presentation.Models;

namespace Presentation.Controllers;

[Route("/api/auth")]
[ApiController]
public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IAuthService authService)
    : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly IAuthService _authService = authService;

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        
        var result = await _authService.SignUpAsync(request);
        if (!result.Succeeded)
            return Conflict(result);
        
        return Ok(result);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(SignInRequest request)
    {
       if (!ModelState.IsValid)
           return BadRequest(ModelState);
       
       var result = await _authService.SignInAsync(request);
       if (!result.Succeeded)
           return Unauthorized(result);
       
       return Ok(result);
    }
}