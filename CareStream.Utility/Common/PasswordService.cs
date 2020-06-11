using CareStream.LoggerService;
using System;

namespace CareStream.Utility
{
    public  class PasswordService
    {
        public static ILoggerManager Logger { get; set; }


        public static string GenerateNewPassword(int lowercase, int uppercase, int numerics, int specialChars)
        {
            var retVal = string.Empty;
            try
            {
                string lowers = "abcdefghijklmnopqrstuvwxyz";
                string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string number = "0123456789";
                string specialChar = "~!@#$%^&*_-+=`|(){}[]:;“‘<>,.?";

                Random random = new Random();

                string generated = "!";
                for (int i = 1; i <= lowercase; i++)
                    generated = generated.Insert(
                        random.Next(generated.Length),
                        lowers[random.Next(lowers.Length - 1)].ToString()
                    );

                for (int i = 1; i <= uppercase; i++)
                    generated = generated.Insert(
                        random.Next(generated.Length),
                        uppers[random.Next(uppers.Length - 1)].ToString()
                    );

                for (int i = 1; i <= numerics; i++)
                    generated = generated.Insert(
                        random.Next(generated.Length),
                        number[random.Next(number.Length - 1)].ToString()
                    );

                for (int i = 1; i <= specialChars; i++)
                    generated = generated.Insert(
                        random.Next(generated.Length),
                        specialChar[random.Next(number.Length - 1)].ToString()
                    );

                retVal = generated.Replace("!", string.Empty); 
            }
            catch (Exception ex)
            {
                Logger.LogError("PasswordService-GenerateNewPassword: Exception occurred...");
                Logger.LogError(ex);
            }
            return retVal;
        }

    }
}
