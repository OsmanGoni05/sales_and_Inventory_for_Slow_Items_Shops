﻿namespace sales_and_Inventory_for_Slow_Items_Shops;

public class UserType
{
    public const string EMPLOYE = "employe" ;
    public const string CUSTOMER = "customer";
    public static List<string> UserTypes { get; set; } = new(){EMPLOYE,CUSTOMER};
}
