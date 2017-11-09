using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using webapi.Repository;
using webapi.Controllers;
using webapi.DataAccess;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Threading;
using System.Security.Principal;
using System.Web.Http.Results;
using System.Net;

namespace webapi.Tests.Controllers
{
    [TestClass]
    public class TasksControllerTest
    {
        int userId = 1;
        string userName = @"user1";
        int taskId = 1;
        string taskTitle = @"user title";

        User user;
        UserTask task;
        List<UserTask> taskList;

        TasksController taskController;

        Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
        Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();
        Mock<ITaskRepository> mockTaskRepo = new Mock<ITaskRepository>();

        [TestInitialize]
        public void TestInit()
        {
            //so controller picks up user
            Thread.CurrentPrincipal = new GenericPrincipal
            (
               new GenericIdentity(userName),
               new[] { "" }
            );

            user = new User()
            {
                Id = userId,
                UserName = userName
            };

            task = new UserTask()
            {
                Id = taskId,
                UserId = user.Id,
                Title = taskTitle,
                CompleteDate = DateTime.MaxValue
            };

            taskList = new List<UserTask>()
            {
                task
            };

            //setup mock objects behavior
            mockUnitOfWork.
                SetupGet(x => x.Tasks).
                Returns(mockTaskRepo.Object);
            mockUnitOfWork.
                SetupGet(x => x.Users).
                Returns(mockUserRepo.Object);
            mockUserRepo.
                Setup(x => x.GetUserId(userName)).
                Returns(userId);
            mockUserRepo.
                Setup(x => x.GetUserbyUserName(userName)).
                Returns(user);
            mockTaskRepo.
                Setup(x => x.GetTaskByUserId(taskId, userId)).
                Returns(task);
            mockTaskRepo.
                Setup(x => x.GetTasksByUserId(userId)).
                Returns(taskList);
            mockTaskRepo.
                Setup(x => x.RemoveById(taskId)).
                Returns(task);

            taskController = new TasksController(mockUnitOfWork.Object)
            {//so response messages work
                Request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(@"http://example.com")
                },
                Configuration = new HttpConfiguration()
            };
        }

        [TestMethod]
        public void TestGet()
        {
            //arrange

            //act
            var response = taskController.Get();
            var contentResult = response as 
                OkNegotiatedContentResult<IEnumerable<UserTask>>;

            //assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(taskList, contentResult.Content);
        }

        [TestMethod]
        public void TestGetNotFound()
        {
            //arrange
            mockTaskRepo.
                Setup(x => x.GetTasksByUserId(userId)).
                Returns(new List<UserTask>());

            //act
            var response = taskController.Get();

            //assert
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestGetById()
        {
            //arrange

            //act
            var response = taskController.Get(taskId);
            var contentResult = response as
                OkNegotiatedContentResult<UserTask>;

            //assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(task, contentResult.Content);
        }

        [TestMethod]
        public void TestGetByIdNotFound()
        {
            //arrange
            mockTaskRepo.
                Setup(x => x.GetTaskByUserId(taskId, userId)).
                Returns<UserTask>(null);

            //act
            var response = taskController.Get(taskId);

            //assert
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestPost()
        {
            //arrange

            //act
            var response = taskController.Post(task);
            var createdResult = response as 
                CreatedAtRouteNegotiatedContentResult<UserTask>;

            //assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(task.Id, createdResult.RouteValues["id"]);
            Assert.IsNotNull(task.InsertDate);
        }

        [TestMethod]
        public void TestPostBadRequestMissingTitle()
        {
            //arrange
            var badTask = new UserTask()
            {
                CompleteDate = DateTime.Now
            };

            //act
            var response = taskController.Post(badTask);
            var contentResult = response as
                BadRequestErrorMessageResult;

            //assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(@"Missing Title", contentResult.Message);
            Assert.IsNull(task.InsertDate);
        }

        [TestMethod]
        public void TestPostBadRequestInvalidCompleteDate()
        {
            //arrange
            var badTask = new UserTask()
            {
                Title = taskTitle
            };

            //act
            var response = taskController.Post(badTask);
            var contentResult = response as
                BadRequestErrorMessageResult;

            //assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(@"Invalid CompleteDate", contentResult.Message);
            Assert.IsNull(task.InsertDate);
        }

        [TestMethod]
        public void TestPut()
        {
            //arrange
            var putTask = new UserTask()
            {
                Id = taskId,
                UserId = user.Id,
                TaskComplete = true
            };

            //act
            var response = taskController.Put(taskId, putTask);
            var contentResult = response as 
                NegotiatedContentResult<UserTask>;

            //assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(task.Id, contentResult.Content.Id);
            Assert.IsTrue(task.TaskComplete);
        }

        [TestMethod]
        public void TestPutNotFound()
        {
            //arrange
            var badTask = new UserTask()
            {
                Id = taskId,
                UserId = user.Id,
                TaskComplete = true
            };

            mockTaskRepo.
                Setup(x => x.GetTaskByUserId(taskId, userId)).
                Returns<UserTask>(null);

            //act
            var response = taskController.Put(taskId, badTask);

            //assert
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestDelete()
        {
            //arrange

            //act
            var response = taskController.Delete(taskId);

            //assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void TestDeleteNotFound()
        {
            //arrange
            mockTaskRepo.
                Setup(x => x.GetTaskByUserId(taskId, userId)).
                Returns<UserTask>(null);

            //act
            var response = taskController.Delete(taskId);

            //assert
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }
    }
}
