using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapi.DataAccess;

namespace webapi.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserbyUserName(string userName);
        long GetUserId(string userName);
    }
}