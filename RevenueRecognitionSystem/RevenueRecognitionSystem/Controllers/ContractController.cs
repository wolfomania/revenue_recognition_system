using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevenueRecognitionSystem.Models;
using RevenueRecognitionSystem.Models.Domain;
using RevenueRecognitionSystem.Services;

namespace RevenueRecognitionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        
        private readonly IClientService _clientService;
        private readonly IContractService _contractService;

        public ContractController(IClientService clientService, IContractService contractService)
        {
            _clientService = clientService;
            _contractService = contractService;
        }
        
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetContract(int id)
        {
            var contract = await _contractService.GetContractById(id);
            
            if (contract == null)
            {
                return NotFound("Contract not found");
            }

            return Ok(new {
                contract.ContractId,
                client = new
                {
                    contract.Client.ClientId,
                    contract.Client.FirstName,
                    contract.Client.LastName,
                    contract.Client.Email,
                    contract.Client.PhoneNumber,
                    contract.Client.Address,
                    contract.Client.PESEL,
                },
                software  = new
                {
                    contract.Software.SoftwareId,
                    contract.Software.Name,
                    contract.Software.CurrentVersion,
                    contract.Software.Price,
                    contract.Software.Category,
                    contract.Software.Description
                },
                contract.StartDate,
                contract.EndDate,
                contract.Version,
                contract.Price,
                contract.FinalPrice,
                contract.AdditionalSupportYears,
                contract.IsSigned,
                contract.Payments
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateContract([FromBody] CreateContractRequest request)
        {
            
            var client = await _clientService.GetClientById(request.ClientId);
            
            if (client == null)
            {
                return NotFound("Client not found");
            }
            
            var software = await _contractService.GetSoftwareById(request.SoftwareId);
            
            if (software == null)
            {
                return NotFound("Software not found");
            }
            
            if (await _contractService.IsActiveContract(client.ClientId, software.SoftwareId))
            {
                return BadRequest("Client already has active contract for this software");
            }
            
            var price = software.Price * 12 + request.AdditionalSupportYears * 1000;
            
            double finalDiscountPercentage = 0;

            if (await _contractService.IsPreviousCustomer(client.ClientId))
            {
                finalDiscountPercentage += 5;
            }
            
            var discount = await _contractService.GetDiscountForSoftware(software.SoftwareId);
            
            finalDiscountPercentage += discount?.Value ?? 0;

            var contract = new Contract
            {
                ClientId = request.ClientId,
                Client = client,
                SoftwareId = request.SoftwareId,
                Software = software,
                StartDate = DateTime.Now,
                EndDate = request.EndDate,
                Version = software.CurrentVersion,
                Price = price,
                FinalPrice = price * (1 - finalDiscountPercentage),
                AdditionalSupportYears = request.AdditionalSupportYears,
                IsSigned = false
            };

            await _contractService.AddContract(contract);
            
            return CreatedAtAction(nameof(GetContract), new { id = contract.ContractId }, GetContract(contract.ContractId));
        }
        
        [Authorize]
        [HttpPost("{contactId:int}/payment")]
        public async Task<IActionResult> AddPayment(int contactId, [FromBody] AddPaymentRequest request)
        {
            var contract = await _contractService.GetContractById(contactId);
            
            if (contract == null)
            {
                return NotFound("Contract not found");
            }

            if (contract.IsSigned)
            {
                return BadRequest("Contract is already signed");
            }
            
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            if (contract.EndDate < DateTime.Now)
            {
                await _contractService.DeleteContract(contract);
                transaction.Complete();
                return BadRequest("Contract was not signed in time. Your payments will be 'returned'(maybe). Contract is deleted, please create new one");
            }
            
            if (contract.EndDate < request.PaymentDate)
            {
                return BadRequest("Payment date is after contract end date.");
            }
            
            var payment = new Payment
            {
                ContractId = contract.ContractId,
                Contract = contract,
                Amount = request.Amount,
                PaymentDate = request.PaymentDate
            };
            
            await _contractService.AddPayment(payment);

            transaction.Complete();
            
            return Ok();
        }
    }
}
