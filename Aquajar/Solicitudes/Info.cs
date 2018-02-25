using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquajar.Solicitudes
{
    public class Info : Solicitud
    {
        private int sensorId { get; set; }


        public Info(int sensorId)
        {
            this.sensorId = sensorId;
        }

        public override string toString()
        {
            return "#" + base.usuario + "#" + sensorId;
        }
    }
}
