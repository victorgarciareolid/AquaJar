using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquajar.Solicitudes
{
    public abstract class Solicitud
    {
        protected int usuario { get;}
        protected bool completada { get;}

        public Solicitud(int usuario)
        {
            this.usuario = usuario;
            this.completada = false;
        }
    }
}
