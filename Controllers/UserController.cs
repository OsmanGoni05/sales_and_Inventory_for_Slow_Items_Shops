using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;
using sales_and_Inventory_for_Slow_Items_Shops.data;
using sales_and_Inventory_for_Slow_Items_Shops.models;

namespace sales_and_Inventory_for_Slow_Items_Shops.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    public readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context,  IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("Option")]
    public IActionResult Option(int userId)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        List<dynamic> elements = _context.User
            .Select(element => new 
            {
                element.Id,
                Name = element.FirstName + element.LastName
            }).ToList<dynamic>();
        return Ok(elements);
    }//func
     [HttpGet("Filter")]
    public IActionResult Filter([FromQuery] UserFilterRequest request)
    {
        var query = _context.User.AsQueryable();

        if (!request.FirstName.IsNullOrEmpty())
        {
           query = query.Where(element => element.FirstName == request.FirstName);
        }//if

      List<dynamic> users = query
            .OrderByDescending(element => element.Id)
            .Skip((request.Page - 1) * request.Take)
            .Take(request.Take)
            .Select(element => new
        {
            element.Id,
            Name = element.FirstName + " " + element.LastName,
            element.RoleName,
            element.MobileNumber,
            element.IsLogedIn,
            element.CreatedAt,
            element.CreatedBy,
            element.UpdatedAt,
            element.UpdatedBy
        }).ToList<dynamic>();

        int count = query.Count();

        int totalPage = count <= request.Take ? 1 : (count / request.Take); 

        var result = new BaseFilterResponse
        {
            Data = users,
            totalElements = count,
            Page = request.Page,
            Take = request.Take,
            TotalPage = totalPage
        };
        return Ok(result);
    }
    
    [HttpPost("Post")]
    public IActionResult Post(UserRequest userRequest)
    {
        User user = _mapper.Map<User>(userRequest);

        _context.User.Add(user);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpPut("Update")]
    public IActionResult Put(int id, UserRequest userRequest)
    {

        User? user = _context.User.Find(id);
        if (user is null) return BadRequest(ResponseMessage.NOT_FOUND);
        user = _mapper.Map(userRequest, user);
        user.UpdatedAt = DateTime.UtcNow;
        user.UpdatedBy = 0;
        _context.User.Update(user);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(int id)
    {
        var find = _context.User.Find(id);
        _context.User.Remove(find);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }

}