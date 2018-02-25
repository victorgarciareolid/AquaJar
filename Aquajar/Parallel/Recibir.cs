using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aquajar.Cola;
using Aquajar.Solicitudes;

namespace Aquajar.Parallel
{
    public class Recibir
    {
        public ColaCircular<Resultado> recibir { get; }

        public Recibir()
        {
            recibir = new ColaCircular<Resultado>(1000);
        }

        public void run()
        {
            // Asociar respuesta con su solicitud
            while (true)
            {
                while (recibir.vacio())
                {
                    Thread.Sleep(1500);
                }




            }
        }



    }
}
