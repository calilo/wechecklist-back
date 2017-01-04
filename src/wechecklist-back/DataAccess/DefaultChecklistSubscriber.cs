using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wechecklist_back.DataAccess.ChecklistSubscriber;
using wechecklist_back.DataAccess.Common;
using wechecklist_back.ORM;

namespace wechecklist_back.DataAccess
{
    public class DefaultChecklistSubscriber : IChecklistSubscriber
    {
        private readonly ChecklistDBContext _dbContext;
        public DefaultChecklistSubscriber(ChecklistDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        BaseResult IChecklistSubscriber.FinishChecklist(long userId, long id)
        {
            try
            {
                var checklist = _dbContext.Checklists.FirstOrDefault(c => c.SubscribeUserId == userId && c.ChecklistId == id);
                if (checklist == null)
                {
                    return new BaseResult { Success = false };
                }
                checklist.Status = (int)EnumChecklistStatus.Done;
                _dbContext.SaveChanges();
                return new BaseResult { Success = true };
            }
            catch
            {
                return new BaseResult { Success = false };
            }
        }

        SingleResult IChecklistSubscriber.GetChecklist(long userId, long id)
        {
            try
            {
                var query = from c in _dbContext.Checklists
                            join pu in _dbContext.Users on c.PublishUserId equals pu.UserId into puTmp
                            from pu in puTmp.DefaultIfEmpty()
                            where c.ChecklistId == id && c.SubscribeUserId == userId
                            select new SingleResult
                            {
                                Success = true,
                                Id = c.ChecklistId,
                                Author = pu.UserName,
                                Description = c.MarkDown,
                                Title = c.Title,
                                Pictures = new List<string>()
                            };
                var checklist = query.FirstOrDefault();
                if (checklist != null)
                {
                    return checklist;
                }
            }
            catch { }
            return new SingleResult { Success = false };
        }

        ListResult IChecklistSubscriber.GetMyChecklists(long userId, int? start, int? size)
        {
            try
            {
                var query = from c in _dbContext.Checklists
                            join pu in _dbContext.Users on c.PublishUserId equals pu.UserId into puTmp
                            from pu in puTmp.DefaultIfEmpty()
                            where c.SubscribeUserId == userId
                            orderby c.CreateTime descending
                            select new ListResultItem
                            {
                                Id = c.ChecklistId,
                                Title = c.Title,
                                Description = c.MarkDown,
                                Status = c.Status,
                                Author = pu.UserName,
                                HeadUrl = pu.HeadImgUrl,
                                PublishTime = c.CreateTime
                            };
                return new ListResult { Success = true, _len = query.Count(), list = BaseListResult.PagedList(query, start, size) };
            }
            catch
            {
                return new ListResult { Success = false };
            }
        }
    }
}
