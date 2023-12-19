using System.ComponentModel.DataAnnotations.Schema;

namespace sales_and_Inventory_for_Slow_Items_Shops.models;

public class Inventory : BaseModel
{
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;
    public int UnitId { get; set; }
    [ForeignKey(nameof(UnitId))]
    public Unit Unit {get; set;} = null!;
    public double PerchesPricePerUnit { get; set; }
    public double SalesPricePerUnit { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public string ProductStatus { get; set; } = string.Empty;
    public Guid SerialNumber { get; set; } = Guid.NewGuid();

    // [ForeignKey(nameof(ProductId))]
    // public Product Product { get; set; } = null!;
}
public class InventoryRequest
{
    public int ProductId { get; set; }
    public int UnitId { get; set; }
    public double PerchesPricePerUnit { get; set; }
    public double SalesPricePerUnit { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public string ProductStatus { get; set; } = string.Empty;
}
public class InventoryResponse : BaseResponse
{
    public int ProductId { get; set; }
    public int UnitId { get; set; }
    public double PerchesPricePerUnit { get; set; }
    public double SalesPricePerUnit { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public string ProductStatus { get; set; } = string.Empty;
    public Guid SerialNumber { get; set; }
}
public class InventoryFilterRequest : BaseFilterRequest
{
    public int? ProductId { get; set; }
    public int? UnitId { get; set; }
    public DateTime? ProductionDate { get; set; }
    public DateTime? ExpireDate { get; set; }
    public string? ProductStatus { get; set; } = string.Empty;
    public Guid? SerialNumber { get; set; }
}