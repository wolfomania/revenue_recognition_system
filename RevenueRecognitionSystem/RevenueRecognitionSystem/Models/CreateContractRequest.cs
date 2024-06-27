using System.ComponentModel.DataAnnotations;
using NuGet.Protocol;
using RevenueRecognitionSystem.Validation;

namespace RevenueRecognitionSystem;

public class CreateContractRequest
{
    [Microsoft.Build.Framework.Required]
    public int ClientId { get; set; }
    [Microsoft.Build.Framework.Required]
    public int SoftwareId { get; set; }
    [Microsoft.Build.Framework.Required]
    [FutureDateRange(3, 30)]
    public DateTime EndDate { get; set; }
    [Microsoft.Build.Framework.Required]
    public int AdditionalSupportYears { get; set; }
}