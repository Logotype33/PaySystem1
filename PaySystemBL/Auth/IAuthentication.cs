using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL.Auth
{
    public interface IAuthentication
    {
        bool IsRegistred();
        void Login(string login, string password);
        void Register(string login, string password);
    }
}
