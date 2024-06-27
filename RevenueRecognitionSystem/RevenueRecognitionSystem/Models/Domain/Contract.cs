using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Domain;

public class Contract
{
    [Key]
    public int ContractId { get; set; }
    [Required]
    public int ClientId { get; set; }
    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; }
    [Required]
    public int SoftwareId { get; set; }
    [ForeignKey(nameof(SoftwareId))]
    public Software Software { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public decimal Price { get; set; }
    public bool IsSigned { get; set; }
    public bool IsActive { get; set; }
    [Required]
    public string Version { get; set; }
    public int AdditionalSupportYears { get; set; }
    public decimal FinalPrice { get; set; }

    public ICollection<Payment> Payments { get; set; }
}