using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace sales_and_Inventory_for_Slow_Items_Shops.models;

public class Product : BaseModel
{
    public int ProductTypeId { get; set; }

    [ForeignKey(nameof(ProductTypeId))]
    public ProductType ProductType { get; set; } = null!;    
    //add unit
    public int UnitId {get; set;}
    [ForeignKey(nameof(UnitId))]
    public Unit Unit {get; set;} = null!;
    //add description
    public string Description { get; set; } = null!;
    //add quality
    public int QualityId {get; set;}
    [ForeignKey(nameof(QualityId))]
    public Quality Quality {get; set;} = null!;
    //add brand
    public int BrandId {get; set;}
    [ForeignKey(nameof(BrandId))]
    public Brand Brand {get; set;} = null!;
    public double SalePrice { get; set; }
    public double PurchasePrice { get; set; }
}

public class ProductRequest
{

    [Required]
    public int ProductTypeId { get; set; }
    public double SalePrice { get; set; }
    public double PurchasePrice { get; set; }
    public int UnitId { get; set; }
    public int QualityId {get; set;}
    public int BrandId {get; set;}
    public string Description {get; set;} = null!;
}

public class ProductResponse : BaseResponse
{
    public int ProductTypeId { get; set; }
    public int ProductType { get; set; }
    public int UnitId {get; set;}
    public string Description {get; set;} = string.Empty;
    public int QualityId {get; set;}
    public int BrandId {get; set;}
}

public class ProductFilterRequest : BaseFilterRequest
{
    public int? ProductTypeId { get; set; }
    public int? UnitId { get; set; }
    public int? QualityId {get; set;}
    public int? BrandId {get; set;}
}