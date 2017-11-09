using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using webapi.MembershipProvider;
using Moq;
using webapi.Repository;
using webapi.DataAccess;

namespace webapi.Tests.MembershipProvider
{
    [TestClass]
    public class MySqlMembershipProviderTest
    {
        int userId = 1;
        string userName = @"user1";
        string password = @"password";

        User user;
        MySqlMembershipProvider mySqlMembershipProvider;

        Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
        Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();

        [TestInitialize]
        public void TestInit()
        {
            user = new User()
            {
                Id = userId,
                UserName = userName,
                Password = password
            };
            
            mockUnitOfWork.
                SetupGet(x => x.Users).
                Returns(mockUserRepo.Object);
            mockUserRepo.
                Setup(x => x.GetUserbyUserName(userName)).
                Returns(user);

            mySqlMembershipProvider = 
                new MySqlMembershipProvider(mockUnitOfWork.Object);
        }

        [TestMethod]
        public void TestGetRolesForUser()
        {
            //arrange
            //act
            var response = mySqlMembershipProvider.GetRolesForUser(userName);
            //assert
            Assert.AreEqual(0, response.Length);
        }

        [TestMethod]
        public void TestGetUser()
        {
            //arrange
            //act
            var response = mySqlMembershipProvider.GetUser(userName);
            //assert
            Assert.AreEqual(userId.ToString(), response.UserId);
            Assert.AreEqual(userName, response.Username);
        }

        [TestMethod]
        public void TestValidateUser()
        {
            //arrange
            //act
            var response = mySqlMembershipProvider.
                             ValidateUser(userName, password);
            //assert
            Assert.IsTrue(response);
        }

        [TestMethod]
        public void TestValidateUserMissingUserName()
        {
            //arrange
            //act
            var response = mySqlMembershipProvider.
                             ValidateUser(string.Empty, password);
            //assert
            Assert.IsFalse(response);
        }

        [TestMethod]
        public void TestValidateUserMissingPassword()
        {
            //arrange
            //act
            var response = mySqlMembershipProvider.
                             ValidateUser(userName, string.Empty);
            //assert
            Assert.IsFalse(response);
        }
    }
}
