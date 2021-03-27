
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services.Interfaces
{
    public interface IPasswordService
    {
        string HashCreate(string value, string salt);
        bool HashValidate(string value, string salt, string hash);

        string SaltCreate();

    }
}
