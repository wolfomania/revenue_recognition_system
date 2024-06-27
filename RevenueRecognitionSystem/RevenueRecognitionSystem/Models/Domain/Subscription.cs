using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Domain;

public class Subscription
{
    [Key]
    public int SubscriptionId { get; set; }
    [Required]
    public int ClientId { get; set; }
    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; }
    [Required]
    public int SoftwareId { get; set; }
    [ForeignKey(nameof(SoftwareId))]
    public Software Software { get; set; }
    [Required]
    public string RenewalPeriod { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public decimal PricePerPeriod { get; set; }
    public bool IsActive { get; set; }
}