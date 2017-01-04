using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wechecklist_back.Tools
{
    public static class Common
    {
        public static long GetUserId(this HttpContext context)
        {
            return (long)context.Items["UserId"];
        }
    }
}
