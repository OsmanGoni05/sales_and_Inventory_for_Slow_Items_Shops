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
        var find = _context.Transactions.Include(element => element.Product)
        .Include(element => element.Receiver)
        .Include(element => element.Giver)
        .Include(element => element.ParentTransaction)
        .Where(element => element.Id == id)
        .Select(element => new
        {
            element.Id,
            element.Type,
            element.State,
            element.Product.ProductType.ProductName,
            element.ReceiverId,
            ReciverName = element.Receiver.FirstName + " "  +element.Receiver.LastName,
            element.GiverId,
            GiverName = element.Giver.FirstName + " "  +element.Giver.LastName,
            element.Quantity,
            element.MoneyReceived,
            element.MoneyPaid,
            element.Bank,
            element.Cash,
            element.MFS,
            element.Due,
            element.Date,
            element.AdvancePayment,
            element.CreatedAt,
            element.CreatedBy,
            element.UpdatedAt,
            element.UpdatedBy
        }).FirstOrDefault();

        return Ok(find);
    }

    [HttpGet("Filter")]
    public IActionResult Filter([FromQuery] TransactionFilterRequest request)
    {
        request.Page = request.Page == 0 ? 1 : request.Page;
        var query = _context.Transactions.AsQueryable();

         if (!request.ProductId.ToString().IsNullOrEmpty())
         {
            query = query.Where(element => element.ProductId == request.ProductId);
         }//if
         if (!request.ReceiverId.ToString().IsNullOrEmpty())
         {
            query = query.Where(element => element.ReceiverId == request.ReceiverId);
         }//if
         if (!request.GiverId.ToString().IsNullOrEmpty())
         {
            query = query.Where(element => element.GiverId == request.GiverId);
         }//if
         if (!request.ParentTransactionId.ToString().IsNullOrEmpty())
         {
            query = query.Where(element => element.ParentTransactionId == request.ParentTransactionId);
         }//if
         if (!request.Date.ToString().IsNullOrEmpty())
         {
            query = query.Where(element => element.Date == request.Date);
         }//if
         if (!request.Type!.ToString().IsNullOrEmpty())
         {
            query = query.Where(element => element.Type == request.Type);
         }//if
         if (!request.State!.ToString().IsNullOrEmpty())
         {
            query = query.Where(element => element.State == request.State);
         }//if
    

        List<dynamic> elements = query
            .OrderByDescending(element => element.Product)
            .Skip((request.Page - 1) * request.Take)
            .Take(request.Take)
            .Select(element => new
        {
            element.Id,
            element.Type,
            element.State,
            element.Product.ProductType.ProductName,
            element.ReceiverId,
            ReciverName = element.Receiver.FirstName + " "  +element.Receiver.LastName,
            element.GiverId,
            GiverName = element.Giver.FirstName + " "  +element.Giver.LastName,
            element.Quantity,
            element.MoneyReceived,
            element.MoneyPaid,
            element.Bank,
            element.Cash,
            element.MFS,
            element.Due,
            element.Date,
            element.AdvancePayment,
            element.CreatedAt,
            element.CreatedBy,
            element.UpdatedAt,
            element.UpdatedBy
        }).ToList<dynamic>();

        int count = query.Count();

        int totalPage = count <= request.Take ? 1 : (count / request.Take);

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
    public IActionResult Post(TransactionRequest request)
    {
        Transaction transaction = _mapper.Map<Transaction>(request);
        _context.Transactions.Add(transaction);
        var result = _context.SaveChanges();
        return Ok(ResponseMessage.SUCCESS_MESSAGE);
    }
}