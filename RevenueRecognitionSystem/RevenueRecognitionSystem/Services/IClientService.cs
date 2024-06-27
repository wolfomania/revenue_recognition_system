using RevenueRecognitionSystem.Domain;

namespace RevenueRecognitionSystem.Services;

public interface IClientService
{
    public Task AddClient(Client client);

    public Task<Client?> GetClientById(int id);

    Task DeleteClient(Client client);
    Task UpdateIndividualClient(Client client, UpdateIndividualClientRequest request);
    Task UpdateCorporateClient(Client client, UpdateCorporateClientRequest request);
}