using Microsoft.AspNetCore.Mvc;
using StoreSystem.Business;
using StoreSystem.Models;
using StoreSystem.Models.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StoreSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly SaleManager _saleManager;

        public SaleController(SaleManager saleManager)
        {
            _saleManager = saleManager;
        }

        [HttpPost("CreateSale")]
        public async Task<IActionResult> CreateSale(Sales sale)
        {
            try
            {
                var createdSale = await _saleManager.CreateSalesAsync(sale);
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = createdSale,
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

        [HttpGet("GetSaleDetailById/{id}")]
        public async Task<IActionResult> GetSaleDetailById(int id)
        {
            try
            {
                var sale = await _saleManager.GetSalesByIdAsync(id);
                if (sale == null)
                    return NotFound();

                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = sale,
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

        [HttpGet("GetAllSalesDetail")]
        public async Task<IActionResult> GetAllSalesDetail()
        {
            try
            {
                var sales = await _saleManager.GetAllSalesAsync();
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = sales,
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

        [HttpPut("UpdateSaleDetail/{id}")]
        public async Task<IActionResult> UpdateSaleDetail(int id, Sales updatedSale)
        {
            try
            {
                var sale = await _saleManager.GetSalesByIdAsync(id);
                if (sale == null)
                    return NotFound();

                var updated = await _saleManager.UpdateSalesAsync(updatedSale);
                
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = updated,
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

        [HttpDelete("DeleteSaleDetail/{id}")]
        public async Task<IActionResult> DeleteSaleDetail(int id)
        {
            try
            {
                var isDeleted = await _saleManager.DeleteSalesAsync(id);
                if (!isDeleted)
                    return NotFound();
                
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = null,
                    Message = "Sale deleted successfully.",
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

        [HttpGet("GetSalesDetailByMobileId/{mobileId}")]
        public async Task<IActionResult> GetSalesDetailByMobileId(int mobileId)
        {
            try
            {
                var sales = await _saleManager.GetSalesByMobileIdAsync(mobileId);                
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = sales,
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

        [HttpGet("GetSalesDetailByBrandId/{brandId}")]
        public async Task<IActionResult> GetSalesDetailByBrandId(int brandId)
        {
            try
            {
                var sales = await _saleManager.GetSalesByBrandIdAsync(brandId);
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = sales,
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

        [HttpGet("GetSalesByUser/{userId}")]
        public async Task<IActionResult> GetSalesByUser(int userId)
        {
            try
            {
                var sales = await _saleManager.GetSalesByUserAsync(userId);
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = sales,
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
        [HttpPost("CreateSaleItem")]
        public async Task<IActionResult> CreateSaleItem(SaleItem saleItem)
        {
            var createdSaleItem = await _saleManager.CreateSaleItemAsync(saleItem);
            return CreatedAtAction(nameof(GetSaleItemById), new { id = createdSaleItem.Id }, createdSaleItem);
        }

        [HttpGet("GetSaleItemById/{id}")]
        public async Task<IActionResult> GetSaleItemById(int id)
        {
            try
            {
                var saleItem = await _saleManager.GetSaleItemByIdAsync(id);
                if (saleItem == null)
                    return NotFound();

                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = saleItem,
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

        [HttpPut("UpdateSaleItem/{id}")]
        public async Task<IActionResult> UpdateSaleItem(int id, SaleItem updatedSaleItem)
        {
            try
            {
                var saleItem = await _saleManager.UpdateSaleItemAsync(id, updatedSaleItem);
                if (saleItem == null)
                    return NotFound();

                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = saleItem,
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

        [HttpDelete("DeleteSaleItem/{id}")]
        public async Task<IActionResult> DeleteSaleItem(int id)
        {
            try
            {
                var result = await _saleManager.DeleteSaleItemAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
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
    }
}
