namespace RevenueRecognitionSystem.Services;

public interface ICurrencyService
{
    public Task<double> GetExchangeRate(string currency);

}