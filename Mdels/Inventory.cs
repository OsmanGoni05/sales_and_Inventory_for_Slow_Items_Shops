using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;


namespace sales_and_Inventory_for_Slow_Items_Shops.models;

public class Inventory : BaseModel
{
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;
    public DateTime ProductionDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public string ProductStatus { get; set; } = null!;// sold|purchesed|damaged
    public Guid SerialNumber { get; set; } = Guid.NewGuid();
}
public class InventoryRequest
{
    public int ProductId { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpireDate { get; set; }
    [RegularExpression($"^{ProductStatusConstant.SOLD}|{ProductStatusConstant.PURCHASE}|{ProductStatusConstant.DAMAGE}!")]
    public string ProductStatus { get; set; } = ProductStatusConstant.SOLD;
}
public class InventoryResponse : BaseResponse
{
    public int ProductId { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public string ProductStatus { get; set; } = null!;
    public Guid SerialNumber { get; set; }
}
public class InventoryFilterRequest : BaseFilterRequest
{
    public int? ProductId { get; set; }
    public DateTime? ProductionDate { get; set; }
    public DateTime? ExpireDate { get; set; }
    public string? ProductStatus { get; set; }
    public Guid? SerialNumber { get; set; }
}