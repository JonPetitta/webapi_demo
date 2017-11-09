using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using webapi.DataAccess;
using webapi.Repository;

namespace webapi.Controllers
{
    public class TasksController : ApiController
    {
        private IUnitOfWork mUnitOfWork;
        private IUserRepository mUserRepo;
        private ITaskRepository mTaskRepo;
        private long mUserId;
        
        public TasksController(IUnitOfWork unitOfWork)
        {
            mUnitOfWork = unitOfWork;
            mUserRepo = mUnitOfWork.Users;
            mTaskRepo = mUnitOfWork.Tasks;

            mUserId = mUnitOfWork.Users.GetUserId(
                        this.User.Identity.Name);
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            //get all tasks for current user
            var tasks = mTaskRepo.GetTasksByUserId(mUserId);
            
            if (0 == tasks.Count())
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            //get a task for current user
            var task = mTaskRepo.GetTaskByUserId(id, mUserId);

            if (null == task)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]UserTask value)
        {
            //validation
            if (string.Empty == value.Title ||
                null == value.Title)
            {
                return BadRequest(@"Missing Title");
            }
            if (DateTime.Now > value.CompleteDate ||
                null == value.CompleteDate)
            {
                return BadRequest(@"Invalid CompleteDate");
            }

            //create task
            value.UserId = mUserId;
            value.InsertDate = DateTime.Now;
            mTaskRepo.Add(value);

            mUnitOfWork.Save();

            return CreatedAtRoute(@"DefaultApi", new { id = value.Id }, value);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]UserTask value)
        {
            //update task
            var task = mTaskRepo.GetTaskByUserId(id, mUserId);

            if (null == task)
            {
                return NotFound();
            }

            task.TaskComplete = value.TaskComplete;

            mUnitOfWork.Save();

            return Content(HttpStatusCode.Accepted, task);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            //make sure task is related to user
            var task = mTaskRepo.GetTaskByUserId(id, mUserId);

            if (null == task)
            {
                return NotFound();
            }

            var removedTask = mTaskRepo.RemoveById(id);

            mUnitOfWork.Save();

            return  Ok();
        }
    }
}