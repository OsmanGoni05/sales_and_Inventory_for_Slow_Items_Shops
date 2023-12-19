using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sales_and_Inventory_for_Slow_Items_Shops.models;

public class Product : BaseModel
{
    public int ProductTypeId { get; set; }

    [ForeignKey(nameof(ProductTypeId))]
    public ProductType ProductType { get; set; } = null!;
    public double Price { get; set; }

}

public class ProductRequest
{

    [Required]
    public int ProductTypeId { get; set; }
    public double Price { get; set; }
}

public class ProductResponse : BaseResponse
{
    public int ProductTypeId { get; set; }
    public ProductTypeResponse ProductType { get; set; } = null!;
}