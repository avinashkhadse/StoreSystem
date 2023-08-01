using Microsoft.EntityFrameworkCore;
using StoreSystem.Models;
using StoreSystem.Models.Reports;
using System.Data;

namespace StoreSystem.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }
        public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        //public DbSet<AddBrand> Brand { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<MonthlyBrandWiseSalesReport> MonthlyBrandWiseSalesReports { get; set; }
        public DbSet<MonthlySalesReportItem> MonthlySalesReportItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mobile>()
            .HasOne<Brand>(s => s.Brand)
            .WithMany(g => g.Mobiles)
            .HasForeignKey(s => s.BrandId);

            modelBuilder.Entity<SaleItem>()
                .HasOne<Sales>(s => s.Sales)
                .WithMany(g => g.SaleItems)
                .HasForeignKey(s => s.SalesId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SaleItem>()
                .HasOne<Mobile>(s => s.Mobile)
                .WithMany(g => g.SaleItems)
                .HasForeignKey(s => s.MobileId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MonthlyBrandWiseSalesReport>()
                .HasNoKey();

            modelBuilder.Entity<MonthlySalesReportItem>()
                .HasNoKey();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=000.00.00.00;Database=Avinash_tem;User Id=AAA;password=BBB;MultipleActiveResultSets=True;Encrypt=False;connect timeout=1000", b => b.MigrationsAssembly("StoreSystem.Data"));
                optionsBuilder.UseSqlServer("Server=AVINASH_PATIL\\SQLEXPRESS01;Database=StoreSystemDb;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=False;connect timeout=1000;", b => b.MigrationsAssembly("StoreSystem.Data"));
                
            }
        }
    }
}