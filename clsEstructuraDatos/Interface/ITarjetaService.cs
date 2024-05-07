using clsEstructuraDatos.Arbol;
using clsEstructuraDatos.Cola;
using clsEstructuraDatos.ListaSimple;
using clsEstructuraDatos.Pila;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Interface
{
    public interface ITarjetaService
    {
        clsLista ListaTarjeta { get; }
        clsArbolBinarioBusqueda ABBCuentas { get; }
        clsCola<string> ColaNotificaciones { get; }
        clsCola<string> ColaPago { get; }
        clsPila PilaMovimientos { get; }
        clsPila AumentoLimite { get; }
        clsLista ListaPin { get; }
    }
}
