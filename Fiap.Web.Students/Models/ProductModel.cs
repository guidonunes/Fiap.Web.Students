namespace Fiap.Web.Students.Models;

public class ProductModel
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    
    
    public int SupplierId { get; set; }
    public SupplierModel Supplier { get; set; }
    
    
    public List<OrderProductModel> OrderProducts { get; set; }
}