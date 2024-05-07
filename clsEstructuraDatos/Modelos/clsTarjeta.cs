using clsEstructuraDatos.Modelos;
using clsEstructuraDatos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Modelos
{
    public class clsTarjeta : Comparador
    {
        public string numeroTarjeta { get; set; }
        public string nombre { get; set; }
        public bool estatusActivo { get; set; }
        public string pin { get; set; }
        public Double saldo { get; set; }
        public Double limiteCredito { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string fechaCorte { get; set; }
        public string fechaPago { get; set; }

        public clsTarjeta(string numeroTarjeta, string nombre, bool estatusActivo, string pin, Double saldo, Double limiteCredito, DateTime fechaVencimiento, string fechaCorte, string fechaPago)
        {
            this.numeroTarjeta = numeroTarjeta;
            this.nombre = nombre;
            this.estatusActivo = estatusActivo;
            this.pin = pin;
            this.saldo = saldo;
            this.limiteCredito = limiteCredito;
            this.fechaVencimiento = fechaVencimiento;
            this.fechaCorte = fechaCorte;
            this.fechaPago = fechaPago;
        }

        public bool igualQue(string q)
        {
            return this.numeroTarjeta == q;
        }

        public bool menorQue(string q)
        {
            if (this.numeroTarjeta.CompareTo(q) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool mayorQue(string q)
        {
            if (this.numeroTarjeta.CompareTo(q) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool igualQue(object q)
        {
            clsTarjeta q2 = (clsTarjeta)q;
            return this.numeroTarjeta == q2.numeroTarjeta;
        }

        public bool menorQue(object q)
        {
            clsTarjeta q2 = (clsTarjeta)q;
            if (this.numeroTarjeta.CompareTo(q2.numeroTarjeta) < 0)
                return true;
            else
                return false;
        }

        public bool mayorQue(object q)
        {
            clsTarjeta q2 = (clsTarjeta)q;
            if (this.numeroTarjeta.CompareTo(q2.numeroTarjeta) > 0)
                return true;
            else
                return false;
        }
    }
}
