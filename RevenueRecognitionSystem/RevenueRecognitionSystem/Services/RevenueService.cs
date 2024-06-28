using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Data;

namespace RevenueRecognitionSystem.Services;

public class RevenueService(DatabaseContext context, CurrencyService currencyService) : IRevenueService
{
    private readonly CurrencyService _currencyService = currencyService;

    public async Task<double> CalculateSoftwareRevenue(int softwareId, string? targetCurrency)
    {
        var revenue = await context.Contracts
            .Where(c => c.SoftwareId == softwareId)
            .Where(c => c.IsSigned)
            .SelectMany(c => c.Payments)
            .SumAsync(p => p.Amount);

        if (string.IsNullOrEmpty(targetCurrency)) return revenue;
        
        var exchangeRate = await _currencyService.GetExchangeRate(targetCurrency);
        revenue /= exchangeRate;

        return revenue;
    }
    
    public async Task<double> CalculatePredictedSoftwareRevenue(int softwareId, string? targetCurrency)
    {
        var revenue = await context.Contracts
            .Where(c => c.SoftwareId == softwareId)
            .SelectMany(c => c.Payments)
            .SumAsync(p => p.Amount);
        
        if (string.IsNullOrEmpty(targetCurrency)) return revenue;
        
        var exchangeRate = await _currencyService.GetExchangeRate(targetCurrency);
        revenue /= exchangeRate;

        return revenue;
    }
    
    public async Task<double> CalculateRevenue(string? targetCurrency)
    {
        var revenue = await context.Contracts
            .Where(c => c.IsSigned)
            .SelectMany(c => c.Payments)
            .SumAsync(p => p.Amount);
        
        if (string.IsNullOrEmpty(targetCurrency)) return revenue;
        
        var exchangeRate = await _currencyService.GetExchangeRate(targetCurrency);
        revenue /= exchangeRate;

        return revenue;
    }
    
    public async Task<double> CalculatePredictedRevenue(string? targetCurrency)
    {
        var revenue = await context.Contracts
            .SelectMany(c => c.Payments)
            .SumAsync(p => p.Amount);
        
        if (string.IsNullOrEmpty(targetCurrency)) return revenue;
        
        var exchangeRate = await _currencyService.GetExchangeRate(targetCurrency);
        revenue /= exchangeRate;

        return revenue;
    }
    
}