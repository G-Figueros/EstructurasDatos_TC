using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Cola
{
    public class clsCola
    {
        public clsNodoCola primero { get; set; }
        public clsNodoCola ultimo { get; set; }

        public clsCola()
        {
            primero = null;
            ultimo = null;
        }

        public void pushCola(string cliente)
        {
            clsNodoCola nuevoNodo = new clsNodoCola(cliente);

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

        public string deleteCola()
        {
            if (primero == null)
            {
                return "Vacia";
            }

            string valor = primero.nombre;
            primero = primero.enlace;

            if (primero == null)
            {
                ultimo = null;
            }
            
            return valor;
        }
    }
}
