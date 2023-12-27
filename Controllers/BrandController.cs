using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;
using sales_and_Inventory_for_Slow_Items_Shops.data;
using sales_and_Inventory_for_Slow_Items_Shops.models;

namespace sales_and_Inventory_for_Slow_Items_Shops.Controllers;

[ApiController]
[Route("[controller]")]
public class BrandController : ControllerBase
{
    private readonly IMapper _mapper;
    public readonly ApplicationDbContext _context;
    public BrandController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
     [HttpGet("GetById")]
    public IActionResult GetById(int id)
    {
        var find = _context.Brands.Find(id);
        BrandResponse response = _mapper.Map<BrandResponse>(find);
        return Ok(response);
    }
    [HttpGet("Option")]
    public IActionResult Option()
    {
        List<dynamic> elements = _context.Brands
            .Select(element => new 
            {
                element.Id,
                Name = element.Name
            }).ToList<dynamic>();
        return Ok(elements);
    }//func

     [HttpGet("Filter")]
    public IActionResult Filter(int userId, [FromQuery] BrandFilterRequest request)
    {
        
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        var query = _context.Brands.AsQueryable();

        if (!request.Name.IsNullOrEmpty())
        {
            query = query.Where(element => element.Name == request.Name);
        }//if

        List<Brand> brand  = query
            .OrderByDescending(element => element.Id)
            .Skip((request.Page - 1) * request.Take)
            .Take(request.Take)
            .ToList();

        int count = query.Count();

        int totalPage = count <= request.Take ? 1 : (count / request.Take); 

        List<BrandResponse> elements = _mapper.Map<List<BrandResponse>>(brand);
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
     [HttpPost("Post")]
    public IActionResult Post(int userId, BrandRequest request)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        Brand brand = _mapper.Map<Brand>(request);
        _context.Brands.Add(brand);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpPut("Put")]
    public IActionResult Update(int id, int userId, BrandRequest request)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        Brand? brand = _context.Brands.Find(id);
        if(brand is null) return BadRequest(ResponseMessage.NOT_FOUND);
        brand = _mapper.Map(request, brand);
        brand.UpdatedAt = DateTime.UtcNow;
        brand.UpdatedBy = 0;
        _context.Brands.Update(brand);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(int id, int userId)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        var find = _context.Brands.Find(id);
        _context.Brands.Remove(find);
        var result = _context.SaveChanges();
        return Ok(result);
    }
}