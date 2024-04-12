using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Camp
    {
        public int CampId { get; set; }
        public string CampName { get; set; }
        public int RatePerNight { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public bool IsBooked { get; set; }

        // Navigation property for one-to-one relationship with Booking
        public ICollection<Booking> Bookings { get; set; }
    }
}
