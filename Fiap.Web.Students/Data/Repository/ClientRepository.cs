using Fiap.Web.Students.Models;

namespace Fiap.Web.Students.Data.Repository;

public class ClientRepository: IClientRepository
{
    private readonly DatabaseContext _databaseContext;
    
    public ClientRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public void Add(ClientModel clientModel)
    {
        _databaseContext.Client.Add(clientModel);
        _databaseContext.SaveChanges();
    }

    public void Delete(ClientModel clientModel)
    {
        _databaseContext.Client.Remove(clientModel);
        _databaseContext.SaveChanges();
    }

    public IEnumerable<ClientModel> GetAll()
    {
        return _databaseContext.Client.ToList();
    }

    public ClientModel GetById(int id)
    {
        return _databaseContext.Client.Find(id);
    }

    public void Update(ClientModel clientModel)
    {
        _databaseContext.Client.Update(clientModel);
        _databaseContext.SaveChanges();
    }
}