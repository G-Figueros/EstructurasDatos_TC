using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Pila
{
    public class clsPila
    {
        public clsNodoPila cima { get; set; }

        public clsPila()
        {
            cima = null;
        }

        public Boolean pilaVacia()
        {
            return cima == null;
        }

        public void insertPila(string elemento)
        {
            clsNodoPila nuevoElemento = new clsNodoPila(elemento);
            nuevoElemento.enlace = cima;
            cima = nuevoElemento;
        }

        public object deletePila()
        {
            object auxElemento = cima.elemento;
            cima = cima.enlace;
            return auxElemento;
        }
    }
}
