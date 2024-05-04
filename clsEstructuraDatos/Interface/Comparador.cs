using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Interface
{
    public interface Comparador
    {
        bool igualQue(string q);
        bool menorQue(string q);
        bool mayorQue(string q);
        bool igualQue(Object q);
        bool mayorQue(Object q);
        bool menorQue(Object q);
       
    }
}
