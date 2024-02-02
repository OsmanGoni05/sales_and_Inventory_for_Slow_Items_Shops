namespace sales_and_Inventory_for_Slow_Items_Shops;

public class Role
{
    public const string CUSTOMER = "custoemr";
    public const string STUFF = "stuff";
    public const string OWNER = "owner";
    public static List<string> Roles { get; set; } = new(){STUFF,OWNER, CUSTOMER};

}
