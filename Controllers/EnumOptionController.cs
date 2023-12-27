//In the name of Allah

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;
using sales_and_Inventory_for_Slow_Items_Shops.data;
using sales_and_Inventory_for_Slow_Items_Shops.models;

namespace sales_and_Inventory_for_Slow_Items_Shops.Controllers;

[ApiController]
[Route("[controller]")]

public class EnumOptionController : ControllerBase
{
    public readonly IMapper _mapper;
    public readonly ApplicationDbContext _context;
    public EnumOptionController(ApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }//constructor
    
    [HttpGet("UserTypeOption")]
    public IActionResult UserTypeOption() => Ok(UserType.UserTypes);

     [HttpGet("TransactionTypesOption")]
    public IActionResult TransactionTypesOption() => Ok(TransactionType.TransactionTypes);

    [HttpGet("TransactionStatesOption")]
    public IActionResult TransactionStatesOption() => Ok(TransactionState.TransactionStates);

    [HttpGet("RolesOption")]
    public IActionResult RolesOption() => Ok(Role.Roles);

    [HttpGet("ProductStatusOption")]
    public IActionResult ProductStatusOption() => Ok(ProductStatus.ProductStatues);

}//class