using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models.Domain;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }
    [MaxLength(100)]
    public string Login { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    [MaxLength(100)]
    public string Role { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenExp { get; set; }
    
}