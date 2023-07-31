using StoreSystem.Models;
using StoreSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreSystem.Models.Reports;

public class MobileManager
{
    private readonly StoreDbContext _dbContext;

    public MobileManager(StoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Mobile> AddMobileAsync(Mobile mobile)
    {
        _dbContext.Mobiles.Add(mobile);
        await _dbContext.SaveChangesAsync();
        return mobile;
    }

    public async Task<Mobile> GetMobileByIdAsync(int id)
    {
        return await _dbContext.Mobiles.FindAsync(id);
    }

    public async Task<List<Mobile>> GetAllMobilesAsync()
    {
        return await _dbContext.Mobiles.ToListAsync();
    }

    public async Task<Mobile> UpdateMobileAsync(int id, Mobile updatedMobile)
    {
        var existingMobile = await _dbContext.Mobiles.FindAsync(id);

        if (existingMobile == null)
            return null;

        existingMobile.BrandId = updatedMobile.BrandId;
        existingMobile.Model = updatedMobile.Model;
        existingMobile.Price = updatedMobile.Price;
        existingMobile.Discount = updatedMobile.Discount;
        existingMobile.Quantity = updatedMobile.Quantity;
        existingMobile.Total = updatedMobile.Total;
        existingMobile.Date = updatedMobile.Date;

        await _dbContext.SaveChangesAsync();
        return existingMobile;
    }

    public async Task<bool> DeleteMobileAsync(int id)
    {
        var mobile = await _dbContext.Mobiles.FindAsync(id);

        if (mobile == null)
            return false;

        _dbContext.Mobiles.Remove(mobile);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<SaleItem>> GetSaleItemsByMobileIdAsync(int mobileId)
    {
        return await _dbContext.SaleItems
            .Where(si => si.MobileId == mobileId)
            .ToListAsync();
    }
}
