namespace Fiap.Web.Students.Models;

public class StoreModel
{
    public int StoreId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    
    
    public List<OrderModel>  Orders { get; set; }
}