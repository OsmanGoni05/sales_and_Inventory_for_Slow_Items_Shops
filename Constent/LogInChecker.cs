//In the name of Allah

using sales_and_Inventory_for_Slow_Items_Shops.data;

namespace sales_and_Inventory_for_Slow_Items_Shops;

public class LogInChecker
{
    public static bool CheckLogIn(int id, ApplicationDbContext _context)
    {
        var user = _context.User.Find(id);
        if (user == null) return false;
        if (!user.IsLogedIn) return false;
        if(user.RoleName == Role.CUSTOMER) return false;

        return true;
    }//func

    internal static bool CheckLogIn(int userId, object context)
    {
        throw new NotImplementedException();
    }
}//class
