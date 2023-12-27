using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;

namespace sales_and_Inventory_for_Slow_Items_Shops.models;

public class Transaction : BaseModel
{
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;
    public int ReceiverId { get; set; }

    [ForeignKey(nameof(ReceiverId))]
    public User Receiver { get; set; } = null!;
    public int GiverId { get; set; }
    [ForeignKey(nameof(GiverId))]
    public User Giver { get; set; } = null!;
    public int? ParentTransactionId { get; set; }
    [ForeignKey(nameof(ParentTransactionId))]
    public Transaction? ParentTransaction { get; set; } = null!;
    public string Type { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
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
    public List<TransactionDetail> Details { get; set; } = new();

}
public class TransactionRequest
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "The field is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than zero.")]
    public int ReceiverId { get; set; }

    [Required(ErrorMessage = "The field is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than zero.")]
    public int GiverId { get; set; }
    public int? ParentTransactionId { get; set; }

    [RegularExpression($"^{TransactionType.SALE}|{TransactionType.PURCHASE}|{TransactionType.DAMAGE_BY_DISTROY}|{TransactionType.DAMAGE_BY_LOST}!")]
    public string Type { get; set; } = TransactionType.SALE;

    [RegularExpression($"^{TransactionState.PAID}|{TransactionState.ADVANCE}|{TransactionState.DUE}|{TransactionState.NON_RECOVERABLE}!")]
    public string State { get; set; } = TransactionState.PAID;
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
    public List<TransactionDetailRequest> Details { get; set; } = new();
}
public class TransactionResponse : BaseResponse
{
    public int ProductId { get; set; }
    public int ReceiverId { get; set; }
    public int GiverId { get; set; }
    public int? ParentTransactionId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
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

public class TransactionFilterRequest : BaseFilterRequest
{
    public int? ProductId { get; set; }
    public int? ReceiverId { get; set; }
    public int? GiverId { get; set; }
    public int? ParentTransactionId { get; set; }
    public string? Type { get; set; } = string.Empty;
    public string? State { get; set; } = string.Empty;
    public DateTime? Date { get; set; }
}