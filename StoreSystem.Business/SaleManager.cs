using StoreSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreSystem.Models.Reports;

namespace StoreSystem.Business
{
    public class SaleManager
    {
        private readonly StoreDbContext _dbContext;

        public SaleManager(StoreDbContext dbContext)
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
        public async Task<SaleItem> CreateSaleItemAsync(SaleItem saleItem)
        {
            _dbContext.SaleItems.Add(saleItem);
            await _dbContext.SaveChangesAsync();
            return saleItem;
        }

        public async Task<SaleItem> GetSaleItemByIdAsync(int id)
        {
            return await _dbContext.SaleItems.FindAsync(id);
        }

        public async Task<List<SaleItem>> GetAllSaleItemsAsync()
        {
            return await _dbContext.SaleItems.ToListAsync();
        }

        public async Task<SaleItem> UpdateSaleItemAsync(int id, SaleItem updatedSaleItem)
        {
            var existingSaleItem = await _dbContext.SaleItems.FindAsync(id);

            if (existingSaleItem == null)
                return null;

            existingSaleItem.SalesId = updatedSaleItem.SalesId;
            existingSaleItem.MobileId = updatedSaleItem.MobileId;
            existingSaleItem.Quantity = updatedSaleItem.Quantity;
            existingSaleItem.Total = updatedSaleItem.Total;

            await _dbContext.SaveChangesAsync();
            return existingSaleItem;
        }

        public async Task<bool> DeleteSaleItemAsync(int id)
        {
            var saleItem = await _dbContext.SaleItems.FindAsync(id);

            if (saleItem == null)
                return false;

            _dbContext.SaleItems.Remove(saleItem);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<SaleItem>> GetSaleItemsBySaleIdAsync(int saleId)
        {
            return await _dbContext.SaleItems
                .Where(si => si.SalesId == saleId)
                .ToListAsync();
        }

        public async Task<List<SaleItem>> GetSaleItemsByMobileIdAsync(int mobileId)
        {
            return await _dbContext.SaleItems
                .Where(si => si.MobileId == mobileId)
                .ToListAsync();
        }
    }
}
