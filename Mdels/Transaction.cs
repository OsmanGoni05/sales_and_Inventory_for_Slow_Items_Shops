using Azure;

namespace sales_and_Inventory_for_Slow_Items_Shops.models;

public class Transaction : BaseModel
{
    public int ProductId { get; set; }
    public int ReceiverId { get; set; }
    public int GiverId { get; set; }
    public int ParentTransactionId { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public double MoneyReceived { get; set; }
    public double MoneyPaid { get; set; }
    public double Due { get; set; }
    public double AdvancePayment { get; set; }
    public double Bank { get; set; }
    public double MFS { get; set; }
    public double Cash { get; set; }
    public DateTime Date { get; set; }
}
public class TransactionRequest
{
    public int ProductId { get; set; }
    public int ReceiverId { get; set; }
    public int GiverId { get; set; }
    public int ParentTransactionId { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public double MoneyReceived { get; set; }
    public double MoneyPaid { get; set; }
    public double Due { get; set; }
    public double AdvancePayment { get; set; }
    public double Bank { get; set; }
    public double MFS { get; set; }
    public double Cash { get; set; }
    public DateTime Date { get; set; }
}
public class TransactionResponse : BaseResponse
{
    public int ProductId { get; set; }
    public int ReceiverId { get; set; }
    public int GiverId { get; set; }
    public int ParentTransactionId { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public double MoneyReceived { get; set; }
    public double MoneyPaid { get; set; }
    public double Due { get; set; }
    public double AdvancePayment { get; set; }
    public double Bank { get; set; }
    public double MFS { get; set; }
    public double Cash { get; set; }
    public DateTime Date { get; set; }
}