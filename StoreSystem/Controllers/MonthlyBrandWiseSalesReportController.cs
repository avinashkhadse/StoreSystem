using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreSystem.Business;
using StoreSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MonthlyBrandWiseSalesReportController : ControllerBase
    {
        private readonly SalesManager _salesManager;

        public MonthlyBrandWiseSalesReportController(SalesManager salesManager)
        {
            _salesManager = salesManager;
        }

        [HttpGet("MonthlyBrandWiseSalesReport")]
        public async Task<IActionResult> GetMonthlyBrandWiseSalesReport(DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (!User.IsInRole("Admin"))
                {
                    return Forbid();
                }

                if (fromDate > toDate)
                {
                    return BadRequest("Invalid date range. 'fromDate' should be earlier than 'toDate'.");
                }

                var report = await _salesManager.GenerateMonthlyBrandWiseSalesReportAsync(fromDate, toDate);
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = report,
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
