using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquajar.Solicitudes
{
    public class Resultado
    {
        private Solicitud sol { get; set; }
        private double value { get; set; }

        public void parseResultado(string data)
        {
            string[] info = data.Split("#");
            if (info.Length < 3) throw new Exception("Dato Invalido! - Length inferior a 3");

            try
            {
                sol.completar();
                this.value = Double.Parse(info[3]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error de Parseo!");
            }

        }
    }
}
