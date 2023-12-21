using Microsoft.EntityFrameworkCore;
using sales_and_Inventory_for_Slow_Items_Shops.models;
namespace sales_and_Inventory_for_Slow_Items_Shops.data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } //Constructor

    public DbSet<ProductType> ProductTypes => Set<ProductType>();
    public DbSet<User> User => Set<User>();
    public DbSet<Inventory> Inventories => Set<Inventory>();
    public DbSet<InventorySummary> InventorySummaries => Set<InventorySummary>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<ProductSale> ProductSales => Set<ProductSale>();
    public DbSet<ProductPerches> ProductPerchess => Set<ProductPerches>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Receiver)
            .WithMany()
            .HasForeignKey(t => t.ReceiverId)
            .OnDelete(DeleteBehavior.NoAction);
    }//func


}