using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RevenueRecognitionSystem.Domain;

[PrimaryKey(nameof(SoftwareId), nameof(DiscountId))]
public class SoftwareDiscount
{
    public int SoftwareId { get; set; }
    [ForeignKey(nameof(SoftwareId))]
    public Software Software { get; set; }

    public int DiscountId { get; set; }    
    [ForeignKey(nameof(DiscountId))]
    public Discount Discount { get; set; }
}