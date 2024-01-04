namespace sales_and_Inventory_for_Slow_Items_Shops.Constants;

public class ProductStatusConstant
{
    public const string SOLD = "sold" ;
    public const string PURCHASE = "purchase";
    public const string DAMAGE = "damage";
    public static List<string> ProductStatues { get; set; } = new(){SOLD,PURCHASE, DAMAGE};
}
