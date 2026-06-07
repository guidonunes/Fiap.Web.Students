using Microsoft.EntityFrameworkCore.Metadata;

namespace Fiap.Web.Students.Models;

public class OrderModel
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    
    public int ClientId { get; set; }
    public ClientModel Client { get; set; }
    
    public int StoreId { get; set; }
    public StoreModel Store { get; set; }
    
    public List<OrderProductModel> OrderProducts { get; set; }
}