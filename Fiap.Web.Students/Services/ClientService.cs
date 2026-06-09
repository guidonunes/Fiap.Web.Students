using Fiap.Web.Students.Data.Repository;
using Fiap.Web.Students.Models;

namespace Fiap.Web.Students.Services;

public class ClientService: IClientService
{
    private readonly IClientRepository _repository;

    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<ClientModel> GetAll() => _repository.GetAll();
    public ClientModel GetById(int id) => _repository.GetById(id);
    public void Add(ClientModel client) => _repository.Add(client);
    public void Update(ClientModel client) => _repository.Update(client);

    public void Delete(int id)
    {
        var client = _repository.GetById(id);
        if (client != null)
        {
            _repository.Delete(client);
        }
    }
    
}