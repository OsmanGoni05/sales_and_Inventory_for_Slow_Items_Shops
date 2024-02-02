namespace  sales_and_Inventory_for_Slow_Items_Shops.models;

public class BaseResponse{
    public int Id {get; set;}
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
}