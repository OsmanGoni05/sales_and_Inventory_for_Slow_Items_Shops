using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;
using sales_and_Inventory_for_Slow_Items_Shops.data;
using sales_and_Inventory_for_Slow_Items_Shops.models;

namespace sales_and_Inventory_for_Slow_Items_Shops.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    public readonly ApplicationDbContext _contex;
    private object _context;

    public UserController(ApplicationDbContext context,  IMapper mapper)
    {
        _contex = context;
        _mapper = mapper;
    }
    [HttpGet("GetById")]
    public IActionResult GetById(int id)
    {
        var find = _contex.User.Find(id);

        UserResponse response = _mapper.Map<UserResponse>(find);
        return Ok(response);
    }

    [HttpGet("Option")]
    public IActionResult Option()
    {
        List<dynamic> elements = _contex.User
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
        var query = _contex.User.AsQueryable();

        //if (!request.FirstName.IsNullOrEmpty())
        //{
          //  query.Where(element => element.FirstName == request.FirstName);
        //}//if

        List<User> users = query
            .OrderByDescending(element => element.Id)
            .Skip((request.Page - 1) * request.Take)
            .Take(request.Take)
            .ToList();

        int count = query.Count();

        int totalPage = count <= request.Take ? 1 : (count / request.Take); 

        List<UserResponse> elements = _mapper.Map<List<UserResponse>>(users);
        var result = new BaseFilterResponse
        {
            Data = elements,
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

        _contex.User.Add(user);
        var result = _contex.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpPut("Update")]
    public IActionResult Put(int id, UserRequest userRequest)
    {

        User? user = _contex.User.Find(id);
        if (user is null) return BadRequest(ResponseMessage.NOT_FOUND);
        user = _mapper.Map(userRequest, user);
        user.UpdatedAt = DateTime.UtcNow;
        user.UpdatedBy = 0;
        _contex.User.Update(user);
        var result = _contex.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(int id)
    {
        var find = _contex.User.Find(id);
        _contex.User.Remove(find);
        var result = _contex.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }

}