using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquajar.Cola
{
    interface Cola <T>
    {
        bool lleno();
        bool vacio();
        int posicionesLibres();
        void poner(T e);
        T sacar();
    }
}
