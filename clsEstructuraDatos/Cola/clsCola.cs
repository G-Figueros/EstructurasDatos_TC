using clsEstructuraDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Cola
{
    public class clsCola<T>
    {
        public clsNodoCola<T> primero { get; set; }
        public clsNodoCola<T> ultimo { get; set; }

        public clsCola()
        {
            primero = null;
            ultimo = null;
        }

        public void pushCola(T dato, clsMovimiento pago)
        {
            clsNodoCola<T> nuevoNodo = new clsNodoCola<T>(dato, pago);

            if (ultimo == null)
            {
                primero = nuevoNodo;
                ultimo = nuevoNodo;
            }
            else
            {
                ultimo.enlace = nuevoNodo;
                ultimo = nuevoNodo;
            }
        }

        public clsNodoCola<T> deleteCola()
        {
            if (primero == null)
            {
                return null;
            }

            clsNodoCola<T> nodoEliminado = primero;
            primero = primero.enlace;

            if (primero == null)
            {
                ultimo = null;
            }

            return nodoEliminado;
        }


        public List<clsMovimiento> BuscarPagosPorNumeroTarjeta(string numeroTarjeta)
        {
            List<clsMovimiento> pagosRelacionados = new List<clsMovimiento>();

            clsNodoCola<T> nodoActual = primero;

            while (nodoActual != null)
            {
                if (nodoActual.dato.Equals(numeroTarjeta))
                {
                    pagosRelacionados.Add(nodoActual.pago);
                }

                nodoActual = nodoActual.enlace;
            }

            return pagosRelacionados;
        }
    }
}
