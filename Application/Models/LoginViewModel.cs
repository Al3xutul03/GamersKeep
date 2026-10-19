using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Enter your username.")]
    [Display(Name = "Username")]
    public string Username { get; set; } = "";

    [Required(ErrorMessage = "Enter your password.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Display(Name = "Keep me signed in")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}
