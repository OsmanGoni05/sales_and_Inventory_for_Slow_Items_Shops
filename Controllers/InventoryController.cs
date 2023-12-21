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

public class InventoryController : ControllerBase
{
    public readonly IMapper _mapper;
    public readonly ApplicationDbContext _context;
    public InventoryController(ApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    [HttpGet("GetById")]
    public IActionResult GetById(int id)
    {
        var find = _context.Inventories.Include(element => element.Product)
        .Include(element=> element.Unit)
        .Where(element => element.Id == id)
        .Select(element => new
        {
            element.Id,
            element.SerialNumber,
            element.ProductId,
            element.Product.ProductType.ProductName,
            element.UnitId,
            element.Unit.Name,
            element.SalesPricePerUnit,
            element.PerchesPricePerUnit,
            element.ProductionDate,
            element.ExpireDate,
            element.ProductStatus,
            element.CreatedAt,
            element.CreatedBy,
            element.UpdatedAt,
            element.UpdatedBy
        }).FirstOrDefault();

        return Ok(find);
    }

    [HttpGet("Filter")]
    public IActionResult Filter([FromQuery] InventoryFilterRequest request)
    {
        request.Page = request.Page == 0 ? 1 : request.Page;
        var query = _context.Inventories.AsQueryable();

         if (!request.ProductId.ToString().IsNullOrEmpty())
         {
           query= query.Where(element => element.ProductId == request.ProductId);
         }//if

         if (!request.UnitId.ToString().IsNullOrEmpty())
         {
           query= query.Where(element => element.UnitId == request.UnitId);
         }//if

         if (!request.ProductionDate.ToString().IsNullOrEmpty())
         {
           query= query.Where(element => element.ProductionDate == request.ProductionDate);
         }//if

         if (!request.ExpireDate.ToString().IsNullOrEmpty())
         {
           query= query.Where(element => element.ExpireDate == request.ExpireDate);
         }//if

         if (!request.ProductStatus!.ToString().IsNullOrEmpty())
         {
           query= query.Where(element => element.ProductStatus == request.ProductStatus);
         }//if

         if (!request.SerialNumber.ToString().IsNullOrEmpty())
         {
           query= query.Where(element => element.SerialNumber == request.SerialNumber);
         }//if

        List<dynamic> inventories = query
            .OrderByDescending(element => element.Id)
            .Skip((request.Page - 1) * request.Take)
            .Take(request.Take)
            .Select(element => new
        {
            element.Id,
            element.SerialNumber,
            element.ProductId,
            element.Product.ProductType.ProductName,
            element.UnitId,
            element.Unit.Name,
            element.SalesPricePerUnit,
            element.PerchesPricePerUnit,
            element.ProductionDate,
            element.ExpireDate,
            element.ProductStatus,
            element.CreatedAt,
            element.CreatedBy,
            element.UpdatedAt,
            element.UpdatedBy
        }).ToList<dynamic>();

        int count = query.Count();

        int totalPage = count <= request.Take ? 1 : (count / request.Take);

        var result = new BaseFilterResponse
        {
            Data = inventories,
            totalElements = count,
            Page = request.Page,
            Take = request.Take,
            TotalPage = totalPage
        };
        return Ok(result);
    }


    [HttpPost("Post")]
    public IActionResult Post(InventoryRequest inventoryRequest)
    {
        Inventory inventory = _mapper.Map<Inventory>(inventoryRequest);
        _context.Inventories.Add(inventory);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpPut("Put")]
    public IActionResult Put(int id, InventoryRequest inventoryRequest)
    {
        Inventory? inventory = _context.Inventories.Find(id);
        if (inventory is null) return BadRequest(ResponseMessage.NOT_FOUND);
        inventory = _mapper.Map(inventoryRequest, inventory);
        inventory.UpdatedAt = DateTime.UtcNow;
        inventory.UpdatedBy = 0;
        inventory.CreatedBy = 0;
        inventory.CreatedAt = DateTime.UtcNow;
        _context.Inventories.Update(inventory);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(int id)
    {
        var find = _context.Inventories.Find(id);
        _context.Inventories.Remove(find);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
}