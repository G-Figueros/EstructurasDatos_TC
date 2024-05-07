using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Modelos
{
    public class clsSolicitudesLimite
    {
        public string numeroTarjeta { get; set; }
        public Double limiteNuevo { get; set; }

        public clsSolicitudesLimite(string numeroTarjeta, Double limiteNuevo)
        {
            this.numeroTarjeta = numeroTarjeta;
            this.limiteNuevo = limiteNuevo;
        }
    }
}
