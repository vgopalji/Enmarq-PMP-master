using CareStream.LoggerService;
using CareStream.Models;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareStream.Utility
{
    public class GroupService
    {
        private readonly ILoggerManager _logger;
        private readonly GraphServiceClient _graphServiceClient;
        public GroupService(ILoggerManager logger, GraphServiceClient graphServiceClient)
        {
            _logger = logger;
            _graphServiceClient = graphServiceClient;
        }
       

        public Dictionary<string, string> GetGroups()
        {
            Dictionary<string, string> retVal =  new Dictionary<string, string>();
            try
            {
                var tasks = FetchGroups();
                foreach(var aDict in tasks.Result)
                {
                    retVal.Add(aDict.Key, aDict.Value);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("GroupService-GetGroups: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
            return retVal;
        }

        public async Task<List<GroupModel>> GetGroupDetails()
        {
            List<GroupModel> retVal = new List<GroupModel>();
            try
            {
                _logger.LogInfo("GroupService-GetGroups: Getting group detail from Azure AD B2C");

                var groupDetails = await _graphServiceClient.Groups.Request().GetAsync();

                if (groupDetails != null)
                {
                    foreach (var groupItem in groupDetails)
                    {
                        try
                        {

                            var groupModel= GraphClientUtility.ConvertGraphGroupToGroupModel(groupItem, _logger);
                          
                            if(groupItem.GroupTypes != null)
                            {
                                if (groupItem.GroupTypes.Any())
                                {
                                    groupModel.GroupType = string.Join('|', groupItem.GroupTypes);
                                }
                            }
                            retVal.Add(groupModel);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"GroupService-GetGroups: failed to add group for group name {groupItem.DisplayName}");
                            _logger.LogError(ex);
                        }
                       
                    }
                    retVal = retVal.OrderBy(x => x.DisplayName).ToList();
                }

                _logger.LogInfo("GroupService-GetGroups: Completed getting the group detail from Azure AD B2C");
            }
            catch (Exception ex)
            {
                _logger.LogError("GroupService-GetGroups: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
            return retVal;
        }

        public async Task<GroupOwnerModel> GetGroupOwnerAsync(string groupId)
        {
            GroupOwnerModel retVal = null;
            try
            {
                if (string.IsNullOrEmpty(groupId))
                {
                    _logger.LogError("GroupService-GetGroupOwnerAsync: Group Id cannot be null");
                    return retVal;

                }
                _logger.LogInfo("GroupService-GetGroupOwnerAsync: Starting to get group owner from Azure AD B2C");


                retVal = new GroupOwnerModel
                {
                    GroupId = groupId
                };

                var tasks = new Task[]
                {
                    Task.Run(() => {
                                var ownersList = GetNewGroupDefaultOwnerMember();
                                if (ownersList != null)
                                {
                                    retVal.Owners = new Dictionary<string, string>();
                                    foreach(var aDict in ownersList.Result)
                                    {
                                        retVal.Owners.Add(aDict.Key, aDict.Value);
                                    }
                                }
                    }),
                    Task.Run(() =>
                    {
                        var assignedOwners = GetOwnersForGroup(groupId);
                        if(assignedOwners != null)
                        {
                            retVal.AssignedOwners = assignedOwners.Result.ToList<UserModel>();
                        }
                    })
                };

                await Task.WhenAll(tasks);

                if (retVal != null)
                {
                    if ((retVal.AssignedOwners != null) && (retVal.Owners != null))
                    {
                        var userIds = retVal.AssignedOwners.Select(x => x.Id).ToList();
                        retVal.Owners = FilterGroupOwnerMember(userIds, retVal.Owners);
                    }
                }

                _logger.LogInfo("GroupService-GetGroupOwnerAsync: Completed getting the group owner from Azure AD B2C");
            }
            catch (Exception ex)
            {
                _logger.LogError("GroupService-GetGroupOwnerAsync: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
            return retVal;
        }

        public async Task<GroupMemberModel> GetGroupMemberAsync(string groupId)
        {
            GroupMemberModel retVal = null;
            try
            {
                if (string.IsNullOrEmpty(groupId))
                {
                    _logger.LogError("GroupService-GetGroupMemberAsync: Group Id cannot be null");
                    return retVal;

                }
                _logger.LogInfo("GroupService-GetGroupMemberAsync: Starting to get group owner from Azure AD B2C");


                retVal = new GroupMemberModel
                {
                    GroupId = groupId
                };

                var tasks = new Task[]
                {
                    Task.Run(() => {
                                var membersList = GetNewGroupDefaultOwnerMember();
                                if (membersList != null)
                                {
                                    retVal.Members = new Dictionary<string, string>();
                                    foreach(var aDict in membersList.Result)
                                    {
                                        retVal.Members.Add(aDict.Key, aDict.Value);
                                    }
                                }
                    }),
                    Task.Run(() =>
                    {
                        var assignedMembers = GetMembersForGroup(groupId);
                        if(assignedMembers != null)
                        {
                            retVal.AssignedMembers = assignedMembers.Result.ToList<UserModel>();
                        }
                    })
                };

                await Task.WhenAll(tasks);

                if(retVal != null)
                {
                    if ((retVal.AssignedMembers != null) && (retVal.Members != null)){
                       var userIds = retVal.AssignedMembers.Select(x => x.Id).ToList();
                        retVal.Members = FilterGroupOwnerMember(userIds, retVal.Members);
                    }
                }
                _logger.LogInfo("GroupService-GetGroupMemberAsync: Completed getting the group owner from Azure AD B2C");
            }
            catch (Exception ex)
            {
                _logger.LogError("GroupService-GetGroupMemberAsync: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
            return retVal;
        }

        private Dictionary<string,string> FilterGroupOwnerMember(List<string> userIds, Dictionary<string,string> userDic)
        {
            Dictionary<string, string> retVal = null;
            try
            {
                if (userDic.Any() && userIds.Any())
                {
                    var keys = userDic.Keys.Where(x => userIds.Contains(x)).ToList();
                    foreach (var key in keys)
                    {
                        userDic.Remove(key);
                    }
                }
                retVal = userDic;
            }
            catch (Exception ex)
            {
                _logger.LogError("GroupService-FilterGroupOwnerMember: Exception occured....");
                _logger.LogError(ex);
            }
            return retVal;
        }

        private async Task<Dictionary<string,string>> GetNewGroupDefaultOwnerMember()
        {
            Dictionary<string, string> retVal = null;
            try
            {
                var users = await _graphServiceClient.Users.Request().GetAsync();

                if (users != null)
                {
                    retVal = new Dictionary<string, string>();
                    foreach (var user in users.Where(x => !string.IsNullOrEmpty(x.UserPrincipalName)))
                    {
                        try
                        {
                            if (!retVal.ContainsKey(user.Id))
                            {
                                retVal.Add(user.Id, user.UserPrincipalName);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"GroupService-GetGroupOwnerMemberAsync: unable to add user in the group owner and member collection for user {user.UserPrincipalName}");
                            _logger.LogError(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GroupService-GetNewGroupDefaultOwnerMember: Exception occured....");
                _logger.LogError(ex);
            }
            return retVal;
        }

        private async Task<List<UserModel>> GetOwnersForGroup(string groupId)
        {
            List<UserModel> retVal = new List<UserModel>();
            try
            {
                var owners = await _graphServiceClient.Groups[groupId].Owners.Request().GetAsync();
                if(owners != null)
                {

                    foreach (var owner in owners)
                    {
                        try
                        {
                            if(!(owner is User))
                            {
                                continue;
                            }

                            UserModel userModel = GraphClientUtility.ConvertGraphUserToUserModel((User)owner, _logger);
                            retVal.Add(userModel);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"GroupService-GetOwnersForGroupGetOwnersForGroup: Error adding group owner to the collection for owner {owner.Id}");
                            _logger.LogError(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GroupService-GetOwnersForGroup: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
            return retVal;
        }

        private async Task<List<UserModel>> GetMembersForGroup(string groupId)
        {
            List<UserModel> retVal = new List<UserModel>();
            try
            {
                var members = await _graphServiceClient.Groups[groupId].Members.Request().GetAsync();
                if (members != null)
                {

                    foreach (var member in members)
                    {
                        try
                        {
                            if (!(member is User))
                            {
                                continue;
                            }

                            UserModel userModel = GraphClientUtility.ConvertGraphUserToUserModel((User)member, _logger);
                            retVal.Add(userModel);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"GroupService-GetMembersForGroup: Error adding group owner to the collection for owner {member.Id}");
                            _logger.LogError(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GroupService-GetMembersForGroup: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
            return retVal;
        }
     
        public Group BuildGroup(GroupModel groupModel)
        {
            Group retVal = null;
            try
            {
                _logger.LogInfo($"GroupService-BuildGroup: Building group Object for {groupModel.DisplayName}");
                if (groupModel == null)
                {
                    _logger.LogError("GroupService-BuildGroup: Input value cannot be null");
                    return retVal;
                }

                retVal = new Group
                {
                    MailNickname = groupModel.MailNickname,
                    DisplayName = groupModel.DisplayName,
                    Visibility = groupModel.Visibility,
                    MailEnabled = groupModel.MailEnabled,
                    SecurityEnabled = groupModel.SecurityEnabled,
                    GroupTypes = groupModel.GroupTypes,
                    Description = groupModel.Description,
                };

                //owner and members
                if (groupModel.AdditionalData != null)
                {
                    if (groupModel.AdditionalData.Count > 0)
                    {
                        retVal.AdditionalData = groupModel.AdditionalData;
                    }
                }


                _logger.LogInfo($"GroupService-BuildGroup: Completed building group Object for {groupModel.DisplayName}");
            }
            catch (Exception ex)
            {
                _logger.LogError("GroupService-BuildGroup: Exception occured....");
                _logger.LogError(ex);
            }

            return retVal;
        }

        public bool IsGroupModelValid(GroupModel groupModel)
        {
            if (groupModel == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(groupModel.DisplayName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(groupModel.MailNickname))
            {
                return false;
            }

            return true;
        }

        private async Task<IDictionary<string,string>> FetchGroups()
        {
            Dictionary<string, string> retVal = null;
            try
            {
                _logger.LogInfo("FetchGroups-GetGroups: Getting group detail from Azure AD B2C");

                var groupList = await _graphServiceClient.Groups.Request().Select(e => new { e.DisplayName, e.Id }).GetAsync();

                if (groupList != null)
                {
                    _logger.LogInfo($"Found {groupList.Count} group from Azure AD B2C");
                    retVal = new Dictionary<string, string>();
                    foreach (var group in groupList.OrderBy(x => x.DisplayName))
                    {
                        if (!retVal.ContainsKey(group.DisplayName))
                        {
                            retVal.Add(group.DisplayName, group.Id);
                        }
                    }
                }

                _logger.LogInfo("FetchGroups-GetGroups: Completed getting the group detail from Azure AD B2C");
            }
            catch (Exception ex)
            {
                _logger.LogError("FetchGroups-GetGroups: Exception occured....");
                _logger.LogError(ex);
                throw ex;
            }
            return retVal;
        }
        
    }
}
