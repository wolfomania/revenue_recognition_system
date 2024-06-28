using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models.ClientRequest;

public class CorporateClientAddRequest
{
    [MaxLength(150)]
    public string CompanyName { get; set; }
    [MaxLength(200)]
    public string Address { get; set; }
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; }
    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; set; }
    [MaxLength(200)]
    public string KRS { get; set; }
}