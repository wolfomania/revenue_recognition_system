using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RevenueRecognitionSystem.Domain;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }
    
    [Required]
    public string Login { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Salt { get; set; }
    
    [Required]
    public string Role { get; set; }
    
    public string RefreshToken { get; set; }

    public DateTime? RefreshTokenExp { get; set; }
    
}