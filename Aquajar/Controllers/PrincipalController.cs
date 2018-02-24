using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aquajar.Solicitudes;
namespace Aquajar.Controllers
{
    public class PrincipalController : Controller
    {
        private Random r = new Random();

        [HttpGet]
        public string Hola(int id)
        {
            if (id == 0) return "Maldito";
            return "Hola "+ r.Next()%id;
        }

        [HttpGet]
        [Route("Principal/Hola")]
        public string Hola()
        {
            return "Puto";
        }

        [HttpGet]
        public IActionResult conseguirInfo()
        {
            Startup.cq.poner(new Info(2));
            // Wait for response
            // Return View
            return View();
        }

        [HttpPost]
        public IActionResult actuar()
        {
            return View();
        }
    }
}
