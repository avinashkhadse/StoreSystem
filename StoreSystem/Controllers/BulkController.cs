using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreSystem.Business;
using StoreSystem.Models;
using StoreSystem.Models.Reports;

namespace StoreSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulkController : ControllerBase
    {
        private readonly BulkManager _bulkManager;
        public BulkController(BulkManager bulkManager)
        {
            _bulkManager = bulkManager;
        }

        [HttpPost("BulkInsertUpdateMobiles")]
        public async Task<IActionResult> BulkInsertOrUpdateMobiles(List<Mobile> mobiles)
        {
            try
            {
                var result = await _bulkManager.BulkInsertOrUpdateMobilesAsync(mobiles);

                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = result,
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

        [HttpPost("BulkInsertUpdateSales")]
        public async Task<IActionResult> BulkInsertOrUpdateSales(List<Sales> sales)
        {
            try
            {
                var result = await _bulkManager.BulkInsertOrUpdateSalesAsync(sales);
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = result,
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
    }

}
