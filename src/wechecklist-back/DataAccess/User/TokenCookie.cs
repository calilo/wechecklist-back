using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wechecklist_back.DataAccess.User
{
    public class TokenCookie
    {
        public long UserId { get; set; }
        public string Token { get; set; }
    }
}
