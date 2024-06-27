using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem;

public class UpdateCorporateClientRequest
{
    [MaxLength(200)]
    public string? Address { get; set; }
    [MaxLength(200)]
    public string? Email { get; set; }
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }
    [MaxLength(150)]
    public string? CompanyName { get; set; }
}