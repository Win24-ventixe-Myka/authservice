using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class SignUpRequest
{
    [Required]
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; } = null!;
    
    public string ConfirmPassword { get; set; } = null!;
}