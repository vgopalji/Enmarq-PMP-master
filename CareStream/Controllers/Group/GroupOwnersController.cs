using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CareStream.LoggerService;
using CareStream.Models;
using CareStream.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;


namespace CareStream.API.Controllers.Group
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupOwnersController : ControllerBase
    {
        private readonly ILoggerManager _logger;

        public GroupOwnersController(ILoggerManager logger)
        {
            _logger = logger;
        }


        // GET api/<GroupOwnersController>/5
        [HttpGet("getgroupowners/{id}")]
        public async Task<IActionResult> GetGroupOwners(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                {
                    _logger.LogError("GroupOwnersController-GetGroupOwners: Id cannot be empty");
                    return BadRequest();
                }

                var groupOwnerModel = new GroupOwnerModel
                {
                    GroupId = id
                };

                _logger.LogInfo($"GroupOwnersController-GetGroupOwners: [Started] to get owner(s) detail for group id {id} from Azure AD B2C");

                var client = GraphClientUtility.GetGraphServiceClient();

                if (client == null)
                {
                    _logger.LogError("GroupOwnersController-GetGroupOwners: Unable to create object for graph client");
                    return NotFound();
                }

                GroupService groupService = new GroupService(_logger, client);
                groupOwnerModel = await groupService.GetGroupOwnerAsync(id);


                _logger.LogInfo($"GroupOwnersController-GetGroup: [Completed] to getting owner(s) detail for group id {id} from Azure AD B2C");
                return Ok(groupOwnerModel);

            }
            catch (ServiceException ex)
            {
                _logger.LogError("GroupOwnersController-GetGroupOwners: Exception occured....");
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

        // POST api/<GroupOwnersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GroupOwnerAssignModel groupOwnerAssign)
        {
            try
            {
                if (groupOwnerAssign == null)
                {
                    _logger.LogError("GroupOwnersController-Post: Group owner cannot be null...");
                    return NotFound();
                }

                _logger.LogInfo("GroupOwnersController-Post: [Started] assigning owner(s) to group in Azure AD B2C");

                if (!string.IsNullOrEmpty(groupOwnerAssign.GroupId) && groupOwnerAssign.SelectedOwners.Any())
                {
                    var client = GraphClientUtility.GetGraphServiceClient();
                    if (client == null)
                    {
                        _logger.LogError("GroupOwnersController-Post: Unable to create object for graph client ");
                        return NotFound();
                    }

                    // Get all user from Graph
                    var users = await client.Users.Request().GetAsync();
                    if (users != null)
                    {
                        var assigningUsers = users.Where(x => groupOwnerAssign.SelectedOwners.Contains(x.Id)).ToList();
                        if (assigningUsers != null)
                        {
                            foreach (var assignUser in assigningUsers)
                            {
                                try
                                {
                                    await client.Groups[groupOwnerAssign.GroupId].Owners.References.Request().AddAsync(assignUser);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"GroupOwnersController-Post: unable to assign owner [{assignUser.UserPrincipalName}] to the group [id:{groupOwnerAssign.GroupId}]");
                                    _logger.LogError(ex);
                                }
                            }
                        }
                    }
                }

                _logger.LogInfo("GroupOwnersController-Post: [Completed] creation of group in Azure AD B2C");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("GroupOwnersController-Post: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }


        // DELETE 
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]GroupOwnerAssignModel val)
        {
            try
            {
                if (val == null)
                {
                    _logger.LogError("GroupOwnersController-Delete: Input value cannot be empty");
                    return NotFound();
                }
               
                var groupId = val.GroupId;
                if (!string.IsNullOrEmpty(groupId))
                {
                    GraphServiceClient client = GraphClientUtility.GetGraphServiceClient();
                    if (client == null)
                    {
                        _logger.LogError("GroupOwnersController-Delete: Unable to create object for graph client");
                        return NotFound();
                    }

                    _logger.LogInfo($"GroupOwnersController-Delete: [Started] unassigning owner(s) for group [Id:{groupId}]");

                    foreach (var ownerId in val.SelectedOwners)
                    {
                        try
                        {
                            _logger.LogInfo($"GroupOwnersController-Delete: Removing owner [{ownerId}] from group [Id:{groupId}] on Azure AD B2C");

                            await client.Groups[groupId].Owners[ownerId].Reference.Request().DeleteAsync();

                            _logger.LogInfo($"GroupOwnersController-Delete: Removed member [{ownerId}] from group [Id:{groupId}] on Azure AD B2C");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"GroupOwnersController-Delete: Exception occured while unassigning owner [Id:{ownerId}] for group  [Id:{groupId}] ");
                            _logger.LogError(ex);
                        }
                    }

                    _logger.LogInfo($"GroupOwnersController-Delete: [Completed] unassigning owner(s) from group [Id:{val.GroupId}]");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("GroupOwnersController-Delete: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }
    }
}
