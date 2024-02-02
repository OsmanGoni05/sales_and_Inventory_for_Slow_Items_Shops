using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;
using sales_and_Inventory_for_Slow_Items_Shops.data;
using sales_and_Inventory_for_Slow_Items_Shops.models;

namespace sales_and_Inventory_for_Slow_Items_Shops.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionDetailController : ControllerBase
{
    private readonly IMapper _mapper;
    public readonly ApplicationDbContext _context;
    public TransactionDetailController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
     [HttpGet("GetById")]
    public IActionResult GetById(int id, int userId)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        var find = _context.TransactionDetails.Find(id);
        TransactionDetailResponse response = _mapper.Map<TransactionDetailResponse>(find);
        return Ok(response);
    }
    [HttpGet("Option")]
    public IActionResult Option(int userId)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        List<dynamic> elements = _context.Transactions
            .Select(element => new 
            {
                element.Id,
                Name = element.Id
            }).ToList<dynamic>();
        return Ok(elements);
    }//func

      [HttpGet("Filter")]
    public IActionResult Filter(int userId, [FromQuery] UnitFilterRequest request)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        request.Page = request.Page == 0 ? 1 : request.Page;
        var query = _context.Units.AsQueryable();

        // if (!request.ProductName.IsNullOrEmpty())
        // {
        //     query.Where(element => element.ProductName == request.ProductName);
        // }//if

        List<Unit> unit = query
            .OrderByDescending(element => element.Id)
            .Skip((request.Page - 1) * request.Take)
            .Take(request.Take)
            .ToList();

        int count = query.Count();

        int totalPage = (int)(count <= request.Take ? 1 : Math.Ceiling(count / (double)request.Take));

        List<UnitResponse> elements = _mapper.Map<List<UnitResponse>>(unit);
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
    public IActionResult Post(int userId, UnitRequest unitRequest)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        Unit unit = _mapper.Map<Unit>(unitRequest);
        _context.Units.Add(unit);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpPut("Put")]
    public IActionResult Update(int id, int userId, UnitRequest unitRequest)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        Unit? unit = _context.Units.Find(id);
        if(unit is null) return BadRequest(ResponseMessage.NOT_FOUND);
        unit = _mapper.Map(unitRequest, unit);
        unit.UpdatedAt = DateTime.UtcNow;
        unit.UpdatedBy = 0;
        _context.Units.Update(unit);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(int id, int userId)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        var find = _context.Units.Find(id);
        _context.Units.Remove(find);
        var result = _context.SaveChanges();
        return Ok(result);
    }
}