using System.Collections.Generic;
using System.Linq;
using webapi.DataAccess;

namespace webapi.Repository
{
    public class TaskRepository : Repository<UserTask>, ITaskRepository
    {
        private static WebAPIContext mWebAPIContext = new WebAPIContext();

        public TaskRepository(WebAPIContext context)
            : base(context)
        {
            mWebAPIContext = context;
        }

        public UserTask GetTaskByUserId(long taskId, long userId)
        {
            return mWebAPIContext.UserTasks.
                                  SingleOrDefault(t => 
                                      (t.Id ==taskId && t.UserId == userId));
        }

        public IEnumerable<UserTask> GetTasksByUserId(long userId)
        {
            return mWebAPIContext.UserTasks.
                                  Where(t => t.UserId == userId).
                                  ToArray();
        }
    }
}