using sales_and_Inventory_for_Slow_Items_Shops.models;

namespace sales_and_Inventory_for_Slow_Items_Shops;

public class Brand : BaseModel
{
    public string Name { get; set; } =string.Empty;
}
public class BrandRequest
{
     public string Name { get; set; } = string.Empty;
}

public class BrandResponse : BaseResponse
{
     public string Name { get; set; } = string.Empty;
}
public class BrandFilterRequest : BaseFilterRequest
{
     public string? Name { get; set; } = string.Empty;
}
