using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;
using sales_and_Inventory_for_Slow_Items_Shops.data;
using sales_and_Inventory_for_Slow_Items_Shops.models;

namespace sales_and_Inventory_for_Slow_Items_Shops.Controllers;

[ApiController]
[Route("[controller]")]

public class InventroySummaryController : ControllerBase
{
    public readonly ApplicationDbContext _context;
    public readonly IMapper _mapper;
    public InventroySummaryController(ApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    [HttpGet("GetById")]
    public IActionResult GetById(int id)
    {
        var find = _context.InventorySummaries.Find(id);
        InventorySummary inventorySummary = _mapper.Map<InventorySummary>(find);
        return Ok(inventorySummary);
    }
    [HttpGet("Get")]
    public IActionResult Get()
    {
        List<InventorySummary> inventorySummaries = _context.InventorySummaries.ToList();
        List<InventorySummaryResponse> inventoryResponses = _mapper.Map<List<InventorySummaryResponse>>(inventorySummaries);
        return Ok(inventoryResponses);
    }
    [HttpPost("Post")]
    public IActionResult Post(InventorySummaryRequest inventorySummaryRequest)
    {
        InventorySummary inventorySummary = _mapper.Map<InventorySummary>(inventorySummaryRequest);
        _context.InventorySummaries.Add(inventorySummary);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpPut("Put")]
    public IActionResult Update(int id, InventorySummaryRequest inventorySummaryRequest)
    {
        InventorySummary? inventorySummary = _context.InventorySummaries.Find(id);
        if(inventorySummary is null) return BadRequest(ResponseMessage.NOT_FOUND);
        inventorySummary = _mapper.Map(inventorySummaryRequest, inventorySummary);
        inventorySummary.UpdatedAt = DateTime.UtcNow;
        inventorySummary.UpdatedBy = 0;
        _context.InventorySummaries.Update(inventorySummary);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(int id)
    {
        var find = _context.InventorySummaries.Find(id);
        _context.InventorySummaries.Remove(find);
        var result = _context.SaveChanges();
        return Ok(result);
    }
}