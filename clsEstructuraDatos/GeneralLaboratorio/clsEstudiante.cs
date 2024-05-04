using clsEstructuraDatos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.GeneralLaboratorio
{
    public class clsEstudiante : Comparador
    {
        public string carne { get; set; }
        public string nombre { get; set; }
        public string correoElectronico { get; set; }
        public int parcialUno { get; set; }
        public int parcialDos { get; set; }
        public int zona { get; set; }
        public int final { get; set; }
        public int total { get; set; }

        public clsEstudiante(string carne, string nombre, string correoElectronico, int parcialUno, int parcialDos, int zona, int final, int total)
        {
            this.carne = carne;
            this.nombre = nombre;
            this.correoElectronico = correoElectronico;
            this.parcialUno = parcialUno;
            this.parcialDos = parcialDos;
            this.zona = zona;
            this.final = final;
            this.total = total;
        }

        public bool igualQue(string q)
        {
            return this.carne == q;
        }

        public bool menorQue(string q)
        {
            if (this.carne.CompareTo(q) < 0)
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
            if (this.carne.CompareTo(q) > 0)
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
            clsEstudiante q2 = (clsEstudiante)q;
            return this.carne == q2.carne;
        }

        public bool menorQue(object q)
        {
            clsEstudiante q2 = (clsEstudiante)q;
            if (this.carne.CompareTo(q2.carne) < 0)
                return true;
            else
                return false;
        }

        public bool mayorQue(object q)
        {
            clsEstudiante q2 = (clsEstudiante)q;
            if (this.carne.CompareTo(q2.carne) > 0)
                return true;
            else
                return false;
        }
    }
}
