using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Data;
using RevenueRecognitionSystem.Models.ClientRequest;
using RevenueRecognitionSystem.Models.Domain;

namespace RevenueRecognitionSystem.Services;

public class ClientService : IClientService
{
    private readonly DatabaseContext _context;

    public ClientService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<Client?> GetClientById(int id)
    {
        return await _context.Clients.FirstOrDefaultAsync(e => e.ClientId == id);
    }

    public async Task DeleteClient(Client client)
    {
        if (client.PESEL != null)
        {
            client.FirstName = null;
            client.LastName = null;
            client.Email = null;
            client.PhoneNumber = null;
            client.PESEL = null;
        }
        client.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task AddClient(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateIndividualClient(Client client, UpdateIndividualClientRequest request)
    {
        client.FirstName = request.FirstName ?? client.FirstName;
        client.LastName = request.LastName ?? client.LastName;
        client.Address = request.Address ?? client.Address;
        client.Email = request.Email ?? client.Email;
        client.PhoneNumber = request.PhoneNumber ?? client.PhoneNumber;
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateCorporateClient(Client client, UpdateCorporateClientRequest request)
    {
        client.CompanyName = request.CompanyName ?? client.CompanyName;
        client.Address = request.Address ?? client.Address;
        client.Email = request.Email ?? client.Email;
        client.PhoneNumber = request.PhoneNumber ?? client.PhoneNumber;
        await _context.SaveChangesAsync();
    }
}