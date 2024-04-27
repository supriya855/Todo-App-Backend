using Microsoft.AspNetCore.Mvc;
using TodoApplicationWebAPI.Services;
using TodoApplicationWebAPI.Model;
using System.Xml;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace TodoApplicationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly ApplicationDBContext _dbContext;
        public TodoController(ILogger<TodoController> logger, ApplicationDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;

        }
        BaseResponse<TodoModel> result = new();
        [HttpGet]
        //public ActionResult<List<TodoModel>> Get() =>
        //   _dbContext.Get();
        public BaseResponse<List<TodoModel>> Get()
        {
            BaseResponse<List<TodoModel>> res = new();
            var todos = _dbContext.GetAllTodos();
            if (todos != null)
            {
                res.ResponseCode = "OK";
                res.ResponseMessage = "Success";
                res.ResultsetData = todos;
            }
            else
            {
                res.ResponseCode = "KO";
                res.ResponseMessage = "Failure";
                res.ResultsetData = todos;
            }
            return res;
        }

        [HttpPost]
        public BaseResponse<TodoModel> Create(TodoModel input)
        {
            TodoModel response = new();
            
            response = _dbContext.Create(input);
            if (response == null)
            {
                result.ResponseCode = "KO";
                result.ResponseMessage = "Failure";
                result.ResultsetData = response;
            }
            _logger.LogInformation("Succesfully input added");
            result.ResponseCode = "OK";
            result.ResponseMessage = "Success";
            result.ResultsetData = response;
            return result;

           
        }
        [HttpPut("{id:length(24)}")]
        public BaseResponse<TodoModel> Update(string id, TodoModel upInput)
        {
            TodoModel response = new();
            var task = _dbContext.Get(id);
            if (task == null)
            {
               _logger.LogInformation("Unable to fetch the id to update in DB");
                result.ResponseCode = "KO";
                result.ResponseMessage = "Failure";
                result.ResultsetData = response;
            }
            response = _dbContext.Update(id, upInput);
            if (response!= null)
             {
                 _logger.LogInformation("Succesfully upinput updated");
                 result.ResponseCode = "OK";
                 result.ResponseMessage = "Success";
                 result.ResultsetData = response;
             }
             else
             {
              _logger.LogInformation("Failed to update the upinput");
                 result.ResponseCode = "KO";
                 result.ResponseMessage = "Failure";
                 result.ResultsetData = response;
            
             }
            return result;
        }
        [HttpDelete("{id:length(24)}")]
        public BaseResponse Delete(string id)
      {
          BaseResponse response = new();
          var task = _dbContext.Get(id);
    
          if (task == null)
          {
              response.ResponseCode = "KO";
              response.ResponseMessage = "Failure";
             
          }
          _dbContext.Delete(task.uniqueId);
          _logger.LogInformation("Succesfully respective id input deleted");
          response.ResponseCode = "OK";
          response.ResponseMessage = "Success";
    
          return response;
      }

    }
}

