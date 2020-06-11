using System;
using System.Linq;
using System.Threading.Tasks;
using CareStream.LoggerService;
using CareStream.Models;
using CareStream.Utility;
using Microsoft.AspNetCore.Mvc;


namespace CareStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        public CountryController(ILoggerManager logger)
        {
            _logger = logger;
        }

        // GET: api/<CountryController>
        [HttpGet("countries/")]
        public async Task<IActionResult> GetCountriesAsync()
        {
            
            try
            {
                Countries retVal = null;
                _logger.LogInfo($"CountryController-GetCountries: Started getting country detail");

                CountryService countryService = new CountryService(_logger);
                var countries = await countryService.GetCountries();

                if(countries != null)
                {
                    var countryModel = countries.CountryModel.OrderBy(x => x.CountryName).ToList();
                    retVal = new Countries
                    {
                        CountryModel = countryModel
                    };
                }
                _logger.LogInfo($"CountryController-GetCountries: Completed getting country detail");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _logger.LogError("CountryController-GetCountries: Exception occured....");
                _logger.LogError(ex);
                return NotFound();
            }
            
        }

       
    }
}
