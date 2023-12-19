namespace sales_and_Inventory_for_Slow_Items_Shops.models;

public class ProductPerches : BaseModel
{
    public int ProductId { get; set; }
    public int QualityId { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public DateTime Date { get; set; }
}
public class ProductPerchesRequest
{
    public int ProductId { get; set; }
    public int QualityId { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public DateTime Date { get; set; }
}
public class ProductPerchesResponse : BaseResponse
{
    public int ProductId { get; set; }
    public int QualityId { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public DateTime Date { get; set; }
}