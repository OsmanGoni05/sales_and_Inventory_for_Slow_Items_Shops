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

public class InventorySummaryController : ControllerBase
{
    public readonly IMapper _mapper;
    public readonly ApplicationDbContext _context;
    public InventorySummaryController(ApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    [HttpGet("GetById")]
    public IActionResult GetById(int id)
    {
        var find = _context.InventorySummaries.Include(element => element.Product)
        .Where(element => element.Id == id)
        .Select(element => new
        {
            element.Id,
            element.ProductId,
            element.Product.ProductType.ProductName,
            element.ProductQuantity,
            element.TotalPrice,
            element.CreatedAt,
            element.CreatedBy,
            element.UpdatedAt,
            element.UpdatedBy
        }).FirstOrDefault();

        return Ok(find);
    }

    [HttpGet("Filter")]
    public IActionResult Filter([FromQuery] InventoryFilterSummaryRequest request)
    {
        request.Page = request.Page == 0 ? 1 : request.Page;
        var query = _context.InventorySummaries.AsQueryable();

         if (!request.ProductId.ToString().IsNullOrEmpty())
         {
           query= query.Where(element => element.ProductId == request.ProductId);
         }//if

        List<dynamic> elements = query
            .OrderByDescending(element => element.Id)
            .Skip((request.Page - 1) * request.Take)
            .Take(request.Take)
            .Select(element => new
        {
            element.Id,
            element.ProductId,
            element.Product.ProductType.ProductName,
            element.ProductQuantity,
            element.TotalPrice,
            element.CreatedAt,
            element.CreatedBy,
            element.UpdatedAt,
            element.UpdatedBy
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
    public IActionResult Post(InventorySummaryRequest request)
    {
        InventorySummary element = _mapper.Map<InventorySummary>(request);
        _context.InventorySummaries.Add(element);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
}