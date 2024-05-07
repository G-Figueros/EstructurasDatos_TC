using clsEstructuraDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Cola
{
    public class clsNodoCola<T>
    {
        public T dato { get; set; }

        public clsMovimiento pago { get; set; }
        public clsNodoCola<T> enlace { get; set; }

        public clsNodoCola(T valor, clsMovimiento pagoV)
        {
            dato = valor;
            pago = pagoV;
            enlace = null;
        }
    }
}
