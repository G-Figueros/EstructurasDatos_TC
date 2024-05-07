using clsEstructuraDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Pila
{
    public class clsNodoPila
    {
        public string elemento { get; set; }
        public object elementoObjeto { get; set; }
        public clsNodoPila? enlace { get; set; }

        public clsNodoPila(string vElemento, object elementoObjetoV)
        {

            elemento = vElemento;
            elementoObjeto = elementoObjetoV;
            enlace = null;
        }

    }
}
