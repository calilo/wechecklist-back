using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wechecklist_back.DataAccess.Common;

namespace wechecklist_back.DataAccess.ChecklistSubscriber
{
    public class SingleResult : BaseResult
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public List<string> Pictures { get; set; } = new List<string>();
    }
}
