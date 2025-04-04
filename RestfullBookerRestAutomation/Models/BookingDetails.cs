using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightAutomationFramework.RestfullBookerAutomation.Models
{
    public class BookingDetails
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? firstname { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? lastname { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? totalprice { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? depositpaid { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BookingDates? bookingdates { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? additionalneeds { get; set; }

    }
}
