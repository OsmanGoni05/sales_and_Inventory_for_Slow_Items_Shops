using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;
using sales_and_Inventory_for_Slow_Items_Shops.data;
using sales_and_Inventory_for_Slow_Items_Shops.models;

namespace sales_and_Inventory_for_Slow_Items_Shops.Controllers;

[ApiController]
[Route("[controller]")]
public class UnitController : ControllerBase
{
    private readonly IMapper _mapper;
    public readonly ApplicationDbContext _context;
    public UnitController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
     [HttpGet("GetById")]
    public IActionResult GetById(int id)
    {
        var find = _context.Units.Find(id);
        UnitResponse response = _mapper.Map<UnitResponse>(find);
        return Ok(response);
    }
    [HttpGet("Option")]
    public IActionResult Option()
    {
        List<dynamic> elements = _context.Units
            .Select(element => new 
            {
                element.Id,
                Name = element.Name
            }).ToList<dynamic>();
        return Ok(elements);
    }//func

     [HttpGet("Filter")]
    public IActionResult Filter([FromQuery] UnitFilterRequest request)
    {
        var query = _context.Units.AsQueryable();

        if (!request.Name.IsNullOrEmpty())
        {
            query.Where(element => element.Name == request.Name);
        }//if

        List<Unit> units  = query
            .OrderByDescending(element => element.Id)
            .Skip((request.Page - 1) * request.Take)
            .Take(request.Take)
            .ToList();

        int count = query.Count();

        int totalPage = count <= request.Take ? 1 : (count / request.Take); 

        List<UnitResponse> elements = _mapper.Map<List<UnitResponse>>(units);
        var result = new BaseFilterResponse
        {
            Data = elements,
            totalElements = count,
            Page = request.Page,
            Take = request.Take,
            TotalPage = totalPage
        };
        return Ok(result);
    }
    [HttpGet("Get")]
    public IActionResult Get()
    {
        List<Unit> units = _context.Units.ToList();
        List<UnitResponse> response = _mapper.Map<List<UnitResponse>>(units);
        return Ok(response);
    }
    [HttpPost("Post")]
    public IActionResult Post(UnitRequest unitRequest)
    {
        Unit unit = _mapper.Map<Unit>(unitRequest);
        _context.Units.Add(unit);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpPut("Put")]
    public IActionResult Update(int id, UnitRequest unitRequest)
    {
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
    public IActionResult Delete(int id)
    {
        var find = _context.Units.Find(id);
        _context.Units.Remove(find);
        var result = _context.SaveChanges();
        return Ok(result);
    }
}