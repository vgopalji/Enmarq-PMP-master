using Newtonsoft.Json;
using System.Collections.Generic;

namespace CareStream.Models
{
    public class UsersModel
    {
        public List<UserModel> Users { get; set; }

        public UsersModel()
        {
            Users = new List<UserModel>();
        }

        public static UserModel Parse(string JSON)
        {
            try
            {
                return JsonConvert.DeserializeObject(JSON, typeof(UserModel)) as UserModel;
            }
            catch (System.Exception ex)
            {
                //Todo: Write logger
                throw ex;
            }
        }
    }
}
