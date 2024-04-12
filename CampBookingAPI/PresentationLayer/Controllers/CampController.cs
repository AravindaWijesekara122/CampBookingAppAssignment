using BusinessLogicLayer.DTO;
using BusinessLogicLayer.IService;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampController : ControllerBase
    {
        private readonly ICampService _campService;

        public CampController(ICampService campService)
        {
            _campService = campService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCampById(int id)
        {
            try
            {
                var camp = await _campService.GetCampByIdAsync(id);
                if (camp == null)
                {
                    return NotFound();
                }
                return Ok(camp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCamps()
        {
            try
            {
                var camps = await _campService.GetAllCampsAsync();
                return Ok(camps);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableCamps([FromBody] AvailibilityRequestDTO request)
        {
            try
            {
                var availableCamps = await _campService.GetAvailableCampsAsync(request.CheckInDate, request.CheckOutDate);
                return Ok(availableCamps);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCamp([FromBody] Camp camp)
        {
            try
            {
                await _campService.AddCampAsync(camp);
                return Ok("Camp added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCamp(int id, [FromBody] Camp camp)
        {
            try
            {
                var existingCamp = await _campService.GetCampByIdAsync(id);
                if (existingCamp == null)
                {
                    return NotFound();
                }

                camp.CampId = id; // Ensure the ID in the request body matches the ID in the URL
                await _campService.UpdateCampAsync(camp);
                return Ok("Camp updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCamp(int id)
        {
            try
            {
                var existingCamp = await _campService.GetCampByIdAsync(id);
                if (existingCamp == null)
                {
                    return NotFound();
                }

                await _campService.DeleteCampAsync(id);
                return Ok("Camp deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
