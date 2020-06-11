using System;
using System.Linq;
using CareStream.LoggerService;
using CareStream.Models;
using CareStream.Utility;
using Microsoft.AspNetCore.Mvc;

namespace CareStream.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly ILoggerManager _logger;

        public UserGroupController(ILoggerManager logger)
        {
            _logger = logger;
        }
      

        // GET api/<UserGroupController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserGroupController>
        [HttpPost]
        public IActionResult Post([FromBody]UserGroup userGroup)
        {
            try
            {
                if(userGroup == null)
                {
                    _logger.LogInfo("UserGroupController-Post: Input value is null....");
                    return NotFound();
                }
                _logger.LogInfo("UserGroupController-Post: [Started] assigning group for user in Azure AD B2C");

                //validation of properites 

                if (!IsValid(userGroup))
                {
                    _logger.LogInfo("UserGroupController-Post: Invalid data in user group");
                    return NotFound();
                }

                var client = GraphClientUtility.GetGraphServiceClient();
                if (client == null)
                {
                    _logger.LogError("UserGroupController-Post: Unable to create object for graph client ");
                    return NotFound();
                }

                var userGroupService = new UserGroupService(_logger, client);
                var assignResult = userGroupService.AssignUserToGroup(userGroup);

                if(assignResult != null)
                {
                    var groupAssignedResults = assignResult.Result.ToList<GroupAssigned>();

                    if (groupAssignedResults.Any(x => x.IsGroupAssigned == false))
                    {
                        _logger.LogWarn($"UserService-AssignGroupsToUser: Failed to assign one or more group for User Id: {userGroup.User.Id}, see the detail above");
                    }
                
                }

                _logger.LogInfo("UserGroupController-Post: [Completed] assigning group for user in Azure AD B2C");
                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError("UserGroupController-Post: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
        }

        private bool IsValid(UserGroup userGroup)
        {
            if (userGroup.User == null)
            {
                return false;
            }

            if (userGroup.Groups == null)
            {
                return false;
            }

            if (userGroup.AllGroups == null)
            {
                return false;
            }


            return true;
        }

        // PUT api/<UserGroupController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserGroupController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
