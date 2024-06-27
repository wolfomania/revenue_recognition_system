using System.ComponentModel.DataAnnotations;
using Contract = RevenueRecognitionSystem.Models.Domain.Contract;

namespace RevenueRecognitionSystem.Models.Domain;

public class Software
{
    [Key]
    public int SoftwareId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
    [MaxLength(20)]
    public string CurrentVersion { get; set; }
    [MaxLength(100)]
    public string Category { get; set; }
    [Range(0, double.MaxValue)]
    public double Price { get; set; }

    public ICollection<Contract> Contracts { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; }
    public ICollection<SoftwareDiscount> SoftwareDiscounts { get; set; }
}