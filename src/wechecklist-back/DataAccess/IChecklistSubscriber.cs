using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wechecklist_back.DataAccess.Common;

namespace wechecklist_back.DataAccess
{
    public interface IChecklistSubscriber
    {
        ChecklistSubscriber.ListResult GetMyChecklists(long userId, int? start, int? size);
        ChecklistSubscriber.SingleResult GetChecklist(long userId, long id);
        BaseResult FinishChecklist(long userId, long id);

    }
}
