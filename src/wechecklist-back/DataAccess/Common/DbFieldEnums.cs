using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wechecklist_back.DataAccess.Common
{
    public enum EnumChecklistStatus : int
    {
        New = 10,
        Done = 20
    }

    public enum EnumUserUserType : int
    {
        NormalUser = 10,
        Group = 20
    }
}
