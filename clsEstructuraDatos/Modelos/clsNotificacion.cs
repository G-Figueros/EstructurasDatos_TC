using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Modelos
{
    public class clsNotificacion
    {
        public string numeroTarjeta { get; set; }
        public Double monto { get; set; }
        public Boolean tipo { get; set; }

        public clsNotificacion(string numeroTarjetaV, Double montoV, Boolean tipoV)
        {
            this.numeroTarjeta = numeroTarjetaV;
            this.monto = montoV;
            this.tipo = tipoV;
        }
    }
}
