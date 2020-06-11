using System.Collections.Generic;
using Newtonsoft.Json;


namespace CareStream.Models
{
    /// <summary>
    /// User model 
    /// </summary>
    public class UserModel 
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "isselected", Required = Required.Default)]
        public bool  IsSelected { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /*--Additional Attributes--*/
        [JsonProperty(PropertyName = "customAttributes", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> CustomAttributes { get; set; }

        [JsonProperty(PropertyName = "groups", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Groups { get; set; }

        [JsonProperty(PropertyName = "rolesaa", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> RolesAA { get; set; }

        [JsonProperty(PropertyName = "usertypeaa", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> UserTypeAA { get; set; }

        [JsonProperty(PropertyName = "userbusinessdepartmentaa", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> UserBusinessDepartmentAA { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "usageLocation", Required = Required.Default)]
        public string UsageLocation { get; set; }

        /*----*/

        /*--Custom Attributes--*/

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "roles_c", Required = Required.Default)]
        public string Roles_C { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "userType_c", Required = Required.Default)]
        public string UserType_C { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "language_c", Required = Required.Default)]
        public string Language_C { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "userBusinessDepartment_c", Required = Required.Default)]
        public string UserBusinessDepartment_C { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "source", Required = Required.Default)]
        public string Source { get; set; }
        /*----*/


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "accountenabled", Required = Required.Default)]
        public bool? AccountEnabled { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "givenName", Required = Required.Default)]
        public string GivenName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "preferredName", Required = Required.Default)]
        public string PreferredName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "surname", Required = Required.Default)]
        public string Surname { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "displayName", Required = Required.Default)]
        public string DisplayName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "userType", Required = Required.Default)]
        public string UserType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "userPrincipalName", Required = Required.Default)]
        public string UserPrincipalName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "creationType", Required = Required.Default)]
        public string CreationType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "country", Required = Required.Default)]
        public string Country { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "companyName", Required = Required.Default)]
        public string CompanyName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "mail", Required = Required.Default)]
        public string Mail { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "mobilePhone", Required = Required.Default)]
        public string MobilePhone { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "streetAddress", Required = Required.Default)]
        public string StreetAddress { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "state", Required = Required.Default)]
        public string State { get; set; }

        [JsonProperty(PropertyName = "delegateadministration", NullValueHandling = NullValueHandling.Ignore)]
        public bool DelegateAdministration { get; set; }

        [JsonProperty(PropertyName = "forcechangepasswordnextsignin", NullValueHandling = NullValueHandling.Ignore)]
        public bool ForceChangePasswordNextSignIn { get; set; }

        [JsonProperty(PropertyName = "autogeneratepassword", NullValueHandling = NullValueHandling.Ignore)]
        public bool AutoGeneratePassword { get; set; }

        [JsonProperty(PropertyName = "manualcreatedpassword", NullValueHandling = NullValueHandling.Ignore)]
        public bool ManualCreatedPassword { get; set; }

        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "postalCode", Required = Required.Default)]
        public string PostalCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "officeLocation", Required = Required.Default)]
        public string OfficeLocation { get; set; }

        [JsonProperty(PropertyName = "signInName", NullValueHandling = NullValueHandling.Ignore)]
        public string SignInName { get; set; }

        [JsonProperty(PropertyName = "businessemail", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessEmail { get; set; }

        [JsonProperty(PropertyName = "region", NullValueHandling = NullValueHandling.Ignore)]
        public string Region { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "address2", NullValueHandling = NullValueHandling.Ignore)]
        public string Address2 { get; set; }

        [JsonProperty(PropertyName = "address3", NullValueHandling = NullValueHandling.Ignore)]
        public string Address3 { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "department", Required = Required.Default)]
        public string Department { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "jobTitle", Required = Required.Default)]
        public string JobTitle { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "businessPhones", Required = Required.Default)]
        public IEnumerable<string> BusinessPhones { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "businessPhone", Required = Required.Default)]
        public string BusinessPhone { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "mySite", Required = Required.Default)]
       public string WebSite { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "signintype", Required = Required.Default)]
        public string SignInType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "userName", Required = Required.Default)]
        public string UserName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "userDomain", Required = Required.Default)]
        public string UserDomain { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

   
}
