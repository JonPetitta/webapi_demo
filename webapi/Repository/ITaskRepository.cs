using System.Collections.Generic;
using webapi.DataAccess;

namespace webapi.Repository
{
    public interface ITaskRepository : IRepository<UserTask>
    {
        UserTask GetTaskByUserId(long taskId, long userId);
        IEnumerable<UserTask> GetTasksByUserId(long userId);
    }
}