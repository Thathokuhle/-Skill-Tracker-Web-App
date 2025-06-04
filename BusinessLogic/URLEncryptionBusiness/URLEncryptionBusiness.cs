using BusinessLogic.AppSettings;

namespace BusinessLogic.URLEncryptionBusiness
{
    public class UrlEncryptionBusiness
    {
        private readonly IEncryptDecrypt _encryptDecrypt;

        public UrlEncryptionBusiness(IEncryptDecrypt encryptDecrypt)
        {
            _encryptDecrypt = encryptDecrypt;
        }   
        public static string EncryptParam(string value)
        {
            return new EncryptDecrypt(AppSettings.AppSettings.GetUrLEncryptionKey()).EncryptString(value);
        }

        public static string DecryptParam(string value)
        {
            return new EncryptDecrypt(AppSettings.AppSettings.GetUrLEncryptionKey()).DecryptString(value);
        }
    }
}
