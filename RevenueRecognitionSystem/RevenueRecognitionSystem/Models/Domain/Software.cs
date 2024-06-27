using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace RevenueRecognitionSystem.Domain;

public class Software
{
    [Key]
    public int SoftwareId { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public string CurrentVersion { get; set; }
    [Required]
    public string Category { get; set; }

    public ICollection<Contract> Contracts { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; }
    public ICollection<SoftwareDiscount> SoftwareDiscounts { get; set; }
}