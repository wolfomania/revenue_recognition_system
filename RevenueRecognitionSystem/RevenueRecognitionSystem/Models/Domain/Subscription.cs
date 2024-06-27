using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Models.Domain;

public class Subscription
{
    [Key]
    public int SubscriptionId { get; set; }
    public int ClientId { get; set; }
    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; }
    public int SoftwareId { get; set; }
    [ForeignKey(nameof(SoftwareId))]
    public Software Software { get; set; }
    public string RenewalPeriod { get; set; }
    public DateTime StartDate { get; set; }
    public double PricePerPeriod { get; set; }
    public bool IsActive { get; set; }
}