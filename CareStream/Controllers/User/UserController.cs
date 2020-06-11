using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CareStream.LoggerService;
using CareStream.Models;
using CareStream.Utility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace CareStream.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILoggerManager _logger;

        public UserController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet("userdropdown/")]
        public async Task<IActionResult> GetUserDropDown()
        {
            try
            {
                UserDropDownModel retVal = new UserDropDownModel();

                _logger.LogInfo("UserController-GetUserDropDown: [Started] for getting users drop down");

                var client = GraphClientUtility.GetGraphServiceClient();

                if (client == null)
                {
                    _logger.LogError("UserController-GetUserDropDown:Unable to create object for graph client ");
                    return NotFound();
                }

                UserService userService = new UserService(_logger);

                retVal = await userService.GetUserDropDownAsync(client);

                _logger.LogInfo($"UserController-GetUsers: Completed getting user drop down");

                return Ok(retVal);
            }
            catch (ServiceException ex)
            {
                _logger.LogError("UserController-GetUserDropDown: Exception occured....");
                _logger.LogError(ex);
                if (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    return BadRequest();
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // GET: api/User
        [HttpGet("users/")]
        public async Task<IActionResult> GetUsers()
        
        {
            UsersModel usersModel = new UsersModel();
            try
            {
                _logger.LogInfo("UserController-GetUsers: [Started] for getting users detail from Azure AD B2C");

                var client = GraphClientUtility.GetGraphServiceClient();

                if (client == null)
                {
                    _logger.LogError("UserController-GetUsers:Unable to create object for graph client ");
                    return NotFound();
                }


                //Filter based on group
                //Care stream its group
                //Each application group
                var userList = await client.Users.Request().GetAsync();

                if(userList!= null)
                {
                    _logger.LogInfo($"UserController-GetUsers: Found {userList.Count} users from Azure AD B2C");
                   
                    foreach (var user in userList)
                    {
                        UserModel userModel = GraphClientUtility.ConvertGraphUserToUserModel(user, _logger);
                        usersModel.Users.Add(userModel);
                    }
                }

                _logger.LogInfo($"UserController-GetUsers: Completed getting users detail from Azure AD B2C");

                return Ok(usersModel);
            }
            catch (ServiceException ex)
            {
                _logger.LogError("UserController-GetUsers: Exception occured....");
                _logger.LogError(ex);
                if (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    return BadRequest();
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // GET: api/User/5
        [HttpGet("users/{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(string id)
        {
            
            try
            {
                var userModel = new UserModel();
                if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                {
                    _logger.LogError("UserController-GetUser: Id cannot be empty");
                    return BadRequest();
                }

                _logger.LogInfo($"UserController-GetUser: [Started] to get detail for users id {id} from Azure AD B2C");

                var client =  GraphClientUtility.GetGraphServiceClient();

                if (client == null)
                {
                    _logger.LogError("UserController-GetUser: Unable to create object for graph client");
                    return NotFound();
                }

                
                var user = await client.Users[id].Request().GetAsync();

                if(user != null)
                {
                    _logger.LogInfo($"UserController-GetUser: Fetched the user detail for id {id} from Azure AD B2C");
                    userModel = GraphClientUtility.ConvertGraphUserToUserModel(user,_logger);
                }


                _logger.LogInfo($"UserController-GetUser: [Completed] to get detail for users id {id} from Azure AD B2C");
                return Ok(userModel);

            }
            catch (ServiceException ex)
            {
                _logger.LogError("UserController-GetUser: Exception occured....");
                _logger.LogError(ex);

                if (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    return BadRequest();
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserModel user)
        {
            try
            {
                //Check Null condition and add logging
                _logger.LogInfo("UserController-Post: [Started] creation of user in Azure AD B2C");

                if (user == null)
                {
                    _logger.LogError("UserController-Post: User Model cannot be null...");
                    return NotFound();
                }

                var userService = new UserService(_logger);

                if (!userService.IsUserModelValid(user))
                {
                    _logger.LogError("UserController-GetGroups: User Name and Name is required for creating user on Azure AD B2C");
                    return NotFound();
                }

                GraphServiceClient client =  GraphClientUtility.GetGraphServiceClient();
                if (client == null)
                {
                    _logger.LogError("UserController-GetGroups: Unable to create object for graph client ");
                    return NotFound();
                }

                

                var tenantId = GraphClientUtility.TenantId;
                var b2cExtensionAppClientId = GraphClientUtility.b2cExtensionAppClientId;

                var newUser = userService.BuildUserForCreation(user, tenantId, b2cExtensionAppClientId);

                var result = await client.Users.Request().AddAsync(newUser);

                if (result != null)
                {

                    newUser.Id = result.Id;
                    _logger.LogInfo($"UserController-Post: Created user with id {newUser.Id} on Azure AD B2C");

                    #region Assign group(s) 

                    if (user.Groups != null)
                    {
                        if (user.Groups.Any())
                        {
                            _logger.LogInfo($"UserController-Post: Assiging group(s) to user with id {newUser.Id} on Azure AD B2C");

                            userService.AssignGroupsToUser(client, result, user.Groups);
                         
                        }
                    }

                    #endregion
                }

                var newUserModel = GraphClientUtility.ConvertGraphUserToUserModel(result,_logger);

                _logger.LogInfo("UserController-Post: [Completed] creation of user in Azure AD B2C");
                return Ok(newUserModel);

            }
            catch (Exception ex)
            {
                _logger.LogError("UserController-Post: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async void Put(string id, [FromBody]UserModel user)
        {
            try
            {
                if(string.IsNullOrEmpty(id) && user == null)
                {
                    _logger.LogError("UserController-Put: Input value cannot be empty");
                    return;
                }

                //Add code for validation of properites 

                _logger.LogInfo($"UserController-Put: [Started] updating detail for user id {id} on Azure AD B2C");

                var userService = new UserService(_logger);

                if (!userService.IsUserModelValid(user))
                {
                    _logger.LogError($"UserController-Put: User Name and Name is required for updating user for user id {id}");
                    return;
                }


                GraphServiceClient client =  GraphClientUtility.GetGraphServiceClient();
                if (client == null)
                {
                    _logger.LogError("UserController-Put: Unable to create object for graph client");
                    return;
                }

                //fields are editable
                var b2cExtensionAppClientId = GraphClientUtility.b2cExtensionAppClientId;

                User updateUser = userService.BuildUserForUpdate(user, b2cExtensionAppClientId);
                await client.Users[id].Request().UpdateAsync(updateUser);



                #region Assign group(s) 

                if (user.Groups != null)
                {
                    _logger.LogInfo($"UserController-Put: Created user with id {id} on Azure AD B2C");

                    if (user.Groups.Any())
                    {
                        _logger.LogInfo($"UserController-Put: Assiging group(s) to user with id {id} on Azure AD B2C");
                        userService.AssignGroupsToUser(client, updateUser, user.Groups);
                    }
                }

                #endregion

                _logger.LogInfo($"UserController-Put: [Completed] updating the user for id {id} on Azure AD B2C");

            }
            catch (Exception ex)
            {
                _logger.LogError("UserController-Put: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async void Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    _logger.LogError("UserController-Delete: Input value cannot be empty");
                    return;
                }


                _logger.LogInfo($"UserController-Delete: [Started] removing user for id {id} on Azure AD B2C");

                GraphServiceClient client =  GraphClientUtility.GetGraphServiceClient();

                if (client == null)
                {
                    _logger.LogError("UserController-Delete: Unable to create object for graph client");
                    return;
                }

                await client.Users[id].Request().DeleteAsync();
                _logger.LogInfo($"UserController-Delete: [Completed] removing for user id {id} on Azure AD B2C");
            }
            catch (Exception ex)
            {
                _logger.LogError("UserController-Delete: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }

        [HttpDelete]
        public async void Delete([FromBody]List<string> userIdsToDelete)
        {
            try
            {
                if (userIdsToDelete == null)
                {
                    _logger.LogError("UserController-Delete: Input value cannot be empty");
                    return;
                }

                GraphServiceClient client = GraphClientUtility.GetGraphServiceClient();
                if (client == null)
                {
                    _logger.LogError("UserController-Delete: Unable to create object for graph client");
                    return;
                }

                foreach (var id in userIdsToDelete)
                {
                    try
                    {
                        _logger.LogInfo($"UserController-Delete: [Started] removing user for id [{id}] on Azure AD B2C");

                        await client.Users[id].Request().DeleteAsync();
                        _logger.LogInfo($"UserController-Delete: [Completed] removing user [{id}] on Azure AD B2C");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"UserController-Delete: Exception occured while removing user for id [{id}]");
                        _logger.LogError(ex);
                    }
                }
            
            }
            catch (Exception ex)
            {
                _logger.LogError("UserController-Delete: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }
    }
}
