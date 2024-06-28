using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevenueRecognitionSystem.Services;

namespace RevenueRecognitionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController(IRevenueService revenueService) : ControllerBase
    {
        [Authorize]
        [HttpGet("current")]
        public async Task<IActionResult> CalculateRevenue(int? softwareId, string? targetCurrency)
        {
            var revenue = softwareId.HasValue
                ? await revenueService.CalculateSoftwareRevenue(softwareId.Value, targetCurrency)
                : await revenueService.CalculateRevenue(targetCurrency);
            
            var response = "Current revenue " + (softwareId.HasValue ? "for software with id " + softwareId : "for entire company") + ": " + revenue + " " + (targetCurrency ?? "PLN");
            return Ok(response);
        }
        
        [Authorize]
        [HttpGet("predicted")]
        public async Task<IActionResult> CalculatePredictedRevenue(int? softwareId, string? targetCurrency)
        {
            var revenue = softwareId.HasValue
                ? await revenueService.CalculatePredictedSoftwareRevenue(softwareId.Value, targetCurrency)
                : await revenueService.CalculatePredictedRevenue(targetCurrency);
            var response = "Predicted revenue " + (softwareId.HasValue ? "for software with id " + softwareId : "for entire company") + ": " + revenue + " " + (targetCurrency ?? "PLN");
            return Ok(response);
        }
    }
}
