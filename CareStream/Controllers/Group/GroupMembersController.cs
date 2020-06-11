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
    public class GroupMembersController : ControllerBase
    {
        private readonly ILoggerManager _logger;

        public GroupMembersController(ILoggerManager logger)
        {
            _logger = logger;
        }


        // GET api/<GroupMembersController>/5
        [HttpGet("getgroupmembers/{id}")]
        public async Task<IActionResult> GetGroupMembers(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                {
                    _logger.LogError("GroupMembersController-GetGroupMembers: Id cannot be empty");
                    return BadRequest();
                }

                var groupMemberModel = new GroupMemberModel
                {
                    GroupId = id
                };

                _logger.LogInfo($"GroupMembersController-GetGroupMembers: [Started] to get member detail for group id {id} from Azure AD B2C");

                var client = GraphClientUtility.GetGraphServiceClient();

                if (client == null)
                {
                    _logger.LogError("GroupMembersController-GetGroupMembers: Unable to create object for graph client");
                    return NotFound();
                }

                GroupService groupService = new GroupService(_logger, client);
                groupMemberModel = await groupService.GetGroupMemberAsync(id);

                _logger.LogInfo($"GroupMembersController-GetGroup: [Completed] to getting member detail for group id {id} from Azure AD B2C");
                return Ok(groupMemberModel);

            }
            catch (ServiceException ex)
            {
                _logger.LogError("GroupMembersController-GetGroup: Exception occured....");
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

        // POST api/<GroupMembersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GroupMemberAssignModel groupMemberAssignModel)
        {
            try
            {
                if (groupMemberAssignModel == null)
                {
                    _logger.LogError("GroupMembersController-Post: Group Member cannot be null...");
                    return NotFound();
                }

                _logger.LogInfo("GroupMembersController-Post: [Started] assigning member to group in Azure AD B2C");

                if (!string.IsNullOrEmpty(groupMemberAssignModel.GroupId) && groupMemberAssignModel.SelectedMembers.Any())
                {
                    var client = GraphClientUtility.GetGraphServiceClient();
                    if (client == null)
                    {
                        _logger.LogError("GroupMembersController-Post: Unable to create object for graph client ");
                        return NotFound();
                    }

                    // Get all user from Graph
                    var users = await client.Users.Request().GetAsync();
                    if (users != null)
                    {
                        var assigningUsers = users.Where(x => groupMemberAssignModel.SelectedMembers.Contains(x.Id)).ToList();
                        if (assigningUsers != null)
                        {
                            foreach (var assignUser in assigningUsers)
                            {
                                try
                                {
                                    await client.Groups[groupMemberAssignModel.GroupId].Members.References.Request().AddAsync(assignUser);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"GroupMembersController-Post: unable to assign member [{assignUser.UserPrincipalName}] to the group [id:{groupMemberAssignModel.GroupId}]");
                                    _logger.LogError(ex);
                                }
                            }
                        }
                    }
                }

                _logger.LogInfo("GroupMembersController-Post: [Completed] assigning member(s) to group in Azure AD B2C");
                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError("GroupMembersController-Post: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }


        // DELETE 
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]GroupMemberAssignModel val)
        {
            try
            {
                if (val == null)
                {
                    _logger.LogError("GroupMembersController-Delete: Input value cannot be empty");
                    return NotFound();
                }
               

                var groupId = val.GroupId;
                if (!string.IsNullOrEmpty(groupId))
                {
                    GraphServiceClient client = GraphClientUtility.GetGraphServiceClient();
                    if (client == null)
                    {
                        _logger.LogError("GroupMembersController-Delete: Unable to create object for graph client");
                        return NotFound();
                    }

                    _logger.LogInfo($"GroupMembersController-Delete: [Started] unassigning member(s) for group [Id:{groupId}]");

                    foreach (var memberId in val.SelectedMembers)
                    {
                        try
                        {
                            _logger.LogInfo($"GroupMembersController-Delete: Removing member [{memberId}] from group [Id:{groupId}] on Azure AD B2C");

                            await client.Groups[groupId].Members[memberId].Reference.Request().DeleteAsync();

                            _logger.LogInfo($"GroupMembersController-Delete: Removed member [{memberId}] from group [Id:{groupId}] on Azure AD B2C");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"GroupMembersController-Delete: Exception occured while unassigning member [Id:{memberId}] for group  [Id:{groupId}] ");
                            _logger.LogError(ex);
                        }
                    }

                    _logger.LogInfo($"GroupMembersController-Delete: [Completed] unassigning member(s) from group [Id:{val.GroupId}]");
                }
                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError("GroupMembersController-Delete: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }

    }
}
