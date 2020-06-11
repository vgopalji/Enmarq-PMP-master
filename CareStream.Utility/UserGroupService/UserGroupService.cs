using CareStream.LoggerService;
using CareStream.Models;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareStream.Utility
{
    public class UserGroupService
    {
        private readonly ILoggerManager _logger;
        private readonly GraphServiceClient client;
        public UserGroupService(ILoggerManager logger, GraphServiceClient graphServiceClient)
        {
            _logger = logger;
            client = graphServiceClient;
        }

        public async Task<List<GroupAssigned>> AssignUserToGroup(UserGroup userGroup)
        {
            List<GroupAssigned> retVal = null;
            try
            {
                _logger.LogInfo("UserGroupController-AssignUserToGroup: [Started] assigning group for user in Azure AD B2C");

                var newUser = userGroup.User;
                var allGroup = userGroup.AllGroups;

                retVal = new List<GroupAssigned>();
                foreach (var group in userGroup.Groups)
                {
                    var groupAssigned = new GroupAssigned();
                    try
                    {
                        groupAssigned.GroupName = group;

                        if (group.StartsWith($"{CareStreamConst.Owner}{CareStreamConst.Dash}"))
                            await client.Groups[allGroup[group.Replace($"{CareStreamConst.Owner}{CareStreamConst.Dash}", "")]].Owners.References.Request().AddAsync(newUser);
                        else if (group.StartsWith($"{CareStreamConst.Member}{CareStreamConst.Dash}"))
                            await client.Groups[allGroup[group.Replace($"{CareStreamConst.Member}{CareStreamConst.Dash}", "")]].Members.References.Request().AddAsync(newUser);
                        else
                            await client.Groups[allGroup[group.Replace($"{CareStreamConst.Member}{CareStreamConst.Dash}", "")]].Members.References.Request().AddAsync(newUser);

                        groupAssigned.IsGroupAssigned = true;
                    }
                    catch (Exception ex)
                    {
                        groupAssigned.IsGroupAssigned = false;
                        _logger.LogError($"UserGroupController-Post: Unable to assign group {group} for user {newUser.DisplayName} - Id: {newUser.Id}");
                        _logger.LogError(ex);
                    }
                    retVal.Add(groupAssigned);

                }

                _logger.LogInfo("UserGroupController-AssignUserToGroup: [Completed] assigning group for user in Azure AD B2C");
                
            }
            catch (Exception ex)
            {
                _logger.LogError("UserGroupController-Post: Exception occured....");
                _logger.LogError(ex);
            }
            return retVal;
        }
    }
}
