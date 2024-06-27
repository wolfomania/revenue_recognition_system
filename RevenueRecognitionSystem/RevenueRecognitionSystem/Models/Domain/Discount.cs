using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Domain;

public class Discount
{
    [Key]
    public int DiscountId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string OfferType { get; set; }
    [Required]
    public float Value { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    
    public ICollection<SoftwareDiscount> SoftwareDiscounts { get; set; }
}