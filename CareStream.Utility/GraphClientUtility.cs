using CareStream.LoggerService;
using CareStream.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareStream.Utility
{
    //Add Logs
    public static class GraphClientUtility
    {
        #region Variables

        public static string TenantId;
        public static string b2cExtensionAppClientId;
        public static string b2cExtensionAppObjectId;
        public static string AADGraphResourceId;
        public static string AADGraphVersion;

        private static IConfiguration configuration;

        private static string appId;
        private static string clientSecret;

        private static string aadInstance;
        private static string graphResource;
        private static string graphAPIEndpoint;
        private static string authority;

        #endregion

        #region Constructor
        static GraphClientUtility()
        {
            configuration = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();
            SetAzureADBB2COptions();
        }

        private static void SetAzureADBB2COptions()
        {
            var azureOptions = new AzureADB2C();
            configuration.Bind(CareStreamConst.AzureADB2C, azureOptions);

            TenantId = azureOptions.TenantId;
            appId = azureOptions.AppId;
            clientSecret = azureOptions.ClientSecret;
            b2cExtensionAppClientId = azureOptions.B2cExtensionAppClientId;
            b2cExtensionAppObjectId = azureOptions.B2cExtensionAppObjectId;

            graphResource = azureOptions.GraphResource;
            graphAPIEndpoint = $"{azureOptions.GraphResource}{azureOptions.GraphResourceEndPoint}";

            aadInstance = azureOptions.Instance;
            authority = $"{aadInstance}{TenantId}";

            AADGraphResourceId = azureOptions.AADGraphResourceId;
            AADGraphVersion = azureOptions.AADGraphVersion;

        }
        #endregion

        #region Get Graph Client
        public static GraphServiceClient GetGraphServiceClient()
        {
            GraphServiceClient graphClient = null;
            try
            {

                if (string.IsNullOrEmpty(appId) && string.IsNullOrEmpty(TenantId) && string.IsNullOrEmpty(clientSecret))
                {
                    //Add Logger
                    return graphClient;
                }

                IConfidentialClientApplication clientApplication = ConfidentialClientApplicationBuilder
                                                                    .Create(appId)
                                                                    .WithTenantId(TenantId)
                                                                    .WithClientSecret(clientSecret)
                                                                    .Build();

                ClientCredentialProvider clientCredentialProvider = null;
                if (clientApplication != null)
                {
                    clientCredentialProvider = new ClientCredentialProvider(clientApplication);
                }

                if (clientCredentialProvider != null)
                {
                    graphClient = new GraphServiceClient(clientCredentialProvider);
                }

            }
            catch (Exception ex)
            {
                //Todo: Add Logger
                throw ex;
            }
            return graphClient;
        }
        #endregion

        #region Extension 

        public async static Task<Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationResult> GetAuthentication()
        {
            Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationResult authenticationResult;
            try
            {
                if (string.IsNullOrEmpty(authority) && string.IsNullOrEmpty(appId) && string.IsNullOrEmpty(clientSecret) && string.IsNullOrEmpty(AADGraphResourceId))
                {
                    //Add Logger
                    return null;
                }

                AuthenticationContext authContext = new AuthenticationContext(authority);
                Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential credential = new Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential(appId, clientSecret);

                authenticationResult = await authContext.AcquireTokenAsync(AADGraphResourceId, credential);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return authenticationResult;
        }

        #endregion

        #region Convert Graph User Object to User Model Object

        public static UserModel ConvertGraphUserToUserModel(User graphUser,ILoggerManager logger)
        {
            UserModel retVal = null;
            try
            {
                if(graphUser != null)
                {
                    retVal = new UserModel
                    {
                        Groups = new List<string>(),
                        RolesAA = new List<string>(),
                        CustomAttributes = new Dictionary<string, string>(),


                        Address2 = string.Empty,
                        Address3 = string.Empty,
                        BusinessEmail = string.Empty,
                        Password = string.Empty,
                        AutoGeneratePassword = false,
                        ManualCreatedPassword = false,
                        DelegateAdministration = false,
                        Region = string.Empty,
                        SignInName = string.Empty,

                        AccountEnabled = graphUser.AccountEnabled,
                        UsageLocation = graphUser.UsageLocation,

                        Id = graphUser.Id,
                        Address = graphUser.StreetAddress, // Add Code to split the address 
                        StreetAddress = graphUser.StreetAddress,
                        GivenName = graphUser.GivenName,
                        DisplayName = graphUser.DisplayName,
                        Department = graphUser.Department,
                        CompanyName = graphUser.CompanyName,
                        Country = graphUser.Country,
                        CreationType = graphUser.CreationType,
                        Mail = graphUser.Mail,
                        MobilePhone = graphUser.MobilePhone,
                        OfficeLocation = graphUser.OfficeLocation,
                        PostalCode = graphUser.PostalCode,
                        PreferredName = graphUser.PreferredName,
                        State = graphUser.State,
                        Surname = graphUser.Surname,
                        UserPrincipalName = graphUser.UserPrincipalName,
                        UserType = graphUser.UserType,
                        JobTitle = graphUser.JobTitle,
                        BusinessPhones = graphUser.BusinessPhones,
                        WebSite = graphUser.MySite
                    };

                }
            }
            catch (Exception ex)
            {
                logger.LogError("GraphClientUtility-ConvertGraphUserToUserModel: Exception occurred....");
                logger.LogError(ex);
            }
            return retVal;
        }

        #endregion

        #region Convert Graph Group Object to Group Model Object

        public static GroupModel ConvertGraphGroupToGroupModel(Group graphGroup, ILoggerManager logger)
        {
            GroupModel retVal = null;
            try
            {
                if (graphGroup != null)
                {
                    retVal = new GroupModel
                    {
                        CreatedDateTime= graphGroup.CreatedDateTime,
                        ObjectId = graphGroup.Id,
                        DisplayName = graphGroup.DisplayName,
                        GroupTypes = graphGroup.GroupTypes,
                        MailEnabled = graphGroup.MailEnabled,
                        MailNickname = graphGroup.MailNickname,
                        SecurityEnabled = graphGroup.SecurityEnabled,
                        Visibility = graphGroup.Visibility,
                        Description = graphGroup.Description,
                        AllowExternalSenders = graphGroup.AllowExternalSenders,
                        AutoSubscribeNewMembers= graphGroup.AutoSubscribeNewMembers,
                        AdditionalData = new Dictionary<string, object>()
                    };

                    if(retVal.GroupTypes != null)
                    {
                        if (retVal.GroupTypes.Any(x => x == CareStreamConst.DynamicMembership))
                        {
                            //Nothing to do
                        }
                        else if (retVal.GroupTypes.Any(x => x == CareStreamConst.Unified))
                        {
                            retVal.GroupTypes = null;
                            retVal.GroupTypes = new List<string>
                            {
                                CareStreamConst.O365
                            };
                        }
                        else
                        {
                            retVal.GroupTypes = null;
                            retVal.GroupTypes = new List<string>
                            {
                                CareStreamConst.Security
                            };
                        }

                        if (retVal.GroupTypes.Any())
                        {
                            retVal.GroupType = string.Join(CareStreamConst.Pipe, retVal.GroupTypes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("GraphClientUtility-ConvertGraphUserToUserModel: Exception occurred....");
                logger.LogError(ex);
            }
            return retVal;
        }

        #endregion
    }
}
