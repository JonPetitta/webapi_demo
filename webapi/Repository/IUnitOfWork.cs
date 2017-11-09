using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITaskRepository Tasks { get; }
        int Save();
    }
}