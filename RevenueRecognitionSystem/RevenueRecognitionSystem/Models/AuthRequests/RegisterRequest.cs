using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models.AuthRequests;

public class RegisterRequest
{
    [MaxLength(100)]
    public string Login { get; set; }
    public string Password { get; set; }
}