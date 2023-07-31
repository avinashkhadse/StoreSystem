using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using StoreSystem.Models.Reports;

public class SalesManager
{
    private readonly StoreDbContext _dbContext;

    public SalesManager(StoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Sales> CreateSalesAsync(Sales sales)
    {
        _dbContext.Sales.Add(sales);
        await _dbContext.SaveChangesAsync();
        return sales;
    }

    public async Task<Sales> GetSalesByIdAsync(int id)
    {
        return await _dbContext.Sales.FindAsync(id);
    }

    public async Task<List<Sales>> GetAllSalesAsync()
    {
        return await _dbContext.Sales.ToListAsync();
    }

    public async Task<Sales> UpdateSalesAsync(Sales updatedSales)
    {
        var existingSales = await _dbContext.Sales.FindAsync(updatedSales.Id);

        if (existingSales == null)
            return null;

        existingSales.MobileId = updatedSales.MobileId;
        existingSales.Quantity = updatedSales.Quantity;
        existingSales.Total = updatedSales.Total;
        existingSales.Date = updatedSales.Date;

        await _dbContext.SaveChangesAsync();
        return existingSales;
    }

    public async Task<bool> DeleteSalesAsync(int id)
    {
        var sales = await _dbContext.Sales.FindAsync(id);

        if (sales == null)
            return false;

        _dbContext.Sales.Remove(sales);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Sales>> GetSalesByMobileIdAsync(int mobileId)
    {
        return await _dbContext.Sales
            .Where(s => s.MobileId == mobileId)
            .ToListAsync();
    }

    public async Task<List<Sales>> GetSalesByBrandIdAsync(int brandId)
    {
        return await _dbContext.Sales
            .Where(s => s.Mobile.BrandId == brandId)
            .ToListAsync();
    }

    public async Task<List<Sales>> GetSalesByUserAsync(int userId)
    {
        return await _dbContext.Sales
            .Where(s => s.User.Id == userId)
            .ToListAsync();
    }

    // ProfitLossReport
    public async Task<decimal> GetTotalSales(DateTime fromDate, DateTime toDate)
    {
        return await _dbContext.Sales
            .Where(s => s.Date >= fromDate && s.Date <= toDate)
            .SumAsync(s => s.Total);
    }

    public async Task<decimal> GetTotalProfitLoss(DateTime fromDate, DateTime toDate)
    {
        return await _dbContext.Sales
            .Where(s => s.Date >= fromDate && s.Date <= toDate)
            .SumAsync(s => s.Total - (s.Mobile.Price * s.Quantity));
    }

    public async Task<List<ProfitLossReportItem>> GetProfitLossReportAsync(DateTime currentFromDate, DateTime currentToDate, DateTime previousFromDate, DateTime previousToDate)
    {
        var currentReport = await _dbContext.Sales
            .Where(s => s.Date >= currentFromDate && s.Date <= currentToDate)
            .GroupBy(s => new { Year = s.Date.Year, Month = s.Date.Month })
            .Select(g => new ProfitLossReportItem
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                ProfitLoss = g.Sum(s => s.Total - (s.Mobile.Price * s.Quantity))
            })
            .ToListAsync();

        var previousReport = await _dbContext.Sales
            .Where(s => s.Date >= previousFromDate && s.Date <= previousToDate)
            .GroupBy(s => new { Year = s.Date.Year, Month = s.Date.Month })
            .Select(g => new ProfitLossReportItem
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                ProfitLoss = g.Sum(s => s.Total - (s.Mobile.Price * s.Quantity))
            })
            .ToListAsync();

        // if both same count
        for (int i = 0; i < currentReport.Count; i++)
        {
            currentReport[i].ProfitLoss -= previousReport[i].ProfitLoss;
        }

        return currentReport;
    }

    // MonthlySalesReport
    public async Task<List<MonthlySalesReportItem>> GetMonthlySalesReportAsync(DateTime fromDate, DateTime toDate)
    {
        return await _dbContext.Sales
            .Where(s => s.Date >= fromDate && s.Date <= toDate)
            .GroupBy(s => new { Year = s.Date.Year, Month = s.Date.Month })
            .Select(g => new MonthlySalesReportItem
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                TotalSales = g.Sum(s => s.Total),
                TotalProfitLoss = g.Sum(s => s.Total - (s.Mobile.Price * s.Quantity))
            })
            .ToListAsync();
    }
    public async Task<List<MonthlyBrandWiseSalesReport>> GenerateMonthlyBrandWiseSalesReportAsync(DateTime fromDate, DateTime toDate)
    {
        fromDate = new DateTime(fromDate.Year, fromDate.Month, 1);
        toDate = new DateTime(toDate.Year, toDate.Month, DateTime.DaysInMonth(toDate.Year, toDate.Month));

        var monthlySales = await _dbContext.Sales
            .Where(s => s.Date >= fromDate && s.Date <= toDate)
            .ToListAsync();

        var brandIds = monthlySales.Select(s => s.Mobile.BrandId).Distinct().ToList();
        var monthlyBrandWiseSalesReport = new List<MonthlyBrandWiseSalesReport>();

        foreach (var brandId in brandIds)
        {
            var brand = await _dbContext.Brands.FindAsync(brandId);
            var brandSales = monthlySales.Where(s => s.Mobile.BrandId == brandId).ToList();
            var totalSales = brandSales.Sum(s => s.Quantity);
            var totalProfitLoss = brandSales.Sum(s => (s.Mobile.Price - s.Mobile.Discount) * s.Quantity);

            var report = new MonthlyBrandWiseSalesReport
            {
                Brand = brand,
                TotalSales = totalSales,
                TotalProfitLoss = totalProfitLoss
            };

            monthlyBrandWiseSalesReport.Add(report);
        }

        return monthlyBrandWiseSalesReport;
    }
}
