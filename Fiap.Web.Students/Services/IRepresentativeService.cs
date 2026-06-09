using Fiap.Web.Students.Models;

namespace Fiap.Web.Students.Services;

public interface IRepresentativeService
{
    IEnumerable<RepresentativeModel> GetAll();
}