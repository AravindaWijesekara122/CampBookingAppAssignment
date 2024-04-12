using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class AvailibilityRequestDTO
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set;}
    }
}
