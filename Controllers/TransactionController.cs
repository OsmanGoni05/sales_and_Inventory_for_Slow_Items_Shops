using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;
using sales_and_Inventory_for_Slow_Items_Shops.data;
using sales_and_Inventory_for_Slow_Items_Shops.models;

namespace sales_and_Inventory_for_Slow_Items_Shops.Controllers;

[ApiController]
[Route("[controller]")]

public class TransactionController : ControllerBase
{
    public readonly IMapper _mapper;
    public readonly ApplicationDbContext _context;
    public TransactionController(ApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    [HttpGet("GetById")]
    public IActionResult GetById(int id)
    {
        var find = _context.Transactions.Find(id);
        TransactionResponse response = _mapper.Map<TransactionResponse>(find);
        return Ok(response);

    }
    [HttpGet("Get")]
    public IActionResult Get()
    {
        List<Transaction> transactions = _context.Transactions.ToList();
        List<TransactionResponse> responses = _mapper.Map<List<TransactionResponse>>(transactions);
        return Ok(responses);
    }
    [HttpPost("Post")]
    public IActionResult Post(TransactionRequest transactionRequest)
    {
       Transaction transaction = _mapper.Map<Transaction>(transactionRequest);
        _context.Transactions.Add(transaction);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpPut("Put")]
    public IActionResult Update(int id, TransactionRequest transactionRequest)
    {
         Transaction? transaction = _context.Transactions.Find(id);
        if(transaction is null) return BadRequest(ResponseMessage.NOT_FOUND);
        transaction = _mapper.Map(transactionRequest, transaction);
        transaction.UpdatedAt = DateTime.UtcNow;
        transaction.UpdatedBy = 0;
        _context.Transactions.Update(transaction);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(int id)
    {
        var find = _context.Transactions.Find(id);
        _context.Transactions.Remove(find);
        var result = _context.SaveChanges();
        return Ok(result);
    }
}