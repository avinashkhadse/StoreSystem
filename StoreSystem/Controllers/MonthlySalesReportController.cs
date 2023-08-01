using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreSystem.Models;

namespace StoreSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlySalesReportController : ControllerBase
    {
        private readonly SalesManager _salesManager;

        public MonthlySalesReportController(SalesManager salesManager)
        {
            _salesManager = salesManager;
        }

        [HttpGet("MonthlySalesReport")]
        public async Task<IActionResult> GetMonthlySalesReport(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var monthlyReport = await _salesManager.GetMonthlySalesReportAsync(fromDate, toDate);
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = monthlyReport,
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
        [HttpGet("BestPrizeByMobileId/{mobileId}")]
        public async Task<IActionResult> GetBestPriceForMobile(int mobileId)
        {
            try
            {
                var bestPrice = await _salesManager.GetBestPriceForMobileAsync(mobileId);
                return Ok(new { MobileId = mobileId, BestPrice = bestPrice });
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
