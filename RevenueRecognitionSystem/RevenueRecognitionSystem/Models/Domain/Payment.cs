using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Models.Domain;

public class Payment
{
    [Key]
    public int PaymentId { get; set; }
    public int ContractId { get; set; }
    [ForeignKey(nameof(ContractId))]
    public Contract Contract { get; set; }
    public double Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}