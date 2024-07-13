using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;

namespace server.Controllers
{
   [ApiController, Route("[Controller]")]
   [Authorize]
   [EnableCors("AllowClient")]
   public class ProjectController : ControllerBase
   {
      #pragma warning disable CS8604
      private readonly TaskManagerContext _taskmanagercontext;
      private readonly EmailService _emailservice;

      public ProjectController(TaskManagerContext taskmanagercontext, EmailService emailservice)
      {
         _taskmanagercontext = taskmanagercontext;
         _emailservice = emailservice;
      }

      [HttpGet("DashBoard")]
      public async Task<IActionResult> DashBoard()
      {
         var iduser = User.Claims.FirstOrDefault(c => c.Type == "IDUser")?.Value;

         if (iduser == null)
         {
            return Unauthorized();
         }
         var ListProject = await _taskmanagercontext.Members.Where(w => w.IDUser.ToString() == iduser)
                                                    .Select(s => s.IDProjectNavigation)
                                                    .Where(w => w.IsDeleted == false)
                                                    .ToListAsync();
         if (!iduser.Any())
         {
            return NotFound();
         }

         return Ok(ListProject);
      }

      [HttpPost("CreateProject")]
      public async Task<IActionResult> CreateProject([FromBody] Project project)
      {
         if (ModelState.IsValid)
         {

            var iduser = User.Claims.FirstOrDefault(c => c.Type == "IDUser")?.Value;

            project.IDLeader = Guid.Parse(iduser);

            project.DayCreate = DateTime.Now;
            _taskmanagercontext.Add(project);
            await _taskmanagercontext.SaveChangesAsync();

            Guid IDPROJECT = project.IDProject;

            var MEMBER = new Member
            {
               IDProject = project.IDProject,
               IDUser = Guid.Parse(iduser),
            };
            _taskmanagercontext.Members.Add(MEMBER);
            await _taskmanagercontext.SaveChangesAsync();
            var PERMISSION = new Permission
            {
               IDProject = project.IDProject,
               IDUser = Guid.Parse(iduser),
               Role = "Leader",
            };
            _taskmanagercontext.Permissions.Add(PERMISSION);

            await _taskmanagercontext.SaveChangesAsync();


            return Ok(new
            {
               redirectUrl = "http://localhost:333/Project/ChooseTemplate",
               IDProject = IDPROJECT,
            });
         }
         return BadRequest("ModelState InValid");
      }

      [HttpGet("ChooseTemplate")] //Need Review
      public async Task<IActionResult> ChooseTemplate(Guid IDProject)
      {
         var TEMPLATE = await _taskmanagercontext.Templates.ToListAsync();
         var GETTEMPLATE = new DashBoardViewModel
         {
            Templates = TEMPLATE,
         };
         return Ok(GETTEMPLATE);
      }

      [HttpPost("LoadTemplate")] //Need Review
      public async Task<IActionResult> LoadTemplate(string idtemplate, string idproject)
      {
         // int IDProject = (int)TempData["IDPROJECT"];
         Guid IDTEMPLATE = Guid.Parse(idtemplate);
         Guid IDPROJECT = Guid.Parse(idproject);

         var GETLISTTEMPLATE = await _taskmanagercontext.ListTemplates
                               .Where(w => w.IDTemplate == IDTEMPLATE)
                               .Select(s => s.StatusName).ToListAsync();

         foreach (var GetListTemplate in GETLISTTEMPLATE)
         {
            var STATUS = new Status
            {
               // IsDeleted = false,
               IDProject = IDPROJECT,
               StatusName = GetListTemplate
            };
            _taskmanagercontext.Statuses.Add(STATUS);
         }
         await _taskmanagercontext.SaveChangesAsync();

         return Ok(new
         {
            redirectUrl = "http://localhost:333/Project/Work",
            IDProject = IDPROJECT,
         });
      }

      // [Authorize(Roles = "Leader")]




   }

}

