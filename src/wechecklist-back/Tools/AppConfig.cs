using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wechecklist_back.Tools
{
    public class AppConfig
    {
        public Wechat Wechat { get; set; }
    }

    public class Wechat
    {
        public string AppID { get; set; }
        public string AppSecret { get; set; }
    }
}
