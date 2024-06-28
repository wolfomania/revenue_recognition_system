using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Data;
using RevenueRecognitionSystem.Helpers;
using RevenueRecognitionSystem.Models.Domain;

namespace RevenueRecognitionSystem.Services;

public class AuthService : IAuthService
{
    private readonly DatabaseContext _context;

    public AuthService(DatabaseContext context)
    {
        _context = context;
    }


    public async Task<Employee?> GetEmployeeByLogin(string modelLogin)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Login == modelLogin);
    }


    public async Task AddEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<Employee?> GetEmployeeByRefreshToken(string refreshTokenRefreshToken)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.RefreshToken == refreshTokenRefreshToken);
    }

    public async Task UpdateEmployeeRefreshToken(Employee employee, DateTime addDays)
    {
        employee.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        employee.RefreshTokenExp = addDays;
        await _context.SaveChangesAsync();
    }
}