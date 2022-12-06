using System.ComponentModel.DataAnnotations;

namespace learning_aspnetcore_mvc_users_and_logins.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(64)]
    public string Login { get; set; } = default!;
    [Required]
    [MaxLength(32)]
    public string Password { get; set; } = default!;
    [Required]
    public string Salt { get; set; } = default!;
    [Required]
    public Role Role { get; set; }
}