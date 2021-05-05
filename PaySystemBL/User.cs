using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL
{
    public class User
    {
        public static int Id { get; private set; }
        public static bool IsAuthorized { get; private set; } = false;
        public User(int id, bool isAut)
        {
            User.Id = id;
            User.IsAuthorized = isAut;
        }
    }
}
