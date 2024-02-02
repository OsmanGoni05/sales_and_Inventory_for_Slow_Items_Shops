using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace sales_and_Inventory_for_Slow_Items_Shops.models;

public class ProductType : BaseModel
{
    public string ProductName { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = new();
}

public class ProductTypeRequest
{
    [Required]
    public string ProductName { get; set; } = string.Empty;
}

public class ProductTypeResponse : BaseResponse
{
    public string ProductName { get; set; } = string.Empty;
    public List<ProductResponse> Products { get; set; } = new();
}

public class ProductTypeFilterRequest : BaseFilterRequest
{
    public string? ProductName { get; set; } = string.Empty;
}
//
