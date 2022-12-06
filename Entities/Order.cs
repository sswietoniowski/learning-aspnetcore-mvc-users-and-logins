using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace learning_aspnetcore_mvc_users_and_logins.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(128)]
    public string Product { get; set; } = string.Empty;
    [Required]
    public int Quantity { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    [Precision(18, 2)]
    public decimal Price { get; set; }
    [Required]
    public User User { get; set; } = default!;
    [Required]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
}