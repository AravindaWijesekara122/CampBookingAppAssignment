using BusinessLogicLayer.DTO;
using BusinessLogicLayer.IService;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBookingsByUserId(int userId)
        {
            var bookings = await _bookingService.GetBookingsByUserIdAsync(userId);
            return Ok(bookings);
        }

        [HttpGet("camp/{campId}")]
        public async Task<IActionResult> GetBookingsByCampId(int campId)
        {
            var bookings = await _bookingService.GetBookingsByCampIdAsync(campId);
            return Ok(bookings);
        }

        [HttpGet("booking/{refNumber}")]
        public async Task<IActionResult> GetBookingByRefNumber(string refNumber)
        {
            var booking = await _bookingService.GetBookingByRefNumberAsync(refNumber);
            return Ok(booking);
        }

        [HttpPost("make-booking")]
        public async Task<IActionResult> MakeBooking([FromBody] BookingRequestDTO request)
        {
            try
            {
                await _bookingService.MakeBookingAsync(request);
                return Ok("Booking successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
