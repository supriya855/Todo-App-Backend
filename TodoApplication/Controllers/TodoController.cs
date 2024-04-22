using Microsoft.AspNetCore.Mvc;
using TodoApplicationWebAPI.Services;
using TodoApplicationWebAPI.Model;

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
        [HttpGet]
        //public ActionResult<List<TodoModel>> Get() =>
        //   _dbContext.Get();
        public ActionResult<List<TodoModel>> Get()
        {
            var todos = _dbContext.GetAllTodos();
            return Ok(todos);
        }

        [HttpPost]
        public IActionResult Create(TodoModel input)
        {
            TodoModel response = new();
            response = _dbContext.Create(input);
            _logger.LogInformation("Succesfully input added");
            return Ok(response);
        }
          [HttpPut("{id:length(24)}")]
       public IActionResult Update(string id, TodoModel upInput)
       {
           TodoModel response = new();
           var task = _dbContext.Get(id);
           if (task == null)
           {
               return NotFound();
           }
           response = _dbContext.Update(id, upInput);
           _logger.LogInformation("Succesfully upinput updated");
           return Ok(response);
       }
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var task = _dbContext.Get(id);

            if (task == null)
            {
                return NotFound();
            }

            _dbContext.Delete(task.Id);
            _logger.LogInformation("Succesfully respective id input deleted");
            return NoContent();
        }

    }
}

