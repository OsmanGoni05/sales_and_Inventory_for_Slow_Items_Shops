using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;
using sales_and_Inventory_for_Slow_Items_Shops.data;
using sales_and_Inventory_for_Slow_Items_Shops.models;
namespace sales_and_Inventory_for_Slow_Items_Shops.Controllers;

[ApiController]
[Route("[controller]")]

public class ProductSaleController : ControllerBase
{
    public readonly IMapper _mapper;
    public readonly ApplicationDbContext _context;
    public ProductSaleController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet("GetById")]
    public IActionResult GetById(int id)
    {
        var find = _context.ProductSales.Find(id);
        ProductSaleResponse productSaleResponse = _mapper.Map<ProductSaleResponse>(find);
        return Ok(productSaleResponse);
    }
    [HttpGet("Get")]
    public IActionResult Get()
    {
        List<ProductSale> productSales = _context.ProductSales.ToList();
        List<ProductSaleResponse> productSaleResponses = _mapper.Map<List<ProductSaleResponse>>(productSales);
        return Ok(productSaleResponses);
    }
    [HttpPost("Post")]
    public IActionResult Post(ProductSaleRequest productSaleRequest)
    {
        ProductSale productSale = _mapper.Map<ProductSale>(productSaleRequest);
        _context.ProductSales.Add(productSale);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpPut("Put")]
    public IActionResult Put(int id, ProductSaleRequest productSaleRequest)
    {
        ProductSale? productSale = _context.ProductSales.Find(id);
        if(productSale is null) return BadRequest(ResponseMessage.NOT_FOUND);
        productSale = _mapper.Map(productSaleRequest, productSale);
        productSale.CreatedAt = DateTime.UtcNow;
        productSale.CreatedBy = 0;
        _context.ProductSales.Update(productSale);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(int id)
    {
        var find = _context.ProductSales.Find(id);
        _context.ProductSales.Remove(find);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);

    }
}