using Newtonsoft.Json.Linq;

namespace RevenueRecognitionSystem.Services;

public class CurrencyService(HttpClient httpClient) : BackgroundService, ICurrencyService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await httpClient.GetStringAsync("https://api.nbp.pl/api/exchangerates/tables/A?format=json", stoppingToken);
            await File.WriteAllTextAsync("exchangeRate.json", response, stoppingToken);
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
    
    public async Task<double> GetExchangeRate(string currency)
    {
        var json = await File.ReadAllTextAsync("exchangeRate.json");
        var jObject = JObject.Parse(json);

        var rates = jObject.SelectToken("$.rates");
        
        if (rates == null) throw new ArgumentException($"Exchange rate for {currency} not found");
        
        foreach (var rate in rates)
        {
            if (rate["code"]?.ToString() == currency)
            {
                return double.Parse(rate["mid"].ToString());
            }
        }

        throw new ArgumentException($"Exchange rate for {currency} not found");
    }
    
    
}