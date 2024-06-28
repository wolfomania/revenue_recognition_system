namespace RevenueRecognitionSystem.Services;

public interface IRevenueService
{
    Task<double> CalculateSoftwareRevenue(int softwareId, string? targetCurrency);
    Task<double> CalculatePredictedSoftwareRevenue(int softwareId, string? targetCurrency);
    Task<double> CalculateRevenue(string? targetCurrency);
    Task<double> CalculatePredictedRevenue(string? targetCurrency);
    
    
}