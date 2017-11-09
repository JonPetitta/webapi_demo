using System.Linq;
using webapi.DataAccess;

namespace webapi.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private static WebAPIContext mWebAPIContext = new WebAPIContext();

        public UserRepository(WebAPIContext context)
            : base(context)
        {
            mWebAPIContext = context;
        }

        public User GetUserbyUserName(string userName)
        {
            return mWebAPIContext.Users.
                                  Where(u => u.UserName == userName).
                                  FirstOrDefault();
        }

        public long GetUserId(string userName)
        {
            return GetUserbyUserName(userName).Id;
        }
    }
}