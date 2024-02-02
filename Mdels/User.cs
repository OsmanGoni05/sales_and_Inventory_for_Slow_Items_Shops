//In the name of Allah

using System.ComponentModel.DataAnnotations;

namespace sales_and_Inventory_for_Slow_Items_Shops.models;

public class User : BaseModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public bool IsLogedIn {get; set;}
    public string RoleName { get; set; } = string.Empty;
}

public class UserRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;

}

public class UserResponse : BaseResponse
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
}

public class UserFilterRequest  : BaseFilterRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
}

public class RegistrationRequest
{
    [Required]
    public string MobileNumber { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}