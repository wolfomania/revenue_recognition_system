using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Models.Domain;

public class Contract
{
    [Key]
    public int ContractId { get; set; }
    public int ClientId { get; set; }
    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; }
    public int SoftwareId { get; set; }
    [ForeignKey(nameof(SoftwareId))]
    public Software Software { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Price { get; set; }
    public bool IsSigned { get; set; }
    [MaxLength(50)]
    public string Version { get; set; }
    public int AdditionalSupportYears { get; set; }
    public double FinalPrice { get; set; }

    public ICollection<Payment> Payments { get; set; }
}