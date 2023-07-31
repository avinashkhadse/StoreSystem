using StoreSystem.Data;
using StoreSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Business
{
    public class BrandManager
    {
        private readonly StoreDbContext _dbContext;

        public BrandManager(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Brand> AddBrandAsync(Brand brand)
        {
            _dbContext.Brands.Add(brand);
            await _dbContext.SaveChangesAsync();
            return brand;
        }

        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            return await _dbContext.Brands.FindAsync(id);
        }

        public async Task<List<Brand>> GetAllBrandsAsync()
        {
            return await _dbContext.Brands.ToListAsync();
        }

        public async Task<Brand> UpdateBrandAsync(int id, Brand updatedBrand)
        {
            var existingBrand = await _dbContext.Brands.FindAsync(id);

            if (existingBrand == null)
                return null;

            existingBrand.BrandName = updatedBrand.BrandName;

            await _dbContext.SaveChangesAsync();
            return existingBrand;
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            var brand = await _dbContext.Brands.FindAsync(id);

            if (brand == null)
                return false;

            _dbContext.Brands.Remove(brand);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
