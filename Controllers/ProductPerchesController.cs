using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;
using sales_and_Inventory_for_Slow_Items_Shops.data;
using sales_and_Inventory_for_Slow_Items_Shops.models;

namespace sales_and_Inventory_for_Slow_Items_Shops.Controllers;
[ApiController]
[Route("[controller]")]

public class ProductPerchesController : ControllerBase
{
    public readonly IMapper _mapper;
    public readonly ApplicationDbContext _context;
    public ProductPerchesController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet("GetById")]
    public IActionResult GetById(int id)
    {
        var find = _context.ProductPerchess.Find(id);
        ProductPerchesResponse productPerches = _mapper.Map<ProductPerchesResponse>(find);
        return Ok(productPerches);
    }
    [HttpGet("Get")]
    public IActionResult Get()
    {
        List<ProductPerches> productPerches = _context.ProductPerchess.ToList();
        List<ProductPerchesResponse> responses = _mapper.Map<List<ProductPerchesResponse>>(productPerches);
        return Ok(responses);
    }
    [HttpPost("Post")]
    public IActionResult Post(ProductPerchesRequest productPerchesRequest)
    {
        ProductPerches productPerches = _mapper.Map<ProductPerches>(productPerchesRequest);
        _context.ProductPerchess.Add(productPerches);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpPut("Put")]
    public IActionResult Put(int id, ProductPerchesRequest productPerchesRequest)
    {
        ProductPerches? productPerches = _context.ProductPerchess.Find(id);
        if(productPerches is null) return BadRequest(ResponseMessage.NOT_FOUND);
        productPerches = _mapper.Map(productPerchesRequest, productPerches);
        productPerches.CreatedAt = DateTime.UtcNow;
        productPerches.CreatedBy = 0;
        _context.ProductPerchess.Update(productPerches);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(int id)
    {
        var find = _context.ProductPerchess.Find(id);
        _context.ProductPerchess.Remove(find);
        var result = _context.SaveChanges();
        return Ok(result);
    }
}