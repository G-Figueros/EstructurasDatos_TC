using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Arbol
{
    public class clsNodoArbol
    {
        protected Object dato;
        protected clsNodoArbol izdo;
        protected clsNodoArbol dcho;

        /// <summary>
        /// Método Constructor del nodo el cual recibe un valor y asign
        /// asigna nulos a los hijos
        /// </summary>
        /// <param name="valor">hhhhhhhhhhhhh</param>
        public clsNodoArbol(Object valor)
        {
            dato = valor;
            izdo = dcho = null;
        }

        public clsNodoArbol(clsNodoArbol ramaIzdo, Object valor, clsNodoArbol ramaDcho)
        {
            this.dato = valor;
            izdo = ramaIzdo;
            dcho = ramaDcho;
        }

        // operaciones de acceso
        public Object valorNodo()
        {
            return dato;
        }

        public clsNodoArbol subarbolIzdo()
        {
            return izdo;
        }

        public clsNodoArbol subarbolDcho()
        {
            return dcho;
        }

        public void nuevoValor(Object d)
        {
            dato = d;
        }


        public void ramaIzdo(clsNodoArbol n) { izdo = n; }
        public void ramaDcho(clsNodoArbol n) { dcho = n; }
        public string visitar()
        {
            return dato.ToString();
        }
    }
}
