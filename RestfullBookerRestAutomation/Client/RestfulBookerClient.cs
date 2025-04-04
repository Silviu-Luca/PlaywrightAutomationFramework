using Microsoft.Playwright;
using Newtonsoft.Json;
using static Microsoft.Playwright.Assertions;
using Microsoft.Extensions.Configuration;
using PlaywrightAutomationFramework.RestfullBookerAutomation.Models;
using PlaywrightAutomationFramework.Helpers;

namespace PlaywrightAutomationFramework.RestfullBookerAutomation.Client
{
    public class RestfulBookerClient
    {
        #region private members
        private IPlaywright _playwright;
        private IAPIRequestContext _request = null!;
        private string _restServiceBaseUrl;
        private string _userName;
        private string _password;
        #endregion

        private RestfulBookerClient(string userName, string password)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            this._restServiceBaseUrl = config["AppConfig:BookerClientBaseUrl"];

            this._userName = userName;
            this._password = password;
        }

        private async Task CheckStatus()
        {
            
            var contextOptions = new APIRequestNewContextOptions
            {
                BaseURL = _restServiceBaseUrl,
                IgnoreHTTPSErrors = true
            };

            var request = await _playwright.APIRequest.NewContextAsync(contextOptions);
            var response = await request.GetAsync("/ping");
            await Expect(response).ToBeOKAsync();
        }

        private async Task<string> GetToken()
        {
            var request = await _playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                BaseURL = _restServiceBaseUrl,
                IgnoreHTTPSErrors = true,
                ExtraHTTPHeaders = new Dictionary<string, string>()
                {
                    { "Accept", "application/json" }
                }
            });
            var response = await request.PostAsync("/auth", new() 
            { 
                DataObject = new Dictionary<string, string>()
                {
                    { "username", _userName },
                    { "_password", _password }
                }
            });
            await Expect(response).ToBeOKAsync();
            return (await response.JsonAsync<Token>()).token;
        }

        private async Task CreateRequest(string token)
        {
            _request = await _playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                BaseURL = _restServiceBaseUrl,
                ExtraHTTPHeaders = new Dictionary<string, string>()
                {
                    {"Accept", "application/json" },
                    { "Content-Type", "application/json" },
                    {"Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=" }
                    //cookie not working
                    //{"Cookie", "token=" + token}
                }
            });
        }

        private async Task Initialize()
        {
            _playwright = await Playwright.CreateAsync();

            await CheckStatus();
            var token = await GetToken();
            await CreateRequest(token);
        }

        public static async Task<RestfulBookerClient> GetInstance(string userName, string password)
        {
            RestfulBookerClient instance = new RestfulBookerClient(userName, password);
            await instance.Initialize();
            return instance;
        }
        
        public async Task<List<Bookings>> GetBookingIds(string firstName,
            string lastName, string checkIn, string checkOut)
        {
            string queryString = "/booking";
            var queryStringParams = QueryParameters.CreateQueryStringFromParams(
                new Dictionary<string, string>()
                {
                    { "firstname", firstName},
                    { "lastname", lastName},
                    { "checkin", checkIn},
                    { "checkout", checkOut}
                });

            if(!String.IsNullOrEmpty(queryStringParams))
            {
                queryString += queryStringParams;
            }

            var response = await _request.GetAsync(queryString);
            return await response.JsonAsync<List<Bookings>>();
        }
        
        public async Task<CompleteBooking> CreateBooking(BookingDetails bookingDetails)
        {
            var response = await _request.PostAsync("/booking", new() { DataObject = bookingDetails });
            await Expect(response).ToBeOKAsync();
            return await response.JsonAsync<CompleteBooking>();
        }
        
        public async Task DeleteBooking(int bookingId)
        {
            var response = await _request.DeleteAsync("/booking/" + bookingId);
            await Expect(response).ToBeOKAsync();
        }
        
        public async Task<BookingDetails> UpdateBooking(int bookingId, BookingDetails bookingDetails)
        {
            var response = await _request.PutAsync("/booking/" + bookingId, new() { DataObject = bookingDetails });
            await Expect(response).ToBeOKAsync();
            return await response.JsonAsync<BookingDetails>();
        }
        
        public async Task<BookingDetails> PartialUpdateBooking(int bookingId, BookingDetails bookingDetails)
        {
            string json = JsonConvert.SerializeObject(bookingDetails);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            var response = await _request.PatchAsync("/booking/" + bookingId, new() { DataObject = dictionary });
            await Expect(response).ToBeOKAsync();
            return await response.JsonAsync<BookingDetails>();
        }
        
        public async Task<BookingDetails> GetBooking(int bookingId)
        {
            var response = await _request.GetAsync("/booking/" + bookingId);
            await Expect(response).ToBeOKAsync();
            return await response.JsonAsync<BookingDetails>();
        }
    }
}