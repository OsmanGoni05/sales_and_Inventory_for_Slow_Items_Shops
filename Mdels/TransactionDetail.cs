using System.ComponentModel.DataAnnotations.Schema;
using sales_and_Inventory_for_Slow_Items_Shops.models;

namespace sales_and_Inventory_for_Slow_Items_Shops;

public class TransactionDetail : BaseModel
{
    public int InventoryId { get; set; }
    [ForeignKey(nameof(InventoryId))]
    public Inventory Inventory { get; set; } = null!;
    public int TransactionId { get; set; }
    [ForeignKey(nameof(TransactionId))]
    public Transaction Transaction { get; set; } = null!;
}

public class TransactionDetailRequest
{
    public int Id { get; set; }
    public int InventoryId { get; set; }
    public int TransactionId { get; set; }
}

public class TransactionDetailResponse : BaseResponse
{
    public int InventoryId { get; set; }
    public int TransactionId { get; set; }

}

public class TransactionDetailFilerRequest : BaseFilterRequest
{
    public int? InventoryId { get; set; }
    public int? TransactionId { get; set; }
}

