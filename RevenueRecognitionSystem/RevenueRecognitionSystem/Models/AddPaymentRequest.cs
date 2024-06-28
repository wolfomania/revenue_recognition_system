using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models;

public class AddPaymentRequest
{
    public int PaymentId { get; set; }
    [Range(0, double.MaxValue)]
    public double Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}