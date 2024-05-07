using clsEstructuraDatos.Modelos;
using clsEstructuraDatos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Arbol
{
    public class clsArbolBinarioBusqueda:clsArbolBinario
    {
        public object[] ArrayBB;
        public clsArbolBinarioBusqueda() : base()
        {
        }

        public clsArbolBinarioBusqueda(clsNodoArbol nodo) : base(nodo)
        {
        }

        public Object buscar(string buscado)
        {
            if (raiz == null)
            {
                return null;
            }
            else
            {
                return buscar(raizArbol(), buscado);
            }
        }

        protected clsTarjeta buscar(clsNodoArbol raizSub, string buscado)
        {
            if (raizSub == null)
            {
                return null;
            }
            else
            {
                clsTarjeta dato = (clsTarjeta)raizSub.valorNodo();
                if (raizSub == null)
                {
                    return null;
                }
                else if (dato.igualQue(buscado))
                {
                    return dato;
                }
                else if (dato.menorQue(buscado))
                {
                    return buscar(raizSub.subarbolDcho(), buscado);
                }
                else
                {
                    return buscar(raizSub.subarbolIzdo(), buscado);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buscado"></param>
        /// <returns></returns>
        public clsNodoArbol buscarIterativo(Object buscado)
        {
            Comparador dato;
            bool encontrado = false;
            clsNodoArbol raizSub = raiz;
            dato = (Comparador)buscado;
            while (!encontrado && raizSub != null)
            {
                if (dato.igualQue(raizSub.valorNodo()))
                    encontrado = true;
                else if (dato.menorQue(raizSub.valorNodo()))
                    raizSub = raizSub.subarbolIzdo();
                else
                    raizSub = raizSub.subarbolDcho();
            }
            return raizSub;
        }


        public double updateSaldo(clsArbolBinarioBusqueda arbol, string numTarjeta, double abono)
        {
            clsNodoArbol nodoTarjeta = (clsNodoArbol)arbol.buscar(numTarjeta);

            if (nodoTarjeta == null)
            {              
                return 0.0;
            }
  
            clsTarjeta tarjeta = (clsTarjeta)nodoTarjeta.valorNodo();         
            tarjeta.saldo -= abono;

          
            return tarjeta.saldo;
        }


        public void insertar(Object valor)
        {
            Comparador dato;
            dato = (Comparador)valor;
            raiz = insertar(raiz, dato);
        }

        //método interno para realizar la operación
        protected clsNodoArbol insertar(clsNodoArbol raizSub, Comparador dato)
        {
            if (raizSub == null)
            {
                raizSub = new clsNodoArbol(dato);
            }
            else if (dato.menorQue(raizSub.valorNodo()))
            {
                clsNodoArbol iz;
                iz = insertar(raizSub.subarbolIzdo(), dato);
                raizSub.ramaIzdo(iz);
            }
            else if (dato.mayorQue(raizSub.valorNodo()))
            {
                clsNodoArbol dr;
                dr = insertar(raizSub.subarbolDcho(), dato);
                raizSub.ramaDcho(dr);
            }
            else throw new Exception("Nodo duplicado");
            return raizSub;
        }

        public void eliminar(Object valor)
        {
            Comparador dato;
            dato = (Comparador)valor;
            raiz = eliminar(raiz, dato);
        }

        //método interno para realizar la operación
        protected clsNodoArbol eliminar(clsNodoArbol raizSub, Comparador dato)
        {
            if (raizSub == null)
                throw new Exception("No encontrado el nodo con la clave");
            else if (dato.menorQue(raizSub.valorNodo()))
            {
                clsNodoArbol iz;
                iz = eliminar(raizSub.subarbolIzdo(), dato);
                raizSub.ramaIzdo(iz);
            }
            else if (dato.mayorQue(raizSub.valorNodo()))
            {
                clsNodoArbol dr;
                dr = eliminar(raizSub.subarbolDcho(), dato);
                raizSub.ramaDcho(dr);
            }
            else // Nodo encontrado
            {
                clsNodoArbol q;
                q = raizSub; // nodo a quitar del árbol
                if (q.subarbolIzdo() == null)
                    raizSub = q.subarbolDcho();
                else if (q.subarbolDcho() == null)
                    raizSub = q.subarbolIzdo();
                else
                { // tiene rama izquierda y derecha
                    q = reemplazar(q);
                }
                q = null;
            }
            return raizSub;
        }

        // método interno para susutituir por el mayor de los menores
        private clsNodoArbol reemplazar(clsNodoArbol act)
        {
            clsNodoArbol a, p;
            p = act;
            a = act.subarbolIzdo(); // rama de nodos menores
            while (a.subarbolDcho() != null)
            {
                p = a;
                a = a.subarbolDcho();
            }
            act.nuevoValor(a.valorNodo());
            if (p == act)
                p.ramaIzdo(a.subarbolIzdo());
            else
                p.ramaDcho(a.subarbolIzdo());
            return a;
        }
    }
}
