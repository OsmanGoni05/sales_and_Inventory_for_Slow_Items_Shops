namespace sales_and_Inventory_for_Slow_Items_Shops.models;

public class InventorySummary : BaseModel
{
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]

    public int ProductQuantity { get; set; }
    public double TotalPrice { get; set; }
}
public class InventorySummaryRequest
{
    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public double TotalPrice { get; set; }
}
public class InventorySummaryResponse :  BaseResponse
{
    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public double TotalPrice { get; set; }
}