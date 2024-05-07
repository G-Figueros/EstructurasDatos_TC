using clsEstructuraDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEstructuraDatos.ListaSimple
{
    public class clsLista
    {
        public clsNodo vtHeader { get; set; }

        public clsLista()
        {
            vtHeader = null;
        }

        public void insertHeaderLista(object objNodo)
        {
            clsNodo nuevoNodo = new clsNodo(objNodo);
            nuevoNodo.Enlace = vtHeader;
            vtHeader = nuevoNodo;
        }

        public int readNodos()
        {
            int suma = 0;
            clsNodo nodoActual = vtHeader;

            while (nodoActual != null)
            {
                suma += int.Parse(nodoActual.Dato.ToString());
                nodoActual = nodoActual.Enlace;
            }

            return suma;
        }

        public Double getSaldo(string numTarjeta)
        {
            double saldo = 0.0;
            clsNodo nodoActual = vtHeader;

            while (nodoActual != null)
            {
                clsTarjeta tarjeta = (clsTarjeta)nodoActual.Dato;

                if (tarjeta.numeroTarjeta == numTarjeta)
                {
                    saldo = tarjeta.saldo;
                    break; 
                }

                nodoActual = nodoActual.Enlace;
            }

            return saldo;
        }

        public clsTarjeta getTarjeta(string numTarjeta)
        {

            clsNodo nodoActual = vtHeader;

            while (nodoActual != null)
            {
                clsTarjeta tarjeta = (clsTarjeta)nodoActual.Dato;

                if (tarjeta.numeroTarjeta == numTarjeta)
                {
                    return tarjeta;
                }

                nodoActual = nodoActual.Enlace;
            }
            return null; 
        }

        public Double updateSaldo(string numTarjeta, double abono, Boolean tipoUpdate)
        {
            double saldo = 0.0;
            clsNodo nodoActual = vtHeader;

            while (nodoActual != null)
            {
                clsTarjeta tarjeta = (clsTarjeta)nodoActual.Dato;

                if (tarjeta.numeroTarjeta == numTarjeta)
                {
                    if (!tipoUpdate)
                    {
                        tarjeta.saldo = tarjeta.saldo - abono; //abonar al saldo del objeto tarjeta
                    }
                    else
                    {
                        tarjeta.saldo = tarjeta.saldo + abono; //cargar al saldo del objeto tarjeta
                    }
                    
                    saldo = tarjeta.saldo;
                    break;
                }

                nodoActual = nodoActual.Enlace;
            }

            return saldo;
        }

        public Double UpdateLimiteCredito(string numTarjeta, double nuevoLimite)
        {
            double limiteActualizado = 0.0;
            clsNodo nodoActual = vtHeader;

            while (nodoActual != null)
            {
                clsTarjeta tarjeta = (clsTarjeta)nodoActual.Dato;

                if (tarjeta.numeroTarjeta == numTarjeta)
                {
                    tarjeta.limiteCredito = nuevoLimite;
                    limiteActualizado = tarjeta.limiteCredito;
                    break;
                }

                nodoActual = nodoActual.Enlace;
            }

            return limiteActualizado;
        }

        public void UpdatePin(clsCambioPin cambioPin)
        {
            clsTarjeta tarjeta = buscarTarjeta(cambioPin.numeroTarjeta.numeroTarjeta);

            if (tarjeta != null)
            {
                tarjeta.pin = cambioPin.nuevo;
            }
        }

        private clsTarjeta buscarTarjeta(string numTarjeta)
        {
            clsNodo nodoActual = vtHeader;

            while (nodoActual != null)
            {
                clsTarjeta tarjeta = (clsTarjeta)nodoActual.Dato;

                if (tarjeta.numeroTarjeta == numTarjeta)
                {
                    return tarjeta;
                }

                nodoActual = nodoActual.Enlace;
            }
            return null;
        }


    }
}
