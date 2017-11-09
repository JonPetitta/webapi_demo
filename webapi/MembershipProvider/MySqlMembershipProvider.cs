using BasicAuthForWebAPI;
using webapi.DataAccess;
using webapi.Repository;

namespace webapi.MembershipProvider
{
    public class MySqlMembershipProvider : IMembershipProvider
    {
        private readonly IUserRepository mUserRepo;

        public MySqlMembershipProvider(IUnitOfWork unitOfWork)
        {
            mUserRepo = unitOfWork.Users;
        }

        //called after validation
        public string[] GetRolesForUser(string userName)
        {
            //no roles in this application
            return new string[0];
        }

        //called after validation
        public MembershipProviderUser GetUser(string userName)
        {
            //user has to exist as we already validated
            var user = mUserRepo.GetUserbyUserName(userName);

            return new MembershipProviderUser()
            {
                UserId = user.Id.ToString(),
                Username = user.UserName,
                Email = @""
            };
        }

        public bool ValidateUser(string userName, string password)
        {
            if (string.Empty == userName ||
                string.Empty == password)
            {
                return false;
            }

            var user = mUserRepo.GetUserbyUserName(userName);

            if (password == user.Password)
            {
                return true;
            }

            return false;
        }
    }
}