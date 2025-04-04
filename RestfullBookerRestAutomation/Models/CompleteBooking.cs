using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightAutomationFramework.RestfullBookerAutomation.Models
{
    public class CompleteBooking
    {
        public int bookingid { get; set; }

        public BookingDetails booking { get; set; }
    }
}
