using AutoMapper;
using sales_and_Inventory_for_Slow_Items_Shops.models;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {

        //Source -> Target
        CreateMap<UserRequest, User>();
        CreateMap<User, UserResponse>();

        CreateMap<UnitRequest, Unit>();
        CreateMap<Unit, UnitResponse>();

        CreateMap<TransactionRequest, Transaction>();
        CreateMap<Transaction, TransactionResponse>();

        CreateMap<ProductTypeRequest, ProductType>();
        CreateMap<ProductType, ProductTypeResponse>();
        CreateMap<ProductType, ProductTypeForProductResponse>();

        CreateMap<InventorySummaryRequest, InventorySummary>();
        CreateMap<InventorySummary, InventorySummaryResponse>();

        CreateMap<InventoryRequest, Inventory>();
        CreateMap<Inventory, InventoryResponse>();

        CreateMap<ProductSaleRequest, ProductSale>();
        CreateMap<ProductSale, ProductSaleResponse>();

        CreateMap<ProductPerchesRequest, ProductPerches>();
        CreateMap<ProductPerches, ProductPerchesResponse>();

        CreateMap<ProductRequest, Product>();
        CreateMap<Product, ProductResponse>();
    }
}

internal class ProductTypeForProductResponse
{
}