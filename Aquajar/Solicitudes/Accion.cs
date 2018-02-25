using System;

namespace Aquajar.Solicitudes
{
    public class Accion : Solicitud
    {
        private int actuatorId { get; set; }
        private double value { get; set; }

        public Accion(int actuatorId, double value)
        {
            this.actuatorId = actuatorId;
            this.value = value;
        }

        public override string toString()
        {
            return "#" + base.usuario + "#" + actuatorId + "#" + value;
        }
    }
}
