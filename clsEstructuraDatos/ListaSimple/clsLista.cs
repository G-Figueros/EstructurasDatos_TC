using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.ListaSimple
{
    public class clsLista
    {
        public clsNodo vtHeader { get; set; }

        public clsLista() {
            vtHeader = null;
        }   

        public void insertHeaderLista(object objNodo)
        {
            clsNodo nuevoNodo = new clsNodo(objNodo);
            nuevoNodo.Enlace = vtHeader;
            vtHeader = nuevoNodo;
        }

        public int readNodos()
        {
            int suma = 0;
            clsNodo nodoActual = vtHeader;

            while (nodoActual != null)
            {
                suma += int.Parse(nodoActual.Dato.ToString());
                nodoActual = nodoActual.Enlace;
            }

            return suma;
        }
    }
}
