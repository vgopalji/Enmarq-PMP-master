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

namespace CareStream.Web.Pages.Groups.GroupOwners
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public GroupOwnerModel groupOwnerModel { get; set; }

        [BindProperty]
        GroupOwnerAssignModel groupOwnerAssignModel { get; set; }

        [BindProperty]
        public string GroupId { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync(string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id))
                {

                    Id = GetGroupIdFromQuery("controller");
                    if (string.IsNullOrEmpty(Id))
                        return;
                }

                TempData[CareStreamConst.Owner_GroupId] = Id;
                GroupId = Id;
                HttpClient httpClient = new HttpClient();

                var httpResponse = await httpClient
                            .GetAsync($"{CareStreamConst.Base_Url}{CareStreamConst.Base_API}{CareStreamConst.GroupOwner_By_Id_Url}{Id}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    var data = await httpResponse.Content.ReadAsStringAsync();
                    groupOwnerModel = JsonConvert.DeserializeObject<GroupOwnerModel>(data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetGroupIdFromQuery(string key)
        {
            var retVal = string.Empty;
            try
            {
                var id = TempData[CareStreamConst.Owner_GroupId];
                if (id != null)
                {
                    return id.ToString();
                }

                if (Request.Query != null)
                {
                    if (Request.Query.Any())
                    {
                        if (Request.Query.ContainsKey(key))
                        {
                            retVal = Request.Query[key];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return retVal;
        }

        public async Task<IActionResult> OnPostAddOwner(List<string> selectedOwner)
        {
            var id = string.Empty;
            try
            {
                id = GetGroupIdFromQuery("id");

                if (selectedOwner != null)
                {
                    if (selectedOwner.Any() && !string.IsNullOrEmpty(id))
                    {
                        groupOwnerAssignModel = new GroupOwnerAssignModel();
                        groupOwnerAssignModel.GroupId = id;
                        groupOwnerAssignModel.SelectedOwners = selectedOwner;


                        HttpClient httpClient = new HttpClient();
                        httpClient.BaseAddress = new Uri(CareStreamConst.Base_Url);

                        var payload = JsonConvert.SerializeObject(groupOwnerAssignModel);
                        StringContent content = new StringContent(payload, Encoding.UTF8, CareStreamConst.Application_Json);

                        var result = await httpClient.PostAsync($"{CareStreamConst.Base_API}{CareStreamConst.GroupOwner_Url}", content);

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
            return this.RedirectToAction("OnGetAsync", id);
        }

        public async Task<ActionResult> OnPostGroupOwnerDelete([FromBody] GroupOwnerAssignModel groupOwnerAssign)
        {
            try
            {
                if (groupOwnerAssign != null)
                {

                    if (groupOwnerAssign.SelectedOwners != null)
                    {
                        if (groupOwnerAssign.SelectedOwners.Any() && !string.IsNullOrEmpty(groupOwnerAssign.GroupId))
                        {
                            HttpClient httpClient = new HttpClient();
                            var url = $"{CareStreamConst.Base_Url}{CareStreamConst.Base_API}{CareStreamConst.GroupOwner_Url}";

                            httpClient.BaseAddress = new Uri(CareStreamConst.Base_Url);
                            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));

                            var payload = JsonConvert.SerializeObject(groupOwnerAssign);
                            request.Content = new StringContent(payload, Encoding.UTF8, CareStreamConst.Application_Json);
                            var result = await httpClient.SendAsync(request);

                            if (result.IsSuccessStatusCode)
                            {
                                var data = await result.Content.ReadAsStringAsync();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return this.RedirectToAction("OnGetAsync");
        }
    }
}