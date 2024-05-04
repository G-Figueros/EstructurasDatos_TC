using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.ListaSimple
{
    public class clsNodo
    {
        public object Dato { get; set; }
        public clsNodo Enlace { get; set; }

        public clsNodo(object vtDato)
        {
            Dato = vtDato;
            Enlace = null;
        }
    }
}
