namespace sales_and_Inventory_for_Slow_Items_Shops.models;
public class BaseFilterRequest
{
    public int Page { get; set; } = 1;
    public int Take { get; set; } = 10;
}