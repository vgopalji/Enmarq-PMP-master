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
using Newtonsoft.Json;

namespace CareStream.Web.Pages.Users
{
    public class CreateUserModel : PageModel
    {
        [BindProperty]
        public UserModel userModel { get; set; }

        public CreateUserModel()
        {
            userModel = new UserModel();
            
        }
        

        public async Task OnGetAsync()
        {
            try
            {
                #region Default Selected List
                    var gItems = new SelectList(new List<SelectListItem>());
                    var roleItems = new SelectList(new List<SelectListItem>());
                    var userLocationItems = new SelectList(new List<SelectListItem>());
                    var userTypeItems = new SelectList(new List<SelectListItem>());
                    var languageItems = new SelectList(new List<SelectListItem>());
                    var userBusDepartmentItems = new SelectList(new List<SelectListItem>()); 
                #endregion

                UserDropDownModel userDropDownModel = null;
                HttpClient httpClient = new HttpClient();

                //var httpResponse = await httpClient.GetAsync("https://localhost:44366/api/User/userdropdown");
                var httpResponse = await httpClient.GetAsync($"{CareStreamConst.Base_Url}{CareStreamConst.Base_API}{CareStreamConst.Users_DropDown_Url}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    var data = await httpResponse.Content.ReadAsStringAsync();
                    userDropDownModel = JsonConvert.DeserializeObject<UserDropDownModel>(data);
                }

               
                if (userDropDownModel != null)
                {
                    userModel.AutoGeneratePassword = true;
                    userModel.Password = userDropDownModel.AutoPassword;

                    #region Group 

                    if (userDropDownModel.Groups != null)
                    {
                        var itemList = new List<SelectListItem>();
                        foreach (var group in userDropDownModel.Groups)
                        {
                            try
                            {
                                var groupListItem = new SelectListItem
                                {
                                    Text = group.Key,
                                    Value = group.Key
                                };

                                itemList.Add(groupListItem);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while assign group key {0}", group.Key);
                                Console.WriteLine(ex);
                            }
                        }

                         gItems = new SelectList(itemList, "Value", "Text");
                    }

                    #endregion

                    #region Roles 

                    if (userDropDownModel.UserRoles != null)
                    {
                        var itemList = new List<SelectListItem>();
                        foreach (var role in userDropDownModel.UserRoles)
                        {
                            try
                            {
                                var roleListItem = new SelectListItem
                                {
                                    Text = role.Value,
                                    Value = role.Key
                                };

                                itemList.Add(roleListItem);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while assign role for key {0}", role.Key);
                                Console.WriteLine(ex);
                            }
                        }

                        roleItems = new SelectList(itemList, "Value", "Text");
                    }

                    #endregion

                    #region User Location 

                    if (userDropDownModel.UserLocation != null)
                    {
                        var itemList = new List<SelectListItem>();
                        foreach (var location in userDropDownModel.UserLocation)
                        {
                            try
                            {
                                var locationListItem = new SelectListItem
                                {
                                    Text = location.CountryName,
                                    Value = location.CountryCode
                                };

                                itemList.Add(locationListItem);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while assign user location for key {0}", location.CountryCode);
                                Console.WriteLine(ex);
                            }
                        }

                        userLocationItems = new SelectList(itemList, "Value", "Text");
                    }

                    #endregion

                    #region User Types 

                    if (userDropDownModel.UserTypes != null)
                    {
                        var itemList = new List<SelectListItem>();
                        foreach (var userType in userDropDownModel.UserTypes)
                        {
                            try
                            {
                                var userTypeListItem = new SelectListItem
                                {
                                    Text = userType.Value,
                                    Value = userType.Key
                                };

                                itemList.Add(userTypeListItem);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while assign user type for key {0}", userType.Key);
                                Console.WriteLine(ex);
                            }
                        }

                        userTypeItems = new SelectList(itemList, "Value", "Text");
                    }

                    #endregion

                    #region Langauage

                    if (userDropDownModel.UserLanguages != null)
                    {
                        var itemList = new List<SelectListItem>();
                        foreach (var langauage in userDropDownModel.UserLanguages)
                        {
                            try
                            {
                                var langauageListItem = new SelectListItem
                                {
                                    Text = langauage.Value,
                                    Value = langauage.Key
                                };

                                itemList.Add(langauageListItem);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while assign user langauage for key {0}", langauage.Key);
                                Console.WriteLine(ex);
                            }
                        }

                        languageItems = new SelectList(itemList, "Value", "Text");
                    }

                    #endregion

                    #region User Business Department

                    if (userDropDownModel.UserBusinessDepartments != null)
                    {
                        var itemList = new List<SelectListItem>();
                        foreach (var userBusinessDepartment in userDropDownModel.UserBusinessDepartments)
                        {
                            try
                            {
                                var userBusinessDepartmentListItem = new SelectListItem
                                {
                                    Text = userBusinessDepartment.Value,
                                    Value = userBusinessDepartment.Key
                                };

                                itemList.Add(userBusinessDepartmentListItem);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while assign user business department for key {0}", userBusinessDepartment.Key);
                                Console.WriteLine(ex);
                            }
                        }

                        userBusDepartmentItems = new SelectList(itemList, "Value", "Text");
                    }

                    #endregion

                    ViewData["Group"] = gItems;
                    ViewData["Roles"] = roleItems;
                    ViewData["UserLocation"] = userLocationItems;
                    ViewData["UserType"] = userTypeItems;
                    ViewData["Language"] = languageItems;
                    ViewData["UserBusDep"] = userBusDepartmentItems;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Expection occured while getting user drop down details");
                Console.WriteLine(ex);
            }

        }


        public async Task<IActionResult> OnPostAsync()
        {
            if(userModel != null)
            {

                if(string.IsNullOrEmpty(userModel.SignInName) && string.IsNullOrEmpty(userModel.DisplayName))
                {
                    return RedirectToPage("./Index");
                }

                #region Custom Attributes

                userModel.CustomAttributes = new Dictionary<string, string>();

                if (userModel.RolesAA != null)
                {
                    if (userModel.RolesAA.Any())
                    {
                        userModel.Roles_C = string.Join(CareStreamConst.Pipe, userModel.RolesAA);
                        userModel.CustomAttributes.Add(CareStreamConst.Roles_C, userModel.Roles_C);
                    }
                }

                if (userModel.UserTypeAA != null)
                {
                    if (userModel.UserTypeAA.Any())
                    {
                        userModel.UserType_C = string.Join(CareStreamConst.Pipe, userModel.UserTypeAA);
                        userModel.CustomAttributes.Add(CareStreamConst.UserType_C, userModel.UserType_C);
                    }
                }

                if (userModel.UserBusinessDepartmentAA != null)
                {
                    if (userModel.UserBusinessDepartmentAA.Any())
                    {
                        userModel.UserBusinessDepartment_C = string.Join(CareStreamConst.Pipe, userModel.UserBusinessDepartmentAA);
                        userModel.CustomAttributes.Add(CareStreamConst.UserBusinessDepartment_C, userModel.UserBusinessDepartment_C);
                    }
                }

                if (!string.IsNullOrEmpty(userModel.Language_C))
                {
                    userModel.CustomAttributes.Add(CareStreamConst.Language_C, userModel.Language_C);
                }
                #endregion


                HttpClient httpClient = new HttpClient();
                //httpClient.BaseAddress = new Uri("https://localhost:44366/");
                httpClient.BaseAddress = new Uri(CareStreamConst.Base_Url);
                var payload = JsonConvert.SerializeObject(userModel);
                StringContent content = new StringContent(payload, Encoding.UTF8, CareStreamConst.Application_Json);
                var result = await httpClient.PostAsync($"{CareStreamConst.Base_API}{CareStreamConst.User_Url}", content);

                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    var t = JsonConvert.DeserializeObject<UserModel>(data);
                }

            }
            return RedirectToPage("./Index");
        }


    }
}