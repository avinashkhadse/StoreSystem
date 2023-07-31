using Microsoft.AspNetCore.Mvc;
using StoreSystem.Business;
using StoreSystem.Models;
using System.Drawing.Drawing2D;


namespace StoreSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandManager _brandManager;

        public BrandController(BrandManager brandManager)
        {
            _brandManager = brandManager;
        }

        [HttpGet("GetAllBrand")]
        public async Task<IActionResult> GetAllBrands()
        {
            try
            {
                var brands = await _brandManager.GetAllBrandsAsync();                
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = brands,
                    Message = "success",
                    Status = "true"
                };
                return Ok(returnJson);
            }
            catch (Exception ex)
            {
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "500",
                    Data = null,
                    Message = "Error: " + ex.Message,
                    Status = "false"
                };
                return StatusCode(500, returnJson);
            }
        }

        [HttpPost("AddBrand")]
        public async Task<IActionResult> AddBrand(Brand brand)
        {
            var createdBrand = await _brandManager.AddBrandAsync(brand);
            return CreatedAtAction(nameof(GetBrandById), new { id = createdBrand.Id }, createdBrand);
        }

        [HttpGet("GetBrandDetailById/{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            try
            {
                var brand = await _brandManager.GetBrandByIdAsync(id);
                if (brand == null)
                    return NotFound();

                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = brand,
                    Message = "success",
                    Status = "true"
                };
                return Ok(returnJson);
            }
            catch (Exception ex)
            {
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "500",
                    Data = null,
                    Message = "Error: " + ex.Message,
                    Status = "false"
                };
                return StatusCode(500, returnJson);
            }
        }

        [HttpPut("UpdateBrand/{id}")]
        public async Task<IActionResult> UpdateBrand(int id, Brand updatedBrand)
        {
            try
            {
                var brand = await _brandManager.UpdateBrandAsync(id, updatedBrand);
                if (brand == null)
                    return NotFound();

                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = brand,
                    Message = "success",
                    Status = "true"
                };
                return Ok(returnJson);
            }
            catch (Exception ex)
            {
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "500",
                    Data = null,
                    Message = "Error: " + ex.Message,
                    Status = "false"
                };
                return StatusCode(500, returnJson);
            }
        }

        [HttpDelete("DeleteBrand/{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var result = await _brandManager.DeleteBrandAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}