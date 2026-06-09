using Fiap.Web.Students.Models;

namespace Fiap.Web.Students.Services;

public interface IClientService
{
    IEnumerable<ClientModel> GetAll();
    ClientModel GetById(int id);
    void Add(ClientModel client);
    void Update(ClientModel client);
    void Delete(int id);
}