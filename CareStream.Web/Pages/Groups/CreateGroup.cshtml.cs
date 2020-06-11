using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CareStream.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CareStream.Web.Pages.Groups
{
    public class CreateGroupModel : PageModel
    {
        [BindProperty]
        public GroupModel groupModel { get; set; }

        public CreateGroupModel()
        {
            groupModel = new GroupModel();
        }
        public void OnGet()
        {
            try
            {
                if(groupModel.GroupTypes != null)
                {
                    var groupItems = new SelectList(new List<SelectListItem>());

                    var itemList = new List<SelectListItem>();
                    foreach (var groupType in groupModel.GroupTypes)
                    {
                        try
                        {
                            var groupTypeListItem = new SelectListItem
                            {
                                Text = groupType,
                                Value = groupType
                            };

                            itemList.Add(groupTypeListItem);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error while assign user group type for key {0}", groupType);
                            Console.WriteLine(ex);
                        }
                    }

                    groupItems = new SelectList(itemList, "Value", "Text");
                    ViewData["GroupType"] = groupItems;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Expection occured while getting group type details");
                Console.WriteLine(ex);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            GroupModel newGroupModel = null;
            try
            {

                if(groupModel != null)
                {
                    if (string.IsNullOrEmpty(groupModel.DisplayName) && string.IsNullOrEmpty(groupModel.GroupType))
                    {
                        return RedirectToPage("./Index");
                    }

                    switch (groupModel.GroupType)
                    {
                        case CareStreamConst.O365:
                            groupModel.GroupTypes = new List<string>
                            {
                                CareStreamConst.Unified
                            };
                            groupModel.MailEnabled = true;
                            groupModel.SecurityEnabled = false;
                            break;
                        case CareStreamConst.Security:
                            groupModel.GroupTypes = new List<string>();
                            groupModel.MailEnabled = false;
                            groupModel.SecurityEnabled = true;
                            break;
                    }
                    groupModel.Visibility = "Public";

                    var displayNames = Regex.Split(groupModel.DisplayName, @"\W+");

                    if (displayNames.Count() > 0)
                        groupModel.MailNickname = string.Join('_', displayNames);

                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(CareStreamConst.Base_Url);

                    var payload = JsonConvert.SerializeObject(groupModel);
                    StringContent content = new StringContent(payload, Encoding.UTF8, CareStreamConst.Application_Json);
                    var result = await httpClient.PostAsync($"{CareStreamConst.Base_API}{CareStreamConst.Group_Url}", content);

                    if (result.IsSuccessStatusCode)
                    {
                        var data = await result.Content.ReadAsStringAsync();
                        newGroupModel = JsonConvert.DeserializeObject<GroupModel>(data);
                    }

                    if (newGroupModel != null)
                    {
                        return RedirectToPage("./ViewGroup", new { id = newGroupModel.ObjectId });
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return RedirectToPage("./Index");
        }
    }
}