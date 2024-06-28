using RevenueRecognitionSystem.Models.Domain;

namespace RevenueRecognitionSystem.Services;

public interface IContractService
{
    Task<Software?> GetSoftwareById(int requestSoftwareId);
    Task<bool> IsActiveContract(int clientClientId, int softwareSoftwareId);
    Task<bool> IsPreviousCustomer(int clientClientId);
    Task<Discount?> GetDiscountForSoftware(int softwareSoftwareId);
    Task<Contract?> GetContractById(int id);
    Task AddContract(Contract contract);
    Task DeleteContract(Contract contract);
    Task AddPayment(Payment payment);
}