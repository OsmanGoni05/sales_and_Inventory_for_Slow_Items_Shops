using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;
using sales_and_Inventory_for_Slow_Items_Shops.data;
using sales_and_Inventory_for_Slow_Items_Shops.models;

namespace sales_and_Inventory_for_Slow_Items_Shops.Controllers;

[ApiController]
[Route("[controller]")]
public class QuaityTypeController : ControllerBase
{
    private readonly IMapper _mapper;
    public readonly ApplicationDbContext _context;
    public QuaityTypeController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
     [HttpGet("GetById")]
    public IActionResult GetById(int id)
    {
        var find = _context.Qualities.Find(id);
        QualityResponse response = _mapper.Map<QualityResponse>(find);
        return Ok(response);
    }
    [HttpGet("Option")]
    public IActionResult Option()
    {
        List<dynamic> elements = _context.Qualities
            .Select(element => new 
            {
                element.Id,
                element = element.Name
            }).ToList<dynamic>();
        return Ok(elements);
    }//func

     [HttpGet("Filter")]
    public IActionResult Filter([FromQuery] QualityFilterRequest request)
    {
        var query = _context.Qualities.AsQueryable();

        if (!request.Name.IsNullOrEmpty())
        {
            query = query.Where(element => element.Name == request.Name);
        }//if

        List<Quality> quality  = query
            .OrderByDescending(element => element.Id)
            .Skip((request.Page - 1) * request.Take)
            .Take(request.Take)
            .ToList();

        int count = query.Count();

        int totalPage = count <= request.Take ? 1 : (count / request.Take); 

        List<QualityResponse> elements = _mapper.Map<List<QualityResponse>>(quality);
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
    public IActionResult Post(int userId, QualityRequest request)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        Quality quality = _mapper.Map<Quality>(request);
        _context.Qualities.Add(quality);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpPut("Put")]
    public IActionResult Update(int id, int userId, QualityRequest request)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        Quality? quality = _context.Qualities.Find(id);
        if(quality is null) return BadRequest(ResponseMessage.NOT_FOUND);
        quality = _mapper.Map(request, quality);
        quality.UpdatedAt = DateTime.UtcNow;
        quality.UpdatedBy = 0;
        _context.Qualities.Update(quality);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(int id)
    {
        // bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        // if(!IsAuthorized) return BadRequest("Unauthorized!");
        var find = _context.Qualities.Find(id);
        _context.Qualities.Remove(find);
        var result = _context.SaveChanges();
        return Ok(result);
    }
}