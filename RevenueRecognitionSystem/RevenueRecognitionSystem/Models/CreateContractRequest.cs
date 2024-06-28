using RevenueRecognitionSystem.Validation;

namespace RevenueRecognitionSystem.Models;

public class CreateContractRequest
{
    public int ClientId { get; set; }
    public int SoftwareId { get; set; }
    [FutureDateRange(3, 30)]
    public DateTime EndDate { get; set; }
    public int AdditionalSupportYears { get; set; }
}