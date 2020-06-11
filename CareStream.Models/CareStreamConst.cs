namespace CareStream.Models
{
    public static class CareStreamConst
    {
        public const string Owner = "Owner";
        public const string Member = "Member";
        public const string User = "User";
        public const string UserSignInType_UserPrincipalName = "UserPrincipalName";
        public const string UserSignInType_EmailAddress = "EmailAddress";
        public const string Extension = "extension";
        public const string AzureADB2C = "AzureADB2C";
        public const string Application = "application";
        public const string Applications = "applications";
        public const string ExtensionProperties = "extensionProperties";
        public const string Bearer = "Bearer";
        public const string Json = "json";
        public const string Application_Json = "application/json";
        public const string Custom = "Custom";


        public const string Dash = "-";
        public const string Underscore = "_";
        public const string ForwardSlash = "/";
        public const string Pipe = "|";
        public const string Question = "?";
        
        public const string O365 = "O365";
        public const string Security = "Security";
        public const string Unified = "Unified";
        public const string DynamicMembership = "DynamicMembership";

        public const string Member_GroupId = "MemberGroupId";
        public const string Owner_GroupId = "OwnerGroupId";

        public const string Custom_DataType_String = "String";
        public const string Custom_DataType_Boolean = "Boolean";
        public const string Custom_DataType_Int = "Integer";

        //User Custom Attributes
        public const string Schema_Extension_Id = "CareStream";

        public const string Roles_C = "Roles_C";
        public const string UserType_C = "UserType_C";
        public const string UserBusinessDepartment_C = "UserBusinessDepartment_C";
        public const string Language_C = "Language_C";
        //URLS
        public const string Base_Url = "https://localhost:44366/";
        public const string Base_API = "api/";

        //URLS for User
        public const string User_Url = "User";
        public const string Users_Detail_Url = "User/users";
        public const string Users_DropDown_Url = "User/userdropdown";


        //URLS for Group
        public const string Group_Url = "Group";
        public const string GroupsDetail_Url = "group/groupdetails";
        public const string Group_By_Id_Url = "group/groups/";

        //URLS for Group Members
        public const string GroupMember_Url = "GroupMembers";
        public const string GroupMember_By_Id_Url = "groupmembers/getgroupmembers/";

        //URLS for Group Owners
        public const string GroupOwner_Url = "GroupOwners";
        public const string GroupOwner_By_Id_Url = "groupowners/getgroupowners/";

        //URLS for Extension 
        public const string Extension_Url = "Extension";
        public const string Extension_All_Url = "extension/extensions/";

    }


}
