using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Cola
{
    public class clsNodoCola
    {
        public string nombre { get; set; }
        public clsNodoCola enlace { get; set; }

        public clsNodoCola(string valor)
        {
            nombre = valor;
            enlace = null;
        }
    }
}
