using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aquajar.Solicitudes;
using System.Threading;

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
        public string conseguirInfo()
        {
            Info info = new Info(1);
            Startup.e.resolver(info);

            int i = 0;
            while (!info.listo() && i<10)
            {
                Thread.Sleep(500);
                i++;
            }

            if (!info.listo())
            {
                return "Not Found";
            }
            return info.res.value.ToString();
        }

        [HttpPost]
        public IActionResult actuar()
        {
            return View();
        }
    }
}
