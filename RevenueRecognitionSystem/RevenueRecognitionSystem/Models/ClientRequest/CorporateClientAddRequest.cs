using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models.ClientRequest;

public class CorporateClientAddRequest
{
    [Required]
    public string CompanyName { get; set; }
    [Required]
    public string Address { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Phone]
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string KRS { get; set; }
}