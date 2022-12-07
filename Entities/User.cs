using System.ComponentModel.DataAnnotations;

namespace learning_aspnetcore_mvc_users_and_logins.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(64)]
    public string UserName { get; set; } = default!;
    [Required]
    [MaxLength(512)]
    public string PasswordHash { get; set; } = default!;
    [Required]
    [MaxLength(512)]
    public string PasswordSalt { get; set; } = default!;
    [Required]
    public Role Role { get; set; }
}