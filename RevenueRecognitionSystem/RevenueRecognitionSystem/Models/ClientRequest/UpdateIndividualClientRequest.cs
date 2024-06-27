using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models.ClientRequest;

public class UpdateIndividualClientRequest
{
    [MaxLength(50)]
    public string? FirstName { get; set; }
    [MaxLength(100)]
    public string? LastName { get; set; }
    [MaxLength(200)]
    public string? Address { get; set; }
    [MaxLength(200)]
    public string? Email { get; set; }
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }
}