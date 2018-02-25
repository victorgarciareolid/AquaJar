using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquajar.Solicitudes
{
    public class Resultado
    {
        public double value { get; set; }

        public void parseResultado(string data)
        {
            System.Diagnostics.Debug.WriteLine(data);
            string[] info = data.Split('#');
            if (info.Length < 3) throw new Exception("Dato Invalido! - Length inferior a 3");

            try
            {
                value = Double.Parse(info[3].Replace('.', ','));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error de Parseo!");
            }

        }
    }
}
