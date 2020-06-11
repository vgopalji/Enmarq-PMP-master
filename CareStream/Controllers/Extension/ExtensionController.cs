using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CareStream.LoggerService;
using CareStream.Models;
using CareStream.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CareStream.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtensionController : ControllerBase
    {
        private readonly ILoggerManager _logger;

        public ExtensionController(ILoggerManager logger)
        {
            _logger = logger;
        }


        //[HttpGet("schemaextensions/")]
        [HttpGet("extensions/")]
        public async Task<IActionResult> GetSchemaExtensions()
        {
            List<ExtensionModel> retVal = new List<ExtensionModel>();
            try
            {

                var client = GraphClientUtility.GetGraphServiceClient();

                if (client == null)
                {
                    _logger.LogError("ExtensionController-GetExtensions: Unable to create proxy for the Azure AD B2C graph client");
                    return NotFound();
                }
                _logger.LogInfo("ExtensionController-GetExtensions: [Started] to fetch user attribute in Azure AD B2C");


                var schemaExtensions = await client.SchemaExtensions.Request().GetAsync();

                var deleteAppList = new List<string>
                {
                    "extcivhhslh_sbtest1",
                    "ext8t93kbpf_testSchemaRodney",
                    "extcivhhslh_sbtest1",
                    "ext8t93kbpf_testSchemaRodney",
                    "exti6wv1vlh_testingext",
                    "ext46vln54c_ntest",
                    "extaca0mlr5_ntest3",
                    "ext1xx3fz1l_ntest3",
                    "extmpe64h5d_ntest3",
                    "xylosbsocial_teamsTestArchiving",
                    "extc5bnq6uk_TestExtension",
                    "extmoxsowno_graphlearnTest1",
                    "extve5g6vcc_askjagroupextensionstest",
                    "extdtcu1cfg_nlegtest",
                    "extl44kejst_nlegtest222",
                    "exts8sy6qub_ExtensionTest",
                    "extq04xs9zk_test",
                    "diginoid_test",
                    "dommailtest_VDSExtensions",
                    "extbbb36prt_testSchemaExtension",
                    "extnr3ono6a_MitaTest",
                    "ext4d06qxmp_fducatest",
                    "regis_testDataSync",
                    "est1933a_test3",
                    "est1933a_test4",
                    "extxba991fd_myphoenixtestlab",
                    "fede_test",
                    "extyk7nj53r_TestExtensionName",
                    "extsgnu4ipx_TestExtensionName3",
                    "ext39c4063f_TestProject",
                    "ext8dswvbrw_sbtest1",
                    "extm3ewp8cn_puttitest",
                    "extxw26d35r_puttitest2",
                    "extoegtdaxg_NextTestExtension",
                    "nethabilis_TestMetadata1",
                    "exta4v28cm3_ActaTestSch",
                    "ext4s9ja9u0_deskutest0",
                    "extl8a9vgqv_TestYeahBoi",
                    "extkx0h0c9g_TestYeahBoi",
                    "gmpoc_UserDealerSchemaTest",
                    "extqxucmyye_ScripsTestExtentions",
                    "extp0sohkm9_test",
                    "extcjwhq847_testExtension",
                    "ext1qq0lzat_collabtest",
                    "extrpzqgpj6_test1",
                    "diginoid_test2",
                    "ext54vl35zq_TestUpdate",
                    "dommailtest_B2BGuestUserExtensions",
                    "ext3ju9rfik_MLschemaTest"
                };

                while (schemaExtensions.NextPageRequest != null)
                {
                    foreach (SchemaExtension extension in schemaExtensions.CurrentPage)
                    {

                        //if (extension.Id.Contains("prasad"))
                        if (extension.Id.Contains(CareStreamConst.Schema_Extension_Id))
                        {

                            try
                            {
                                foreach (var item in extension.Properties)
                                {
                                    try
                                    {
                                        var extensionModel = new ExtensionModel
                                        {
                                            ObjectId = extension.Id,
                                            Description = extension.Description,
                                            AttributeType = CareStreamConst.Custom,
                                            IsBuildIn = false,
                                            Name = item.Name,
                                            DataType = item.Type
                                        };

                                        if(extension.TargetTypes != null)
                                        {
                                            extensionModel.TargetObjects = extension.TargetTypes.ToList();
                                        }
                                        retVal.Add(extensionModel);
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogError($"ExtensionController-GetExtensions: fail to add extension [name:{item.Name}] to collection ");
                                        _logger.LogError(ex);
                                    }
                                }
                               
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError($"ExtensionController-GetExtensions: fail to add extension [name:{extension.Id}] to collection ");
                                _logger.LogError(ex);
                            }
                        }

                }
                    schemaExtensions = await schemaExtensions.NextPageRequest.GetAsync();
                }

                if (retVal.Any())
                {
                    retVal = retVal.OrderBy(x => x.Name).ToList();
                }

                _logger.LogInfo("ExtensionController-GetExtensions: [Completed] getting user attribute in Azure AD B2C");
            }
            catch (ServiceException ex)
            {
                _logger.LogError("ExtensionController-GetExtensions: Exception occured....");
                _logger.LogError(ex);

            }

            return Ok(retVal);
        }

        // GET: api/<ExtensionController>
        //[HttpGet("extensions/")]
        public async Task<IActionResult> GetExtensions()
        {
            List<ExtensionModel> retVal = new List<ExtensionModel>();
            try
            {

                var authenticationResult = GraphClientUtility.GetAuthentication();

                if (authenticationResult == null)
                {
                    _logger.LogError("ExtensionController-GetExtensions: Unable to get the Access token and Authentication Result");
                    return NotFound();
                }
                _logger.LogInfo("ExtensionController-GetExtensions: [Started] to fetch user attribute in Azure AD B2C");

                var accessToken = authenticationResult.Result.AccessToken;
                var b2cExtensionAppObjectId = GraphClientUtility.b2cExtensionAppObjectId;
                var tenantId = GraphClientUtility.TenantId;
                var aadGraphVersion = GraphClientUtility.AADGraphVersion;
                var aadGraphResourceId = GraphClientUtility.AADGraphResourceId;

                CustomExtension customExtensions = null;

                if (!string.IsNullOrEmpty(b2cExtensionAppObjectId) 
                        && !string.IsNullOrEmpty(tenantId) 
                        && !string.IsNullOrEmpty(aadGraphVersion)
                        && !string.IsNullOrEmpty(aadGraphResourceId))
                {

                    string url = $"{aadGraphResourceId}{tenantId}" +
                                 $"{CareStreamConst.ForwardSlash}{CareStreamConst.Applications}{CareStreamConst.ForwardSlash}" +
                                 $"{b2cExtensionAppObjectId}{CareStreamConst.ForwardSlash}{CareStreamConst.ExtensionProperties}" +
                                 $"{CareStreamConst.Question}{aadGraphVersion}";

                    HttpClient http = new HttpClient();
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                    request.Headers.Authorization = new AuthenticationHeaderValue(CareStreamConst.Bearer, accessToken);
                    HttpResponseMessage response = await http.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        customExtensions = JsonConvert.DeserializeObject<CustomExtension>(data);
                    }
                }

                if(customExtensions != null)
                {
                    if(customExtensions.value != null)
                    {
                        _logger.LogInfo($"ExtensionController-GetExtensions:got {customExtensions.value.Count} user attribute from Azure AD B2C");
                    }

                    var b2cExtensionAppClientId = GraphClientUtility.b2cExtensionAppClientId;
                    b2cExtensionAppClientId= b2cExtensionAppClientId.Replace(CareStreamConst.Dash, "");
                    var toReplace = $"{CareStreamConst.Extension}{CareStreamConst.Underscore}{b2cExtensionAppClientId}{CareStreamConst.Underscore}";

                    foreach (var value in customExtensions.value)
                    {
                        try
                        {
                            var extensionModel = new ExtensionModel
                            {
                                ObjectId = value.objectId,
                                DataType = value.dataType,
                                TargetObjects = value.targetObjects,
                                AttributeType = CareStreamConst.Custom,
                                Description = string.Empty,
                                Name = value.name,
                                IsBuildIn = false
                            };

                            if (!string.IsNullOrEmpty(extensionModel.Name))
                            {
                                extensionModel.Name = extensionModel.Name.Replace(toReplace, "");
                            }

                            retVal.Add(extensionModel);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"ExtensionController-GetExtensions: fail to add extension [name:{value.name}] to collection ");
                            _logger.LogError(ex);
                        }
                    }
                }
                if (retVal.Any())
                {
                    retVal = retVal.OrderBy(x => x.Name).ToList();
                }
                
                _logger.LogInfo("ExtensionController-GetExtensions: [Completed] getting user attribute in Azure AD B2C");
            }
            catch (ServiceException ex)
            {
                _logger.LogError("ExtensionController-GetExtensions: Exception occured....");
                _logger.LogError(ex);
               
            }

            return Ok(retVal);
        }

        private async Task<string> CheckIfExtensionExist(string schemaName)
        {
            var retVal = string.Empty;
            try
            {
                var client = GraphClientUtility.GetGraphServiceClient();

                if (client == null)
                {
                    _logger.LogError("ExtensionController-CheckIfExtensionExist: Unable to create proxy for the Azure AD B2C graph client");
                    return retVal;
                }

                var schemaExtensions = await client.SchemaExtensions.Request().GetAsync();

                while (schemaExtensions.NextPageRequest != null)
                {
                    foreach (SchemaExtension extension in schemaExtensions.CurrentPage)
                    {
                        if (extension.Id.Contains(schemaName))
                        {
                            retVal = extension.Id;
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(retVal))
                    {
                        break;
                    }
                    schemaExtensions = await schemaExtensions.NextPageRequest.GetAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ExtensionController-CheckIfExtensionExist: Exception occured....");
                _logger.LogError(ex);
            }
            return retVal;
        }

        [HttpPost]
        public async Task<string> Post([FromBody]ExtensionModel extension)
        {
            try
            {
                _logger.LogInfo("ExtensionController-Post: [Started] creation of user attribute in Azure AD B2C");

                if (extension == null)
                {
                    _logger.LogError("ExtensionController-Post: Input cannot be null");
                    return string.Empty;
                }


                #region Validation 

                if (extension.TargetObjects == null)
                {
                    _logger.LogError("ExtensionController-Post: Target Object for creation of custom attribute cannot be empty");
                    return string.Empty;
                }

                if (string.IsNullOrEmpty(extension.Name)
                    && string.IsNullOrEmpty(extension.DataType)
                    && !extension.TargetObjects.Any())
                {
                    _logger.LogError("ExtensionController-Post: Input [Name | Data Type | Target Obejct] for creation of custom attribute cannot be empty");
                    return string.Empty;
                }
                #endregion

                var client = GraphClientUtility.GetGraphServiceClient();

                if (client == null)
                {
                    _logger.LogError("ExtensionController-Post: Unable create graph proxy to access Azure AD B2C");
                    return string.Empty;
                }

                var taskSchemaName = CheckIfExtensionExist(CareStreamConst.Schema_Extension_Id);
                var schemaName = string.Empty;

                if(taskSchemaName != null)
                {
                    schemaName = taskSchemaName.Result;
                }

                if (string.IsNullOrEmpty(schemaName))
                {
                    var schemaExtension = new SchemaExtension
                    {
                        Id = CareStreamConst.Schema_Extension_Id,
                        Description = extension.Description,
                        TargetTypes = extension.TargetObjects,
                        Properties = new List<ExtensionSchemaProperty>()
                        {
                            new ExtensionSchemaProperty
                            {
                                Name=extension.Name,
                                Type = extension.DataType
                            }
                        }
                    };
                    await client.SchemaExtensions.Request().AddAsync(schemaExtension);
                }
                else
                {
                    var schemaExtension = new SchemaExtension
                    {
                        TargetTypes = extension.TargetObjects,
                        Properties = new List<ExtensionSchemaProperty>()
                        {
                            new ExtensionSchemaProperty
                            {
                                Name=extension.Name,
                                Type = extension.DataType
                            }
                        }
                    };
                     await client.SchemaExtensions[schemaName].Request().UpdateAsync(schemaExtension);
                }
               

                _logger.LogInfo("ExtensionController-Post: [Completed] creation of user attribute in Azure AD B2C");

                return "";

            }
            catch (Exception ex)
            {
                _logger.LogError("ExtensionController-Post: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }



        /*
        
        // POST api/<ExtensionController>
        [HttpPost]
        public async Task<string> Post([FromBody]ExtensionModel extension)
        {
            try
            {
                //Check Null condition and add logging
                _logger.LogInfo("ExtensionController-Post: [Started] creation of user attribute in Azure AD B2C");

                if (extension == null)
                {
                    _logger.LogError("ExtensionController-Post: Input cannot be null");
                    return string.Empty;
                }


                var authenticationResult = GraphClientUtility.GetAuthentication();

                if(authenticationResult == null)
                {
                    _logger.LogError("ExtensionController-Post: Unable to get the Access token and Authentication Result");
                    return string.Empty;
                }

                #region Validation 

                if (extension.TargetObjects == null)
                {
                    _logger.LogError("ExtensionController-Post: Target Object for creation of custom attribute cannot be empty");
                    return string.Empty;
                }

                if (string.IsNullOrEmpty(extension.Name)
                    && string.IsNullOrEmpty(extension.DataType)
                    && !extension.TargetObjects.Any())
                {
                    _logger.LogError("ExtensionController-Post: Input [Name | Data Type | Target Obejct] for creation of custom attribute cannot be empty");
                    return string.Empty;
                }
                #endregion

                UserAttributeModel userAttributeModel = new UserAttributeModel
                {
                    TargetObjects = extension.TargetObjects,
                    Name = extension.Name,
                    DataType = extension.DataType
                };

                var json = JsonConvert.SerializeObject(userAttributeModel);

                var accessToken = authenticationResult.Result.AccessToken;

                var tenantId = GraphClientUtility.TenantId;
                var api = $"{CareStreamConst.ForwardSlash}{CareStreamConst.Applications}{CareStreamConst.ForwardSlash}" +
                            $"{GraphClientUtility.b2cExtensionAppObjectId}{CareStreamConst.ForwardSlash}{CareStreamConst.ExtensionProperties}";

                HttpClient httpClient = new HttpClient();
                string url = GraphClientUtility.AADGraphResourceId + tenantId + api + CareStreamConst.Question + GraphClientUtility.AADGraphVersion;

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Authorization = new AuthenticationHeaderValue(CareStreamConst.Bearer, accessToken);
                request.Content = new StringContent(json, Encoding.UTF8, $"{CareStreamConst.Application_Json}");
                HttpResponseMessage response = await httpClient.SendAsync(request);


                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    object formatted = JsonConvert.DeserializeObject(error);

                    var errorMessage = "Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented);

                    _logger.LogError($"ExtensionController-Post: {errorMessage}");
                    return errorMessage;
                }


                _logger.LogInfo("ExtensionController-Post: [Completed] creation of user attribute in Azure AD B2C");


                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError("ExtensionController-Post: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }

        */



        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]List<string> extensionIdsToDelete)
        {
            try
            {
                if (extensionIdsToDelete == null)
                {
                    _logger.LogError("ExtensionController-Delete: Input value cannot be empty");
                    return NotFound();
                }

                GraphServiceClient client = GraphClientUtility.GetGraphServiceClient();
                if (client == null)
                {
                    _logger.LogError("ExtensionController-Delete: Unable to create object for graph client");
                    return NotFound();
                }

                foreach (var id in extensionIdsToDelete)
                {
                    try
                    {
                        _logger.LogInfo($"ExtensionController-Delete: [Started] removing group for id [{id}] on Azure AD B2C");

                        //await client.Groups[id].Request().DeleteAsync();

                        _logger.LogInfo($"ExtensionController-Delete: [Completed] removed group [{id}] on Azure AD B2C");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"ExtensionController-Delete: Exception occured while removing group for id [{id}]");
                        _logger.LogError(ex);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("ExtensionController-Delete: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }
    }
}
