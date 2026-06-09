using Fiap.Web.Students.Data;
using Fiap.Web.Students.Data.Repository;
using Fiap.Web.Students.Models;

namespace Fiap.Web.Students.Services;

public class RepresentativeService : IRepresentativeService
{
    private readonly IRepresentativeRepository _repository;

    public RepresentativeService(IRepresentativeRepository repository)
    {
        _repository = repository;
    }
    
    
    public IEnumerable<RepresentativeModel> GetAll() => _repository.GetAll();
    
}