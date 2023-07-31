using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreSystem.Business;
using StoreSystem.Models;

namespace StoreSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfitLossReportController : ControllerBase
    {
        private readonly SalesManager _salesManager;

        public ProfitLossReportController(SalesManager salesManager)
        {
            _salesManager = salesManager;
        }

        [HttpGet("ProfitLossReport")]
        public async Task<IActionResult> GetProfitLossReport(DateTime currentFromDate, DateTime currentToDate, DateTime previousFromDate, DateTime previousToDate)
        {
            try
            {
                var profitReport = await _salesManager.GetProfitLossReportAsync(currentFromDate, currentToDate, previousFromDate, previousToDate);
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = profitReport,
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
