using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int CampId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int TotalCost { get; set; }
        public string BookingReferenceNumber { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Camp Camp { get; set; }
    }
}
