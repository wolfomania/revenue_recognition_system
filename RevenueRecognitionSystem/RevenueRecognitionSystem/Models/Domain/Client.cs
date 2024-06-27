using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models.Domain;

public class Client
{
    [Key]
    public int ClientId { get; set; }
    [MaxLength(50)]
    public string? FirstName { get; set; }
    [MaxLength(100)]
    public string? LastName { get; set; }
    [MaxLength(200)]
    public string? Address { get; set; }
    [MaxLength(200)]
    public string? Email { get; set; }
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }
    [MaxLength(11)]
    public string? PESEL { get; set; }
    [MaxLength(150)]
    public string? CompanyName { get; set; }
    [MaxLength(200)]
    public string? KRS { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Contract> Contracts { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; }
}