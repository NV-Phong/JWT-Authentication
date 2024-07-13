using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
   [AllowAnonymous]
   [ApiController]
   public class HomeController : ControllerBase
   {
      private readonly ILogger<HomeController> _logger;
      public HomeController(ILogger<HomeController> logger)
      {
         _logger = logger;
      }

      

   }
}