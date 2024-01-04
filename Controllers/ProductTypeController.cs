using System.ComponentModel.DataAnnotations;
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

public class ProductTypeController : ControllerBase
{
    public readonly IMapper _mapper;
    public readonly ApplicationDbContext _context;
    public ProductTypeController(ApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    [HttpGet("GetById")]
    public IActionResult GetById(int id, int userId)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        var find = _context.ProductTypes.Include(element => element.Products).FirstOrDefault(element => element.Id == id);
        ProductTypeResponse productResponse = _mapper.Map<ProductTypeResponse>(find);
        return Ok(productResponse);
    }
   
    [HttpGet("Option")]
    public IActionResult Option(int userId)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        List<dynamic> elements = _context.ProductTypes
            .Select(element => new 
            {
                element.Id,
                Name = element.ProductName
            }).ToList<dynamic>();
        return Ok(elements);
    }//func

    [HttpGet("Filter")]
    public IActionResult Filter([Required(ErrorMessage ="User Id is Required!")]int userId, [FromQuery] ProductTypeFilterRequest request)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        request.Page = request.Page == 0 ? 1 : request.Page;
        var query = _context.ProductTypes.AsQueryable();

        if (!request.ProductName.IsNullOrEmpty())
        {
            query.Where(element => element.ProductName == request.ProductName);
        }//if

        List<ProductType> productTypes = query
            .OrderByDescending(element => element.Id)
            .Skip((request.Page - 1) * request.Take)
            .Take(request.Take)
            .ToList();

        int count = query.Count();

        int totalPage = count <= request.Take ? 1 : (count / request.Take); 

        List<ProductTypeResponse> elements = _mapper.Map<List<ProductTypeResponse>>(productTypes);
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
    public IActionResult Post(int userId, ProductTypeRequest productTypeRequest)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        ProductType productType = _mapper.Map<ProductType>(productTypeRequest);
        _context.ProductTypes.Add(productType);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpPut("Put")]
    public IActionResult Put(int id, int userId, ProductTypeRequest productTypeRequest)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        ProductType? productType = _context.ProductTypes.Find(id);
        if (productType is null) return BadRequest(ResponseMessage.NOT_FOUND);
        productType = _mapper.Map(productTypeRequest, productType);
        productType.UpdatedAt = DateTime.UtcNow;
        productType.UpdatedBy = 0;
        productType.CreatedBy = 0;
        productType.CreatedAt = DateTime.UtcNow;
        _context.ProductTypes.Update(productType);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(int id, int userId)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        var find = _context.ProductTypes.Find(id);
        _context.ProductTypes.Remove(find);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
}