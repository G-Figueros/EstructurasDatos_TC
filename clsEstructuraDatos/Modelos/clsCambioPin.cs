using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Modelos
{
    public class clsCambioPin
    {
        public clsTarjeta numeroTarjeta { get; set; }
        public string anterior { get; set; }
        public string nuevo { get; set; }

        public clsCambioPin(clsTarjeta numeroTarjetaV, string anteriorV, string nuevoV)
        {
            this.numeroTarjeta = numeroTarjetaV;
            this.anterior = anteriorV;
            this.nuevo = nuevoV;
        }
    }
}
