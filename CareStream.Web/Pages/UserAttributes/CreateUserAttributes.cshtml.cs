using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CareStream.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CareStream.Web.Pages.UserAttributes
{
    public class CreateUserAttributesModel : PageModel
    {
       [BindProperty]
       public ExtensionModel extensionModel { get; set; }

        public CreateUserAttributesModel()
        {
            extensionModel = new ExtensionModel();
        }

        public void OnGet()
        {
            try
            {
                    var dataTypeItems = new SelectList(new List<SelectListItem>());

                    var itemList = new List<SelectListItem>
                    {
                        new SelectListItem
                        {
                            Text = CareStreamConst.Custom_DataType_String,
                            Value = CareStreamConst.Custom_DataType_String
                        },
                        new SelectListItem
                        {
                            Text = CareStreamConst.Custom_DataType_Boolean,
                            Value = CareStreamConst.Custom_DataType_Boolean
                        },
                        new SelectListItem
                        {
                            Text = CareStreamConst.Custom_DataType_Int,
                            Value = CareStreamConst.Custom_DataType_Int
                        }
                    };

                    dataTypeItems = new SelectList(itemList, "Value", "Text");
                    ViewData["DataTypes"] = dataTypeItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Expection occured while getting group type details");
                Console.WriteLine(ex);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (extensionModel != null)
                {
                    if (string.IsNullOrEmpty(extensionModel.Name) && string.IsNullOrEmpty(extensionModel.DataType))
                    {
                        return RedirectToPage("./Index");
                    }

                    extensionModel.TargetObjects = new List<string>
                    {
                        CareStreamConst.User
                    };

                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(CareStreamConst.Base_Url);

                    var payload = JsonConvert.SerializeObject(extensionModel);
                    StringContent content = new StringContent(payload, Encoding.UTF8, CareStreamConst.Application_Json);
                    var result = await httpClient.PostAsync($"{CareStreamConst.Base_API}{CareStreamConst.Extension_Url}", content);

                    if (result.IsSuccessStatusCode)
                    {
                        var data = await result.Content.ReadAsStringAsync();
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