using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.URLEncryptionBusiness
{
    public interface IEncryptDecrypt
    {
        string EncryptString(string value);
        string DecryptString(string value);
    }
}
