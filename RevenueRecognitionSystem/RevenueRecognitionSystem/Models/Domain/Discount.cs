using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models.Domain;

public class Discount
{
    [Key]
    public int DiscountId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string? OfferType { get; set; }
    [Range(0, 100)]
    public double Value { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public ICollection<SoftwareDiscount> SoftwareDiscounts { get; set; }
}