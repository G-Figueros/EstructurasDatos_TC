using clsEstructuraDatos.Arbol;
using clsEstructuraDatos.Cola;
using clsEstructuraDatos.Interface;
using clsEstructuraDatos.ListaSimple;
using clsEstructuraDatos.Pila;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.Modelos
{
    public class clsTarjetaService : ITarjetaService
    {
        public clsLista ListaTarjeta { get; } = new clsLista();
        public clsArbolBinarioBusqueda ABBCuentas { get; } = new clsArbolBinarioBusqueda();
        public clsCola<string> ColaNotificaciones { get; } = new clsCola<string>();
        public clsCola<string> ColaPago { get; } = new clsCola<string>();
        public clsPila PilaMovimientos { get; } = new clsPila();
        public clsPila AumentoLimite { get; } = new clsPila();
        public clsLista ListaPin { get; } = new clsLista();
    }
}
