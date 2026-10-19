using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Choose a username.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Use 3 to 100 characters.")]
    [Display(Name = "Username")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Enter your email address.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [StringLength(255)]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Choose a password.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Use at least 8 characters.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Required(ErrorMessage = "Repeat your password.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
    [Display(Name = "Confirm password")]
    public string ConfirmPassword { get; set; } = "";
}
