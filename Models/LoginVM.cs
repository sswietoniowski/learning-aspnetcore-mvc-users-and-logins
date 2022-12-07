using System.ComponentModel.DataAnnotations;

namespace learning_aspnetcore_mvc_users_and_logins.Models;

public class LoginVM
{
    [Display(Name = "User Name")]
    [Required]
    public string UserName { get; set; } = default!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    public bool RememberMe { get; set; } = false;
}