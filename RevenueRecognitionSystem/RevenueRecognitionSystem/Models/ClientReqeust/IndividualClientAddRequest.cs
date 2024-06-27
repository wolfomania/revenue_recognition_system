using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem;

public class IndividualClientAddRequest
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Address { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Phone]
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    [Length(maximumLength:11, minimumLength:11, ErrorMessage = "PESEL must be 11 characters long")]
    public string PESEL { get; set; }

}