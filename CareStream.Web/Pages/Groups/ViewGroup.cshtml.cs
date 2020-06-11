using System.Net.Http;
using System.Threading.Tasks;
using CareStream.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CareStream.Web.Pages.Groups
{
    public class ViewGroupModel : PageModel
    {
        [BindProperty]
        public GroupModel groupModel { get; set; }
        public ViewGroupModel()
        {

        }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return NotFound();
            }
            try
            {
                HttpClient httpClient = new HttpClient();

                //var httpResponse = await httpClient.GetAsync($"https://localhost:44366/api/group/groups/{Id}");
                var httpResponse = await httpClient
                        .GetAsync($"{CareStreamConst.Base_Url}{CareStreamConst.Base_API}{CareStreamConst.Group_By_Id_Url}{Id}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    var data = await httpResponse.Content.ReadAsStringAsync();
                    groupModel = JsonConvert.DeserializeObject<GroupModel>(data);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           

            return Page();
        }
    }
}