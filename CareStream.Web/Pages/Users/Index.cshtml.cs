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


namespace CareStream.Web.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public UsersModel UserData { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                //var httpResponse = await httpClient.GetAsync("https://localhost:44366/api/User/users");
                var httpResponse = await httpClient.GetAsync($"{CareStreamConst.Base_Url}{CareStreamConst.Base_API}{CareStreamConst.Users_Detail_Url}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    var data = await httpResponse.Content.ReadAsStringAsync();
                    UserData = JsonConvert.DeserializeObject<UsersModel>(data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
      
        public async Task<ActionResult> OnPostUserDelete([FromBody] IEnumerable<string> userIdsToDelete)
        {
            try
            {
                if(userIdsToDelete != null)
                {
                    if (userIdsToDelete.Any())
                    {
                        HttpClient httpClient = new HttpClient();
                        //httpClient.BaseAddress = new Uri("https://localhost:44366/");
                        httpClient.BaseAddress = new Uri(CareStreamConst.Base_Url);

                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, 
                                    new Uri($"{CareStreamConst.Base_Url}{CareStreamConst.Base_API}{CareStreamConst.User_Url}"));

                        var payload = JsonConvert.SerializeObject(userIdsToDelete);
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
            return RedirectToPage("./Index");
        }
    }
}