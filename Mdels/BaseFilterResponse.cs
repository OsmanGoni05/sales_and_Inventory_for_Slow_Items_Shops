namespace sales_and_Inventory_for_Slow_Items_Shops.models;
    public class BaseFilterResponse 
{
    public dynamic Data { get; set; }
    public int totalElements { get; set; }
    public int Page { get; set; } = 1;
    public int Take { get; set; } = 10;
    public int TotalPage { get; set; }
}
