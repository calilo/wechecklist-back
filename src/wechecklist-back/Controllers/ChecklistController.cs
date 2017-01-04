using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using cls = wechecklist_back.DataAccess.ChecklistSubscriber;
using wechecklist_back.DataAccess;
using wechecklist_back.Tools;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace wechecklist_back.Controllers
{
    public class ChecklistController : Controller
    {
        private readonly IChecklistSubscriber _subscriber;

        public ChecklistController(IChecklistSubscriber subscriber)
        {
            _subscriber = subscriber;
        }
        [HttpGet]
        public cls.ListResult GetMyChecklists(int? start, int? size)
        {
            return _subscriber.GetMyChecklists(HttpContext.GetUserId(), start, size);
        }
        [HttpGet]
        public cls.SingleResult GetChecklist(long id)
        {
            return _subscriber.GetChecklist(HttpContext.GetUserId(), id);
        }
        [HttpPost]
        public DataAccess.Common.BaseResult FinishChecklist([FromBody]long id)
        {
            return _subscriber.FinishChecklist(HttpContext.GetUserId(), id);
        }
    }
}
