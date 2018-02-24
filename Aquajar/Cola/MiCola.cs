using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquajar.Cola
{
    public abstract class MiCola<T> : Cola<T>
    {
        protected readonly int N;
        protected int numElementos;

        public MiCola(int N)
        {
            this.N = N;
        }

        public bool lleno()
        {
            return N == numElementos;
        }

        public bool vacio()
        {
            return numElementos == 0;
        }

        public int posicionesLibres()
        {
            return N - numElementos;
        }

        public abstract void poner(T e);
        public abstract T sacar();
    }
}
