using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Data;
using RevenueRecognitionSystem.Models.Domain;

namespace RevenueRecognitionSystem.Services;

public class ContractService : IContractService
{
    
    private readonly DatabaseContext _context;

    public ContractService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Software?> GetSoftwareById(int requestSoftwareId)
    {
        return await _context.Softwares.FirstOrDefaultAsync(s => s.SoftwareId == requestSoftwareId);
    }
    
    
    public async Task<Contract?> GetContractById(int contractId)
    {
        return await _context.Contracts
            .Include(c => c.Payments)
            .Include(c => c.Client)
            .Include(c => c.Software)
            .FirstOrDefaultAsync(c => c.ContractId == contractId);
    }
    
    public async Task AddContract(Contract contract)
    {
        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsActiveContract(int clientClientId, int softwareSoftwareId)
    {
        return await _context.Contracts.AnyAsync(c => 
            c.ClientId == clientClientId 
            && c.SoftwareId == softwareSoftwareId 
            && c.StartDate.AddYears(c.AdditionalSupportYears + 1) > DateTime.Now
            && c.IsSigned);
    }

    public async Task<bool> IsPreviousCustomer(int clientClientId)
    {
        return await _context.Contracts.AnyAsync(c => c.ClientId == clientClientId && c.IsSigned);
    }

    public async Task<Discount?> GetDiscountForSoftware(int softwareSoftwareId)
    {
        return await _context.SoftwareDiscounts
            .Include(sd => sd.Discount)
            .Where(sd => sd.SoftwareId == softwareSoftwareId)
            .OrderByDescending(sd => sd.Discount.Value)
            .Select(sd => sd.Discount)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteContract(Contract contract)
    {
        _context.Contracts.Remove(contract);
        await _context.SaveChangesAsync();
    }

    public async Task AddPayment(Payment payment)
    {
        _context.Payments.Add(payment);
        var total = await _context.Payments
            .Where(p => p.ContractId == payment.ContractId)
            .SumAsync(p => p.Amount);
        
        var contract = await _context.Contracts.FindAsync(payment.ContractId);
        
        if (total >= contract.FinalPrice)
        {
            contract.IsSigned = true;
        }
        
        await _context.SaveChangesAsync();
    }
}