namespace sales_and_Inventory_for_Slow_Items_Shops.Constants;

public class TransactionType
{
    public const string SALE = "sale" ;
    public const string PURCHASE = "purchase" ;
    public const string DAMAGE_BY_DISTROY = "dameage_by_destroy" ;
    public const string DAMAGE_BY_LOST = "damage_by_lost" ;
    public static List<string> TransactionTypes { get; set; } = new(){SALE,PURCHASE,DAMAGE_BY_DISTROY,DAMAGE_BY_LOST};
}//class