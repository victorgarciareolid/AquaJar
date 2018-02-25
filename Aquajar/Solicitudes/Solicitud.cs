using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquajar.Solicitudes
{
    public abstract class Solicitud
    {
        protected static int numUsuarios = 0;
        protected int usuario { get; set; }
        protected bool completada { get; set; }
        protected HttpContext contexto{ get; }
        public Resultado res { get; set; }

        public Solicitud()
        {
            completada = false;
            numUsuarios++;
            this.usuario = numUsuarios;
            this.res = new Resultado();
        }

        public void completar()
        {
            this.completada = true;
        }

        public bool listo()
        {
            return completada;
        }

        public abstract string toString();
    }
}
