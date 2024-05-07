using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Modelos
{
    public class clsMovimiento
    {
        public string tarjetaPago { get; set; }
        public Double monto { get; set; }
        public string tipo { get; set; }

        public clsMovimiento(string tarjeta, Double monto, string tipoMovimiento) { 
            this.tarjetaPago = tarjeta;
            this.monto = monto;
            this.tipo = tipoMovimiento;
        }
    }
}
