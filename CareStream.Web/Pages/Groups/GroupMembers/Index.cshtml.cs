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

namespace CareStream.Web.Pages.Groups.GroupMembers
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public GroupMemberModel groupMemberModel { get; set; }

        [BindProperty]
        GroupMemberAssignModel groupMemberAssignModel { get; set; }

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
                
                TempData[CareStreamConst.Member_GroupId] = Id;
                GroupId = Id;
                HttpClient httpClient = new HttpClient();

                var httpResponse = await httpClient
                            .GetAsync($"{CareStreamConst.Base_Url}{CareStreamConst.Base_API}{CareStreamConst.GroupMember_By_Id_Url}{Id}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    var data = await httpResponse.Content.ReadAsStringAsync();
                    groupMemberModel = JsonConvert.DeserializeObject<GroupMemberModel>(data);
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
                var id = TempData[CareStreamConst.Member_GroupId];
                if(id != null)
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

        public async Task<IActionResult> OnPostAddMember(List<string> selectedMember)
        {
            var id = string.Empty;
            try
            {
                id = GetGroupIdFromQuery("id");

                if (selectedMember != null)
                {
                    if (selectedMember.Any() && !string.IsNullOrEmpty(id))
                    {
                        groupMemberAssignModel = new GroupMemberAssignModel();
                        groupMemberAssignModel.GroupId = id;
                        groupMemberAssignModel.SelectedMembers = selectedMember;


                        HttpClient httpClient = new HttpClient();
                        httpClient.BaseAddress = new Uri(CareStreamConst.Base_Url);

                        var payload = JsonConvert.SerializeObject(groupMemberAssignModel);
                        StringContent content = new StringContent(payload, Encoding.UTF8, CareStreamConst.Application_Json);
                        var result = await httpClient.PostAsync($"{CareStreamConst.Base_API}{CareStreamConst.GroupMember_Url}", content);

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

        public async Task<ActionResult> OnPostGroupMemberDelete([FromBody] GroupMemberAssignModel groupMemberAssign)
        {
            try
            {
                if (groupMemberAssign != null)
                {

                    if (groupMemberAssign.SelectedMembers != null)
                    {
                        if (groupMemberAssign.SelectedMembers.Any() && !string.IsNullOrEmpty(groupMemberAssign.GroupId))
                        {
                            HttpClient httpClient = new HttpClient();
                            var url = $"{CareStreamConst.Base_Url}{CareStreamConst.Base_API}{CareStreamConst.GroupMember_Url}";

                            httpClient.BaseAddress = new Uri(CareStreamConst.Base_Url);
                            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));

                            var payload = JsonConvert.SerializeObject(groupMemberAssign);
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