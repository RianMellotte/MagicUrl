using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("")]
    public class Fallbackcontroller : Controller
    {
        public IActionResult Index()
        {        
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"),
                "text/HTML");
        }
    }
}