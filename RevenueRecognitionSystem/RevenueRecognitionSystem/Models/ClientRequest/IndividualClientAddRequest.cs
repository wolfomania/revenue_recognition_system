using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models.ClientRequest;

public class IndividualClientAddRequest
{
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    [MaxLength(200)]
    public string Address { get; set; }
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; }
    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; set; }
    [Length(maximumLength:11, minimumLength:11, ErrorMessage = "PESEL must be 11 characters long")]
    public string PESEL { get; set; }

}