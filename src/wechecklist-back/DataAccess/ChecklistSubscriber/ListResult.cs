using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wechecklist_back.DataAccess.Common;

namespace wechecklist_back.DataAccess.ChecklistSubscriber
{
    public class ListResult : BaseListResult
    {
        public List<ListResultItem> list { get; set; } = new List<ListResultItem>();
    }
    public class ListResultItem
    {
        public long Id { get; set; }
        public string HeadUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime PublishTime { get; set; }
        public int Status { get; set; }
    }
}
