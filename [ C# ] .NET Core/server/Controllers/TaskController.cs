using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Controllers
{
   [ApiController, Route("[Controller]")]
   [Authorize]
   [EnableCors("AllowClient")]
   public class TaskController : ControllerBase
   {
      private readonly TaskManagerContext _taskmanagercontext;
      public TaskController(TaskManagerContext taskmanagercontext)
      {
         _taskmanagercontext = taskmanagercontext;
      }

      // [Authorize(Roles = "Leader")]
      [HttpPost("CreateTask")]
      public async Task<IActionResult> CreateTask()
      {
         return Ok();
      }
   }
}