using System.Linq.Expressions;
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

public class ProductController : ControllerBase
{
    public readonly IMapper _mapper;
    public readonly ApplicationDbContext _context;
    public ProductController(ApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    [HttpGet("GetById")]
    public IActionResult GetById(int id, int userId)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        var find = _context.Products.Include(element => element.ProductType)
        .Where(element => element.Id == id)
        .Select(element => new
        {
            element.Id,
            element.ProductType.ProductName,
            element.Unit.Name,
            element.Brand,
            element.Description,
            element.CreatedAt,
            element.CreatedBy,
            element.UpdatedAt,
            element.UpdatedBy
        }).FirstOrDefault();

        return Ok(find);
    }

    [HttpGet("Option")]
    public IActionResult Option(int userId)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        List<dynamic> elements = _context.Products.Include(element => element.ProductType)
    .Select(element => new
    {
        element.Id,
        element.ProductType.ProductName,
        element.BrandId
    }).ToList<dynamic>();
        return Ok(elements);
    }//func

    [HttpGet("Filter")]
    public IActionResult Filter(int userId, [FromQuery] ProductTypeFilterRequest request)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        request.Page = request.Page == 0 ? 1 : request.Page;
        var query = _context.Products.AsQueryable();

        if (!request.ProductName.IsNullOrEmpty())
        {
            query = query.Where(element => element.ProductType.ProductName == request.ProductName);
        }//if

        List<dynamic> elements = query
            .OrderByDescending(element => element.Id)
            .Skip((request.Page - 1) * request.Take)
            .Take(request.Take)
             .Select(element => new
        {
            element.Id,
            element.ProductType.ProductName,
        }).ToList<dynamic>();

        int count = query.Count();

        int totalPage = count <= request.Take ? 1 : (count / (request.Take == 0 ? 1 : request.Take));
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
    public IActionResult Post(int userId, ProductRequest productRequest)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        Product product = _mapper.Map<Product>(productRequest);
        _context.Products.Add(product);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpPut("Put")]
    public IActionResult Put(int id, int userId, ProductRequest productRequest)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        Product? product = _context.Products.Find(id);
        if (product is null) return BadRequest(ResponseMessage.NOT_FOUND);
        product = _mapper.Map(productRequest, product);
        product.UpdatedAt = DateTime.UtcNow;
        product.UpdatedBy = 0;
        product.CreatedBy = 0;
        product.CreatedAt = DateTime.UtcNow;
        _context.Products.Update(product);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(int id, int userId)
    {
        bool IsAuthorized = LogInChecker.CheckLogIn(userId,_context);
        if(!IsAuthorized) return BadRequest("Unauthorized!");
        var find = _context.Products.Find(id);
        _context.Products.Remove(find);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
}