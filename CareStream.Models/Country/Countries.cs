using System.Collections.Generic;

namespace CareStream.Models
{
    public class Countries
    {
        public List<Country> CountryModel { get; set; }
    }

    public class Country
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Region { get; set; }
    }
}
