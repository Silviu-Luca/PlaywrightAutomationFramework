using PlaywrightAutomationFramework.Helpers;
using PlaywrightAutomationFramework.RestfullBookerAutomation.Client;
using PlaywrightAutomationFramework.RestfullBookerAutomation.Models;
using PlaywrightAutomationFramework.RestfullBookerRestAutomation.DataSources;

namespace PlaywrightAutomationFramework.RestfullBookerAutomation.Tests
{
    public class RestfulBookerTests : PlaywrightTest
    {
        private RestfulBookerClient restfulBookerClient;

        [SetUp]
        public async Task SetUp()
        {
            restfulBookerClient = await RestfulBookerClient.GetInstance("admin", "password123");
        }

        [Test]
        [Category("smoke")]
        public async Task VerifyWeHaveResults()
        {
            var result = await restfulBookerClient.GetBookingIds("", "", "", "");
            Assert.That(result.Count, Is.Not.EqualTo(0));
        }

        [Test]
        [Category("regression")]
        public async Task CompleteFlow()
        {
            var createBooking = BookingDetailsDataSource.GetBookingForCreate();
            var updateBooking = BookingDetailsDataSource.GetBookingForUpdate();
            var patchBooking = BookingDetailsDataSource.GetBookingForPatch();
            var patchedBook = BookingDetailsDataSource.GetBookingAfterPatch();
             
            //search for custom booking and if exists, delete it
            var results = await restfulBookerClient.GetBookingIds(createBooking.firstname,
                createBooking.lastname, "", "");
            foreach (var result in results)
            {
                await restfulBookerClient.DeleteBooking(result.bookingid);
            }

            //check that there's no custom booking after deletion
            results = await restfulBookerClient.GetBookingIds(createBooking.firstname,
                createBooking.lastname, "", "");
            Assert.That(results.Count, Is.EqualTo(0));

            var createdBooking = await restfulBookerClient.CreateBooking(createBooking);
            Assert.That(ObjectComparer.AreObjectsEqual(createBooking, createdBooking.booking), Is.EqualTo(true));

            var updatedBooking = await restfulBookerClient.UpdateBooking(createdBooking.bookingid, updateBooking);
            Assert.That(ObjectComparer.AreObjectsEqual(updateBooking, updatedBooking), Is.EqualTo(true));

            var patchedBooking = await restfulBookerClient.PartialUpdateBooking(createdBooking.bookingid, patchBooking);
            Assert.That(ObjectComparer.AreObjectsEqual(patchedBook, patchedBooking), Is.EqualTo(true));
        }
    }
}