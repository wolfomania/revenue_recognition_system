using RevenueRecognitionSystem.Models.Domain;

namespace RevenueRecognitionSystem.Services;

public interface IAuthService
{
    public Task UpdateEmployeeRefreshToken(Employee employee, DateTime addDays);
    public Task<Employee?> GetEmployeeByRefreshToken(string refreshTokenRefreshToken);
    public Task AddEmployee(Employee employee);
    public Task<Employee?> GetEmployeeByLogin(string modelLogin);
}