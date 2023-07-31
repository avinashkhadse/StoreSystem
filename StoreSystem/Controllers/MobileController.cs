using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreSystem.Models;

namespace StoreSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private readonly MobileManager _mobileManager;

        public MobileController(MobileManager mobileManager)
        {
            _mobileManager = mobileManager;
        }

        [HttpPost("AddMobileDetails")]
        public async Task<IActionResult> AddMobileDetails(Mobile mobile)
        {
            var createdMobile = await _mobileManager.AddMobileAsync(mobile);
            return CreatedAtAction(nameof(GetMobileById), new { id = createdMobile.Id }, createdMobile);
        }

        [HttpGet("GetMobileById/{id}")]
        public async Task<IActionResult> GetMobileById(int id)
        {
            try
            {
                var mobile = await _mobileManager.GetMobileByIdAsync(id);
                if (mobile == null)
                    return NotFound();

                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = mobile,
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

        [HttpPut("UpdateMobileDetails/{id}")]
        public async Task<IActionResult> UpdateMobileDetails(int id, Mobile updatedMobile)
        {
            try
            {
                var mobile = await _mobileManager.UpdateMobileAsync(id, updatedMobile);
                if (mobile == null)
                    return NotFound();

                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = mobile,
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

        [HttpDelete("DeleteMobileDetails/{id}")]
        public async Task<IActionResult> DeleteMobileDetails(int id)
        {
            var result = await _mobileManager.DeleteMobileAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }

}
