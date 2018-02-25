using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquajar.Cola
{
    public class ColaCircular<T> : MiCola<T>
    {
        private readonly T[] p;
        private int f, l;

        public ColaCircular(int N) : base(N)
        {
            p = new T[N];
        }

        public override void poner(T e)
        {
            if (lleno()) throw new Exception("Cola Llena");
            p[l] = e;
            l = (l + 1) % N;
            numElementos += 1;

        }
        public override T sacar()
        {
            if (vacio()) throw new Exception("Cola vacia");
            T elemento = p[f];
            f = (f + 1) % N;
            numElementos -= 1;
            return elemento;
        }

    }
}
