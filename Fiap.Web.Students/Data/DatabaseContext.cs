using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Students.Data;

public class DatabaseContext: DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
}