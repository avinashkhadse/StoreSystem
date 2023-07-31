using StoreSystem.Data;
using StoreSystem.Models;
using StoreSystem.Models.Reports;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Business
{
    public class BulkManager
    {
        private readonly StoreDbContext _dbContext;

        public BulkManager(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> BulkInsertOrUpdateMobilesAsync(List<Mobile> mobiles)
        {
            if (mobiles == null || mobiles.Count == 0)
                return false;

            var existingMobileIds = _dbContext.Mobiles.Select(m => m.Id).ToList();
            var newMobiles = mobiles.Where(m => !existingMobileIds.Contains(m.Id)).ToList();
            var updatedMobiles = mobiles.Where(m => existingMobileIds.Contains(m.Id)).ToList();

            if (newMobiles.Any())
                _dbContext.Mobiles.AddRange(newMobiles);

            if (updatedMobiles.Any())
            {
                foreach (var mobile in updatedMobiles)
                {
                    var existingMobile = _dbContext.Mobiles.Find(mobile.Id);
                    if (existingMobile != null)
                    {
                        existingMobile.BrandId = mobile.BrandId;
                        existingMobile.Model = mobile.Model;
                        existingMobile.Price = mobile.Price;
                        existingMobile.Discount = mobile.Discount;
                        existingMobile.Quantity = mobile.Quantity;
                        existingMobile.Total = mobile.Total;
                        existingMobile.Date = mobile.Date;
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BulkInsertOrUpdateSalesAsync(List<Sales> sales)
        {
            if (sales == null || sales.Count == 0)
                return false;

            var existingSaleIds = _dbContext.Sales.Select(s => s.Id).ToList();
            var newSales = sales.Where(s => !existingSaleIds.Contains(s.Id)).ToList();
            var updatedSales = sales.Where(s => existingSaleIds.Contains(s.Id)).ToList();

            if (newSales.Any())
                _dbContext.Sales.AddRange(newSales);

            if (updatedSales.Any())
            {
                foreach (var sale in updatedSales)
                {
                    var existingSale = _dbContext.Sales.Find(sale.Id);
                    if (existingSale != null)
                    {
                        existingSale.MobileId = sale.MobileId;
                        existingSale.Quantity = sale.Quantity;
                        existingSale.Total = sale.Total;
                        existingSale.Date = sale.Date;
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
