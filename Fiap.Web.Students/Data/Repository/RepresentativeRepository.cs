using Fiap.Web.Students.Models;

namespace Fiap.Web.Students.Data.Repository;

public class RepresentativeRepository: IRepresentativeRepository
{
   private readonly DatabaseContext _databaseContext;

   public RepresentativeRepository(DatabaseContext databaseContext)
   {
      _databaseContext = databaseContext;
   }
   
   public void Add(RepresentativeModel representative)
   {
      _databaseContext.Representative.Add(representative);
      _databaseContext.SaveChanges();
   }

   public void Delete(RepresentativeModel representative)
   {
      _databaseContext.Representative.Remove(representative);
      _databaseContext.SaveChanges();
   }

   public IEnumerable<RepresentativeModel> GetAll()
   {
      return _databaseContext.Representative.ToList();
   }

   public RepresentativeModel GetById(int id)
   {
       return _databaseContext.Representative.Find(id);
   }

   public void Update(RepresentativeModel representative)
   {
      _databaseContext.Representative.Update(representative);
      _databaseContext.SaveChanges();
   }
}