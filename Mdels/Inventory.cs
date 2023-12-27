using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;


namespace sales_and_Inventory_for_Slow_Items_Shops.models;

public class Inventory : BaseModel
{
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;
    public double PerchesPricePerUnit { get; set; }
    public double SalesPricePerUnit { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public string ProductStatus { get; set; } = null!;// sold|purchesed|damaged
    public Guid SerialNumber { get; set; } = Guid.NewGuid();
}
public class InventoryRequest
{
    public int ProductId { get; set; }
    public double PerchesPricePerUnit { get; set; }
    public double SalesPricePerUnit { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpireDate { get; set; }
    [RegularExpression($"^{ProductStatus.SOLD}|{ProductStatus.PURCHASE}|{ProductStatus.DAMAGE}!")]
    public string Status { get; set; } = ProductStatus.SOLD;
}
public class InventoryResponse : BaseResponse
{
    public int ProductId { get; set; }
    public double PerchesPricePerUnit { get; set; }
    public double SalesPricePerUnit { get; set; }
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
    public string? ProductStatus { get; set; } = null!;
    public Guid? SerialNumber { get; set; }
}