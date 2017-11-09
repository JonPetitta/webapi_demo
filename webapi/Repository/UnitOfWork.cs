using webapi.DataAccess;

namespace webapi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebAPIContext mContext;

        public IUserRepository Users { get; private set; }
        public ITaskRepository Tasks { get; private set; }

        public UnitOfWork()
        {
            mContext = new WebAPIContext();
            Users = new UserRepository(mContext);
            Tasks = new TaskRepository(mContext);
        }

        public void Dispose()
        {
            mContext.Dispose();
        }

        public int Save()
        {
            return mContext.SaveChanges();
        }
    }
}