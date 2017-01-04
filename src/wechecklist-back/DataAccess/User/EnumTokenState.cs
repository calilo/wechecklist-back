using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wechecklist_back.DataAccess.User
{
    public enum EnumTokenState : int
    {
        Valid = 1,
        Refresh = 2,
        Expired = 3,
        NoUser = 4
    }
}
