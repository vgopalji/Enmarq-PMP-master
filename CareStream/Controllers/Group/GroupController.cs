using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CareStream.LoggerService;
using CareStream.Models;
using CareStream.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace CareStream.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly ILoggerManager _logger;

        public GroupController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet("groupdetails/")]
        public async Task<IActionResult> GetGroupDetails()
        {
            try
            {
                _logger.LogInfo("GroupController-GetGroupDetails: [Started] for fetching group detail from Azure AD B2C");

                var client = GraphClientUtility.GetGraphServiceClient();

                if (client == null)
                {
                    _logger.LogError("GroupController-GetGroupDetails: Unable to create object for graph client");
                    return NotFound();
                }

                var groupService = new GroupService(_logger, client);
                var retVal =  await groupService.GetGroupDetails();

                _logger.LogInfo("GroupController-GetGroupDetails: [Completed] fetching group detail from Azure AD B2C");

                return Ok(retVal);
            }
            catch (ServiceException ex)
            {
                _logger.LogError("GroupController-GetGroupDetails: Exception occured....");
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

        //GET: api/<GroupController>
        [HttpGet("groups/")]
        public  IActionResult GetGroups()
        {
            try
            {
                _logger.LogInfo("GroupController-GetGroups: [Started] for fetching group detail from Azure AD B2C");

                var client = GraphClientUtility.GetGraphServiceClient();

                if (client == null)
                {
                    _logger.LogError("GroupController-GetGroups: Unable to create object for graph client");
                    return NotFound();
                }

                var groupService = new GroupService(_logger,client);
                Dictionary<string, string> retVal = groupService.GetGroups();

                _logger.LogInfo("GroupController-GetGroups: [Completed] fetching group detail from Azure AD B2C");

                return Ok(retVal);
            }
            catch (ServiceException ex)
            {
                _logger.LogError("GroupController-GetGroups: Exception occured....");
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

        // GET api/<GroupController>/5
        [HttpGet("groups/{id}", Name = "GetGroup")]
        public async Task<IActionResult> GetGroup(string id)
        {
            try
            {
                var groupModel = new GroupModel();

                if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                {
                    _logger.LogError("GroupController-GetGroup: Id cannot be empty");
                    return BadRequest();
                }

                _logger.LogInfo($"UGroupController-GetGroup: [Started] to get detail for group id {id} from Azure AD B2C");

                var client = GraphClientUtility.GetGraphServiceClient();

                if (client == null)
                {
                    _logger.LogError("GroupController-GetGroup: Unable to create object for graph client");
                    return NotFound();
                }

                var group = await client.Groups[id].Request().GetAsync();

                if (group != null)
                {
                    _logger.LogInfo($"GroupController-GetGroup: Fetched the group detail for id {id} from Azure AD B2C");
                    groupModel = GraphClientUtility.ConvertGraphGroupToGroupModel(group, _logger);

                    var members = await client.Groups[id].Members.Request().Select(e => new { e.Id }).GetAsync();
                    if(members != null)
                    {
                        groupModel.NoOfMembers = members.Count();
                    }

                    var owners = await client.Groups[id].Owners.Request().Select(e => new { e.Id }).GetAsync();
                    if (owners != null)
                    {
                        groupModel.NoOfOwners = owners.Count();
                    }
                }


                _logger.LogInfo($"GroupController-GetGroup: [Completed] to get detail for group id {id} from Azure AD B2C");
                return Ok(groupModel);

            }
            catch (ServiceException ex)
            {
                _logger.LogError("GroupController-GetGroup: Exception occured....");
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

       

        [HttpGet("groupOwners/")]
        public async Task<IActionResult> GetGroupOwners(string id)
        {
            try
            {
                var groupOwnerModel = new GroupOwnerModel();

                if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                {
                    _logger.LogError("GetGroupOwners-GetGroupMembers: Id cannot be empty");
                    return BadRequest();
                }

                _logger.LogInfo($"GetGroupOwners-GetGroupOwners: [Started] to get owner detail for group id {id} from Azure AD B2C");

                var client = GraphClientUtility.GetGraphServiceClient();

                if (client == null)
                {
                    _logger.LogError("GroupController-GetGroupMembers: Unable to create object for graph client");
                    return NotFound();
                }

                GroupService groupService = new GroupService(_logger, client);
                groupOwnerModel = await groupService.GetGroupOwnerAsync(id);


                _logger.LogInfo($"GetGroupOwners-GetGroup: [Completed] to getting owner detail for group id {id} from Azure AD B2C");
                return Ok(groupOwnerModel);

            }
            catch (ServiceException ex)
            {
                _logger.LogError("GetGroupOwners-GetGroup: Exception occured....");
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

        // POST api/<GroupController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GroupModel groupModel)
        {
            try
            {
                _logger.LogInfo("GroupController-Post: [Started] creation of group in Azure AD B2C");

                if (groupModel == null)
                {
                    _logger.LogError("GroupController-Post: Group Model cannot be null...");
                    return NotFound();
                }

                var client = GraphClientUtility.GetGraphServiceClient();
                if (client == null)
                {
                    _logger.LogError("GroupController-Post: Unable to create object for graph client ");
                    return NotFound();
                }

                var groupService = new GroupService(_logger, client);

                if (!groupService.IsGroupModelValid(groupModel))
                {
                    _logger.LogError("GroupController-Post: Display Name || Mail Nick Name is required for creating the group on Azure AD B2C");
                    return NotFound();
                }

                var group = groupService.BuildGroup(groupModel);

                var newGroup = await client.Groups.Request().AddAsync(group);

                var newUserModel = GraphClientUtility.ConvertGraphGroupToGroupModel(newGroup, _logger);

                _logger.LogInfo("GroupController-Post: [Completed] creation of group in Azure AD B2C");
                return Ok(newUserModel);

            }
            catch (Exception ex)
            {
                _logger.LogError("GroupController-Post: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }

        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        public async void Put(string id, [FromBody] GroupModel groupModel)
        {
            try
            {
                if (string.IsNullOrEmpty(id) && groupModel == null)
                {
                    _logger.LogError("GroupController-Put: Input value cannot be empty");
                    return;
                }

                _logger.LogInfo($"GroupController-Put: [Started] updating group for {id} on Azure AD B2C");

                if (groupModel == null)
                {
                    _logger.LogError("GroupController-Put: Group Model cannot be null...");
                    return;
                }

                var client = GraphClientUtility.GetGraphServiceClient();
                if (client == null)
                {
                    _logger.LogError("GroupController-Put: Unable to create object for graph client ");
                    return;
                }

                var groupService = new GroupService(_logger, client);

                if (!groupService.IsGroupModelValid(groupModel))
                {
                    _logger.LogError("GroupController-Put: Display Name || Mail Nick Name is required for creating the group on Azure AD B2C");
                    return;
                }

                var updateGroup = groupService.BuildGroup(groupModel);

                await client.Groups[id].Request().UpdateAsync(updateGroup);


                _logger.LogInfo($"GroupController-Put: [Completed] updating group id {id} on Azure AD B2C");

            }
            catch (Exception ex)
            {
                _logger.LogError("GroupController-Put: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }

        }

        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        public async void Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    _logger.LogError("GroupController-Delete: Input value cannot be empty");
                    return;
                }

                _logger.LogInfo($"GroupController-Delete: [Started] removing group for id {id} on Azure AD B2C");

                GraphServiceClient client = GraphClientUtility.GetGraphServiceClient();

                if (client == null)
                {
                    _logger.LogError("GroupController-Delete: Unable to create object for graph client");
                    return;
                }

                await client.Groups[id].Request().DeleteAsync();
                _logger.LogInfo($"GroupController-Delete: [Completed] removing for group id {id} on Azure AD B2C");
            }
            catch (Exception ex)
            {
                _logger.LogError("GroupController-Delete: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<string> groupIdsToDelete)
        {
            try
            {
                if (groupIdsToDelete == null)
                {
                    _logger.LogError("GroupController-Delete: Input value cannot be empty");
                    return NotFound();
                }

                GraphServiceClient client = GraphClientUtility.GetGraphServiceClient();
                if (client == null)
                {
                    _logger.LogError("GroupController-Delete: Unable to create object for graph client");
                    return NotFound();
                }

                foreach (var id in groupIdsToDelete)
                {
                    try
                    {
                        _logger.LogInfo($"GroupController-Delete: [Started] removing group for id [{id}] on Azure AD B2C");

                        await client.Groups[id].Request().DeleteAsync();

                        _logger.LogInfo($"GroupController-Delete: [Completed] removed group [{id}] on Azure AD B2C");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"GroupController-Delete: Exception occured while removing group for id [{id}]");
                        _logger.LogError(ex);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("GroupController-Delete: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }
    }
}
