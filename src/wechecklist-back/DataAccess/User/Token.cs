using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wechecklist_back.DataAccess.User
{
    public class Token
    {
        public long UserId { get; set; }
        public string TokenString { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
