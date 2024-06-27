using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevenueRecognitionSystem.Domain;
using RevenueRecognitionSystem.Services;

namespace RevenueRecognitionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IClientService clientService) : ControllerBase
    {
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetClient(int id)
        {
            var client = await clientService.GetClientById(id);
            if (client == null)
            {
                return NotFound();
            }

            if (client.PESEL != null)
            {
                return Ok(new
                {
                    firstName = client.FirstName,
                    lastName = client.LastName,
                    address = client.Address,
                    email = client.Email,
                    phoneNumber = client.PhoneNumber,
                    client.PESEL
                });
            }

            return Ok(new
            {
                companyName = client.CompanyName,
                address = client.Address,
                email = client.Email,
                phoneNumber = client.PhoneNumber,
                client.KRS
            });
        }
        
        [Authorize]
        [HttpPost("individual")]
        public async Task<IActionResult> AddIndividualClient([FromBody] IndividualClientAddRequest addRequest)
        {
            
            var client = new Client
            {
                FirstName = addRequest.FirstName,
                LastName = addRequest.LastName,
                Address = addRequest.Address,
                Email = addRequest.Email,
                PhoneNumber = addRequest.PhoneNumber,
                PESEL = addRequest.PESEL,
                IsDeleted = false
            };

            await clientService.AddClient(client);

            return CreatedAtAction(nameof(GetClient), new { id = client.ClientId }, client);
        }
        
        [Authorize]
        [HttpPut("individual/{id:int}")]
        public async Task<IActionResult> UpdateIndividualClient(int id, [FromBody] UpdateIndividualClientRequest request)
        {
            var client = await clientService.GetClientById(id);
            if (client?.PESEL == null)
            {
                return NotFound();
            }
            
            await clientService.UpdateIndividualClient(client, request);

            return NoContent();
        }
        
        [Authorize]
        [HttpPost("corporate")]
        public async Task<IActionResult> AddCorporateClient([FromBody] CorporateClientAddRequest addRequest)
        {
            
            var client = new Client
            {
                CompanyName = addRequest.CompanyName,
                Address = addRequest.Address,
                Email = addRequest.Email,
                PhoneNumber = addRequest.PhoneNumber,
                KRS = addRequest.KRS,
                IsDeleted = false
            };

            await clientService.AddClient(client);

            return CreatedAtAction(nameof(GetClient), new { id = client.ClientId }, client);
        }
        
        [Authorize]
        [HttpPut("corporate/{id:int}")]
        public async Task<IActionResult> UpdateCorporateClient(int id, [FromBody] UpdateCorporateClientRequest request)
        {
            var client = await clientService.GetClientById(id);
            if (client?.KRS == null)
            {
                return NotFound();
            }
            
            await clientService.UpdateCorporateClient(client, request);

            return NoContent();
        }
        
        
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await clientService.GetClientById(id);
            if (client == null)
            {
                return NotFound();
            }

            await clientService.DeleteClient(client);

            return NoContent();
        }
    }
    
}
