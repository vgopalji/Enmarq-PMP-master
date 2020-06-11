using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CareStream.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CareStream.Web.Pages.Groups
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<GroupModel> Groups { get; set; }
       
        public async Task OnGetAsync()
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                var httpResponse = await httpClient
                            .GetAsync($"{CareStreamConst.Base_Url}{CareStreamConst.Base_API}{CareStreamConst.GroupsDetail_Url}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    var data = await httpResponse.Content.ReadAsStringAsync();
                    Groups = JsonConvert.DeserializeObject<List<GroupModel>>(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }

        }

        public async Task<ActionResult> OnPostGroupDelete([FromBody] IEnumerable<string> groupIdsToDelete)
        {
            try
            {
                if (groupIdsToDelete != null)
                {
                    if (groupIdsToDelete.Any())
                    {
                        HttpClient httpClient = new HttpClient();

                        httpClient.BaseAddress = new Uri(CareStreamConst.Base_Url);
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete,
                                   new Uri($"{CareStreamConst.Base_Url}{CareStreamConst.Base_API}{CareStreamConst.Group_Url}"));

                        var payload = JsonConvert.SerializeObject(groupIdsToDelete);
                        request.Content = new StringContent(payload, Encoding.UTF8, CareStreamConst.Application_Json);
                        var result = await httpClient.SendAsync(request);

                        if (result.IsSuccessStatusCode)
                        {
                            var data = await result.Content.ReadAsStringAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
           return  RedirectToAction("OnGetAsync");
           // return RedirectToPage("./Index");
        }
    }
}