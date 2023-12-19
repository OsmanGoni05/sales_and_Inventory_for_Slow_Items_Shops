namespace sales_and_Inventory_for_Slow_Items_Shops.models;

public class ProductSale : BaseModel
{
    public int ReceiverId { get; set; }
    public int QualityId { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public DateTime Date { get; set; }
}
public class ProductSaleRequest
{
    public int ReceiverId { get; set; }
    public int QualityId { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public DateTime Date { get; set; }
}
public class ProductSaleResponse : BaseResponse
{
    public int ReceiverId { get; set; }
    public int QualityId { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public DateTime Date { get; set; }
}