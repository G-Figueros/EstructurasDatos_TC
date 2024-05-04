using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Arbol
{
    public class clsArbolBinario
    {
        protected clsNodoArbol raiz;

        public clsArbolBinario()
        {
            raiz = null;
        }

        public clsArbolBinario(clsNodoArbol raiz)
        {
            this.raiz = raiz;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public clsNodoArbol raizArbol()
        {
            return raiz;
        }

        /// <summary>
        /// Comprueba el estatus del árbol
        /// </summary>
        /// <returns></returns>
        bool esVacio()
        {
            return raiz == null;
        }

        public static clsNodoArbol nuevoArbol(clsNodoArbol ramaIzqda, Object dato, clsNodoArbol ramaDrcha)
        {
            return new clsNodoArbol(ramaIzqda, dato, ramaDrcha);
        }


        //binario en preorden
        public static string preorden(clsNodoArbol r)
        {
            if (r != null)
            {
                return r.visitar() + preorden(r.subarbolIzdo()) +
                    preorden(r.subarbolDcho());
            }
            return "";
        }

        // Recorrido de un árbol binario en inorden
        public static string inorden(clsNodoArbol r)
        {
            if (r != null)
            {
                return inorden(r.subarbolIzdo())
                    + r.visitar() + inorden(r.subarbolDcho());
            }
            return "";
        }

        // Recorrido de un árbol binario en postorden
        public static string postorden(clsNodoArbol r)
        {
            if (r != null)
            {
                return postorden(r.subarbolIzdo()) + postorden(r.subarbolDcho()) + r.visitar();
            }
            return "";
        }

        //Devuelve el número de nodos que tiene el árbol
        public static int numNodos(clsNodoArbol raiz)
        {
            if (raiz == null)
                return 0;
            else
                return 1 + numNodos(raiz.subarbolIzdo()) +
                numNodos(raiz.subarbolDcho());
        }
    }
}
