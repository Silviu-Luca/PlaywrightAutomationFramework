using PlaywrightAutomationFramework.RestfullBookerAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightAutomationFramework.RestfullBookerRestAutomation.DataSources
{
    public class BookingDetailsDataSource
    {
        public static BookingDetails GetBookingForCreate()
        {
            return new BookingDetails()
            {
                firstname = "FirstName",
                lastname = "LastName",
                bookingdates = new BookingDates()
                {
                    checkin = "2030-01-01",
                    checkout = "2030-01-06"
                },
                totalprice = 100,
                depositpaid = true,
                additionalneeds = "TesterNeeds"
            };
        }

        public static BookingDetails GetBookingForUpdate()
        {
            return new BookingDetails()
            {
                firstname = "UpdatedFirstName",
                lastname = "UpdatedLastName",
                bookingdates = new BookingDates()
                {
                    checkin = "2030-02-01",
                    checkout = "2030-02-06"
                },
                totalprice = 200,
                depositpaid = true,
                additionalneeds = "TesterNeedsUpdated"
            };
        }

        public static BookingDetails GetBookingForPatch()
        {
            return new BookingDetails()
            {
                firstname = "FirstName",
                lastname = "LastName",
            };
        }

        public static BookingDetails GetBookingAfterPatch()
        {
            return new BookingDetails()
            {
                firstname = "FirstName",
                lastname = "LastName",
                bookingdates = new BookingDates()
                {
                    checkin = "2030-02-01",
                    checkout = "2030-02-06"
                },
                totalprice = 200,
                depositpaid = true,
                additionalneeds = "TesterNeedsUpdated"
            };
        }
    }
}
