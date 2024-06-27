using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Domain;

public class Payment
{
    [Key]
    public int PaymentId { get; set; }
    [Required]
    public int ContractId { get; set; }
    [ForeignKey(nameof(ContractId))]
    public Contract Contract { get; set; }
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public DateTime PaymentDate { get; set; }
}