namespace sales_and_Inventory_for_Slow_Items_Shops.models;

public class Unit : BaseModel
{
    public string Name { get; set; } = string.Empty;
}

public class UnitRequest
{
     public string Name { get; set; } = string.Empty;
}

public class UnitResponse : BaseResponse
{
     public string Name { get; set; } = string.Empty;
}
public class UnitFilterRequest : BaseFilterRequest
{
     public string? Name { get; set; } = string.Empty;
}