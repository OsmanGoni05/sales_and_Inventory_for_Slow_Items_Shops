using sales_and_Inventory_for_Slow_Items_Shops.models;

namespace sales_and_Inventory_for_Slow_Items_Shops;

public class Quality : BaseModel
{
    public string Name { get; set; } = string.Empty;
}
public class QualityRequest
{
     public string Name { get; set; } = string.Empty;
}

public class QualityResponse : BaseResponse
{
     public string Name { get; set; } = string.Empty;
}
public class QualityFilterRequest : BaseFilterRequest
{
     public string? Name { get; set; } = string.Empty;
}
