using clsEstructuraDatos.Modelos;
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

        public void insertPila(string elemento, Object elementoObjeto) 
        {
            
            clsNodoPila nuevoElemento = new clsNodoPila(elemento, elementoObjeto);
            nuevoElemento.enlace = cima;
            cima = nuevoElemento;
        }

        public object deletePila()
        {
            if (pilaVacia())
            {
                return null;
            }

            clsNodoPila auxNodo = cima;
            cima = cima.enlace;
            return auxNodo;
        }

        public IEnumerable<clsNodoPila> RecorrerPila()
        {
            clsNodoPila nodoActual = cima;

            while (nodoActual != null)
            {
                yield return nodoActual;
                nodoActual = nodoActual.enlace;
            }
        }
    }
}
