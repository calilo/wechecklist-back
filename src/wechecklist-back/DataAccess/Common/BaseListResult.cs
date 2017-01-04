using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wechecklist_back.DataAccess.Common
{
    public class BaseListResult : BaseResult
    {
        public int _len { get; set; }

        public static List<T> PagedList<T>(IQueryable<T> q, int? start, int? size)
        {
            if (size == null)
            {
                return q.ToList();
            }
            else
            {
                return q.Skip(start ?? 0).Take(size ?? 0).ToList();
            }
        }
    }
}
