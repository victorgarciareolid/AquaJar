using System;

namespace Aquajar.Solicitudes
{
    public class Accion : Solicitud
    {
        private int actuatorId { get;}
        private double value { get;}

        public Accion(int usuario, int actuatorId, double value) : base(usuario)
        {
            this.actuatorId = actuatorId;
            this.value = value;
        }
    }
}
