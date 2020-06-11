using CareStream.LoggerService;
using CareStream.Models;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareStream.Utility
{
    public class UserService
    {
        private readonly ILoggerManager _logger;
        public UserService(ILoggerManager logger)
        {
            _logger = logger;
        }

        public User BuildUserForCreation(UserModel userModel,string tenantId, string b2cExtensionAppClientId)
        {
            User newUser = null;
            try
            {
                if(userModel == null)
                {
                    _logger.LogError("UserService-BuildUserForCreation: Input value cannot be null");
                    return newUser;
                }

                _logger.LogInfo($"UserService-BuildUserForCreation: Building User Object for {userModel.SignInName}");

                if (string.IsNullOrEmpty(userModel.Password))
                {

                    _logger.LogWarn($"UserService-BuildUserForCreation: User {userModel.SignInName} has not set the password. System is assigning automatic password");
                    userModel.Password = GetRandomPassword();
                }

                //How to get the delegated administration
                newUser = new User();
                newUser.GivenName = userModel.GivenName;
                newUser.Surname = userModel.Surname;
                newUser.DisplayName = userModel.DisplayName;
                newUser.AccountEnabled = userModel.AccountEnabled;
                
                newUser.MailNickname = getMailNickName(userModel.GivenName, userModel.Surname);
                newUser.UsageLocation = userModel.UsageLocation;
                newUser.JobTitle = userModel.JobTitle;
                newUser.Department = userModel.Department;
                newUser.StreetAddress = $"{userModel.Address} {userModel.Address2} {userModel.Address3}";
                newUser.State = userModel.State;
                newUser.Country = userModel.Country;
                newUser.PostalCode = userModel.PostalCode;
                
                newUser.MobilePhone = userModel.MobilePhone;
                newUser.MySite = userModel.WebSite;
                newUser.Mail = userModel.BusinessEmail;

                if (!string.IsNullOrEmpty(userModel.BusinessPhone))
                    newUser.BusinessPhones = new List<string> { userModel.BusinessPhone };

                newUser.Identities = new List<ObjectIdentity>
                {
                        new ObjectIdentity()
                        {
                            SignInType =  CareStreamConst.UserSignInType_UserPrincipalName,
                            Issuer = tenantId,
                            IssuerAssignedId = $"{getMailNickName(userModel.GivenName, userModel.Surname)}@{tenantId}"
                            //IssuerAssignedId = $"{userModel.Password}-{getMailNickName(userModel.GivenName, userModel.Surname)}@{tenantId}"
                        },
                        new ObjectIdentity()
                        {
                            SignInType =  CareStreamConst.UserSignInType_EmailAddress,
                            Issuer = tenantId,
                            IssuerAssignedId = userModel.SignInName
                        }
                };

                newUser.PasswordProfile = new PasswordProfile()
                {
                    Password = userModel.Password,
                    ForceChangePasswordNextSignIn = userModel.ForceChangePasswordNextSignIn
                };

                if(userModel.CustomAttributes != null)
                {
                    if (userModel.CustomAttributes.Any())
                    {
                        var extensionInstance = BulidCustomExtension(b2cExtensionAppClientId, userModel.CustomAttributes);
                        newUser.AdditionalData = extensionInstance;
                    }
                }

                _logger.LogInfo($"UserService-BuildUserForCreation: Completed building User Object for {userModel.SignInName}");
            }
            catch (Exception ex)
            {
                _logger.LogError("UserService-BuildUserForCreation: Exception occured....");
                _logger.LogError(ex);
            }
            return newUser;
        }

        public User BuildUserForUpdate(UserModel userModel, string b2cExtensionAppClientId)
        {
            User newUser = null;
            try
            {
                if (userModel == null)
                {
                    _logger.LogError("UserService-BuildUserForUpdate: Input value cannot be null");
                    return newUser;
                }

                _logger.LogInfo($"UserService-BuildUserForUpdate: Building User Object for {userModel.SignInName}");

                newUser = new User();
                newUser.Id = userModel.Id;

                newUser.GivenName = userModel.GivenName;
                newUser.Surname = userModel.Surname;
                newUser.DisplayName = userModel.DisplayName;
                newUser.AccountEnabled = userModel.AccountEnabled;
                newUser.MailNickname = getMailNickName(userModel.GivenName, userModel.Surname);
                newUser.UsageLocation = userModel.UsageLocation;
                newUser.JobTitle = userModel.JobTitle;
                newUser.Department = userModel.Department;
                newUser.StreetAddress = $"{userModel.Address} {userModel.Address2} {userModel.Address3}";
                newUser.State = userModel.State;
                newUser.Country = userModel.Country;
                newUser.PostalCode = userModel.PostalCode;
                newUser.BusinessPhones = userModel.BusinessPhones;
                newUser.MobilePhone = userModel.MobilePhone;
                newUser.MySite = userModel.WebSite;

                if (userModel.CustomAttributes != null)
                {
                    if (userModel.CustomAttributes.Any())
                    {
                        var extensionInstance = BulidCustomExtension(b2cExtensionAppClientId, userModel.CustomAttributes);
                        newUser.AdditionalData = extensionInstance;
                    }
                }

                _logger.LogInfo($"UserService-BuildUserForCreation: Completed building User Object for {userModel.SignInName}");
            }
            catch (Exception ex)
            {
                _logger.LogError("UserService-BuildUserForCreation: Exception occured....");
                _logger.LogError(ex);
            }
            return newUser;
        }

        public bool IsUserModelValid(UserModel userModel)
        {
            if (userModel == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(userModel.SignInName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(userModel.DisplayName))
            {
                return false;
            }

            return true;
        }

        public void AssignGroupsToUser(GraphServiceClient client, User user, List<string> groups)
        {
            try
            {
                if (groups == null)
                {
                    _logger.LogWarn("UserService-AssignGroupsToUser: User group is null");
                    return;
                }

                if (!groups.Any())
                {
                    _logger.LogInfo("UserService-AssignGroupsToUser: No user group");
                    return;
                }

                _logger.LogInfo("UserService-AssignGroupsToUser: Starting to assign user to groups");

                var groupService = new GroupService(_logger, client);
                var allGroups = groupService.GetGroups();

                if (allGroups != null)
                {
                    _logger.LogInfo($"UserService-AssignGroupsToUser: Got {allGroups.Count}  groups from Azure AD B2C");

                    if (allGroups.Any())
                    {
                        UserGroup userGroup = new UserGroup
                        {
                            AllGroups = allGroups,
                            User = user,
                            Groups = groups
                        };

                        var userGroupService = new UserGroupService(_logger, client);
                        var assignResult = userGroupService.AssignUserToGroup(userGroup);

                        if (assignResult != null)
                        {
                            var groupAssignedResults = assignResult.Result.ToList<GroupAssigned>();

                            if (groupAssignedResults.Any(x => x.IsGroupAssigned == false))
                            {
                                _logger.LogWarn($"UserService-AssignGroupsToUser: Failed to assign one or more group for User Id: {user.Id}, see the detail above");
                            }
                            else
                            {
                                _logger.LogInfo($"UserService-AssignGroupsToUser: Assigned group(s) to user [with id {user.Id} on Azure AD B2C");
                            }

                        }
                    }
                    else
                    {
                        _logger.LogInfo("UserService-AssignGroupsToUser: No group found in Azure AD B2C");
                    }

                }
                _logger.LogInfo("UserService-AssignGroupsToUser: Completed to assign user to groups");
            }
            catch (Exception ex)
            {
                _logger.LogError("UserService-AssignGroupsToUser: Exception occured....");
                _logger.LogError(ex);
            }
        }

        public async Task<UserDropDownModel> GetUserDropDownAsync(GraphServiceClient client)
        {
            UserDropDownModel retVal = null;
            try
            {
                 retVal = new UserDropDownModel();

                var loadUserDropDownTasks = new Task[]
                {
                    Task.Run(() => {
                                         PasswordService.Logger = _logger;
                                         retVal.AutoPassword = PasswordService.GenerateNewPassword(GetRandomNumber(),
                                                                    GetRandomNumber(), GetRandomNumber(), GetRandomNumber());
                                     }),
                    Task.Run(() => {
                                        GroupService groupService = new GroupService(_logger, client);
                                        retVal.Groups = groupService.GetGroups();
                                     }),
                    Task.Run(() => retVal.UserRoles = GetUserRoles()),
                    Task.Run(() =>
                                {
                                    CountryService countryService = new CountryService(_logger);
                                    var countryResult = countryService.GetCountries();

                                    if(countryResult != null)
                                    {
                                        retVal.UserLocation = countryResult.Result.CountryModel;
                                    }
                                }),
                     Task.Run(() => retVal.UserTypes = GetUserTypes()),
                     Task.Run(() => retVal.UserLanguages = GetUserLanguage()),
                     Task.Run(() =>  retVal.UserBusinessDepartments = GetUserBusinessDepartments()),
                };

                await Task.WhenAll(loadUserDropDownTasks);
            }
            catch (Exception ex)
            {
                retVal = null;
                _logger.LogError("UserService-GetUserDropDownAsync: Exception occured....");
                _logger.LogError(ex);
            }
            return retVal;
        }

        public string GetRandomPassword()
        {
            var retVal = string.Empty;
            try
            {
                PasswordService.Logger = _logger;
                retVal = PasswordService.GenerateNewPassword(GetRandomNumber(), GetRandomNumber(), GetRandomNumber(), GetRandomNumber());
            }
            catch (Exception ex)
            {
                _logger.LogError("UserService-GetRandomPassword: Exception occured....");
                _logger.LogError(ex);
            }
            return retVal;
        }
        public UserDropDownModel GetUserDropDown(GraphServiceClient client)
        {
            UserDropDownModel retVal = null;
            try
            {
                if (client == null)
                {
                    _logger.LogWarn("UserService-GetUserDropDown: Graph client cannot be null");
                    return retVal;
                }

                _logger.LogInfo("UserService-GetUserDropDown: Starting to get user drop down");
                
                retVal = new UserDropDownModel();

                // Password
                retVal.AutoPassword = GetRandomPassword();
                _logger.LogInfo("UserService-GetUserDropDown: Generated auto password for user drop down");

                //Groups 
                GroupService groupService = new GroupService(_logger, client);
                retVal.Groups = groupService.GetGroups();

                //Roles
                retVal.UserRoles = GetUserRoles();

                //User Location
                CountryService countryService = new CountryService(_logger);
                var countryResult = countryService.GetCountries();

                if(countryResult != null)
                {
                    retVal.UserLocation = countryResult.Result.CountryModel;
                }

                // User Type
                retVal.UserTypes = GetUserTypes();

                //User Language
                retVal.UserLanguages = GetUserLanguage();

                //User Business Department
                retVal.UserBusinessDepartments = GetUserBusinessDepartments();

                _logger.LogInfo("UserService-GetUserDropDown: Completed getting user drop down");
            }
            catch (Exception ex)
            {
                retVal = null;
                _logger.LogError("UserService-GetUserDropDown: Exception occured....");
                _logger.LogError(ex);
            }
            return retVal;
        }


        #region Private Methods
        private Dictionary<string, object> BulidCustomExtension(string b2cExtensionAppClientId, Dictionary<string, string> customAttributes)
        {
            Dictionary<string, object> retVal = null;

            try
            {
                if (string.IsNullOrWhiteSpace(b2cExtensionAppClientId))
                {
                    _logger.LogError("B2C Extension App ClientId (ApplicationId) is missing in the appsettings.json. " +
                        "Get it from the App Registrations blade in the Azure portal. The app registration has the name 'b2c-extensions-app. " +
                        "Do not modify. Used by AADB2C for storing user data.");
                    return retVal;
                }

                if (customAttributes != null)
                {
                    var extensionAppClientId = b2cExtensionAppClientId.Replace(CareStreamConst.Dash, "");
                    retVal = new Dictionary<string, object>();

                    foreach (KeyValuePair<string, string> entry in customAttributes)
                    {
                        if (!string.IsNullOrEmpty(entry.Key))
                        {
                            var key = $"{CareStreamConst.Extension}_{extensionAppClientId}_{entry.Key}";
                            retVal.Add(key, entry.Value);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("UserService-BulidCustomExtension: Exception occurred...");
                _logger.LogError(ex);
            }

            return retVal;
        }

        private string getMailNickName(string givenName, string surName)
        {
            givenName = givenName != null ? givenName : "";
            surName = surName != null ? surName : "";

            return (givenName.Replace(" ", "") + "." + surName.Replace(" ", "")).Trim();
        } 

        private int GetRandomNumber()
        {
            var retVal = 4;
            try
            {
                Random random = new Random();
                retVal = random.Next(1, 10);
            }
            catch (Exception ex)
            {
                _logger.LogError("UserService-GetRandomString: Exception occured....");
                _logger.LogError(ex);
            }
            return retVal;
        }

        private List<UserRole> GetUserRoles()
        {
            List<UserRole> retVal = null;
            try
            {
                retVal = new List<UserRole>
                {
                    new UserRole
                    {
                        Key = "AppAdm",
                        Value = "Application administrator"
                    },
                     new UserRole
                    {
                        Key = "AppDev",
                        Value = "Application developer"
                    },
                      new UserRole
                    {
                        Key = "AutAdm",
                        Value = "Authentication administrator"
                    },
                     new UserRole
                    {
                        Key = "AzDevAdm",
                        Value = "Azure DevOps administrator"
                    },

                    new UserRole
                    {
                        Key = "AzInfProAdm",
                        Value = "Azure Information Protection administrator"
                    },
                     new UserRole
                    {
                        Key = "B2CIEFAdm",
                        Value = "B2C IEF Keyset administrator"
                    },
                      new UserRole
                    {
                        Key = "ClDevAdm",
                        Value = "Cloud device administrator"
                    },
                     new UserRole
                    {
                        Key = "DeskAnaAdm",
                        Value = "Desktop Analytics administrator"
                    },
                        new UserRole
                    {
                        Key = "GloAdm",
                        Value = "Global administrator"
                    },
                     new UserRole
                    {
                        Key = "GloRea",
                        Value = "Global reader"
                    }
                };

                retVal = retVal.OrderBy(x => x.Value).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("UserService-GetUserRoles: Exception occured....");
                _logger.LogError(ex);
            }
            return retVal;
        }

        private List<UserType> GetUserTypes()
        {
            List<UserType> retVal = null;
            try
            {
                retVal = new List<UserType>
                {
                    new UserType
                    {
                        Key = "Cus",
                        Value = "Customer"
                    },
                    new UserType
                    {
                        Key = "Del",
                        Value = "Dealer"
                    },
                     new UserType
                    {
                        Key = "Con",
                        Value = "Contractor"
                    },
                    new UserType
                    {
                        Key = "Ext",
                        Value = "External Manager"
                    },
                    new UserType
                    {
                        Key = "Oth",
                        Value = "Other"
                    }
                };

                retVal = retVal.OrderBy(x => x.Value).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("UserService-GetUserRoles: Exception occured....");
                _logger.LogError(ex);
            }
            return retVal;
        }

        private List<UserLanguage> GetUserLanguage()
        {
            List<UserLanguage> retVal = null;
            try
            {
                retVal = new List<UserLanguage>
                {
                    new UserLanguage
                    {
                        Key = "Ar",
                        Value = "Arabic"
                    },
                     new UserLanguage
                    {
                        Key = "EN",
                        Value = "English"
                    },
                      new UserLanguage
                    {
                        Key = "GE",
                        Value = "German"
                    },
                     new UserLanguage
                    {
                        Key = "FR",
                        Value = "French"
                    },
                      new UserLanguage
                    {
                        Key = "RU",
                        Value = "Russian"
                    },
                     new UserLanguage
                    {
                        Key = "SP",
                        Value = "Spanish"
                    }

                };

                retVal = retVal.OrderBy(x => x.Value).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("UserService-GetUserLanguage: Exception occured....");
                _logger.LogError(ex);
            }
            return retVal;
        }

        private List<UserBusinessDepartment> GetUserBusinessDepartments()
        {
            List<UserBusinessDepartment> retVal = null;
            try
            {
                retVal = new List<UserBusinessDepartment>
                {
                    new UserBusinessDepartment
                    {
                        Key = "Ser",
                        Value = "Service"
                    },
                     new UserBusinessDepartment
                    {
                        Key = "Mar",
                        Value = "Marketing"
                    },
                     new UserBusinessDepartment
                    {
                        Key = "Sal",
                        Value = "Sales"
                    },
                     new UserBusinessDepartment
                    {
                        Key = "Pur",
                        Value = "Purchasing"
                    },
                    new UserBusinessDepartment
                    {
                        Key = "Fin",
                        Value = "Finance"
                    },
                     new UserBusinessDepartment
                    {
                        Key = "Log",
                        Value = "Logistics"
                    }
                };

                retVal = retVal.OrderBy(x => x.Value).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("UserService-GetUserBusinessDepartments: Exception occured....");
                _logger.LogError(ex);
            }
            return retVal;
        }
        #endregion


    }
}
