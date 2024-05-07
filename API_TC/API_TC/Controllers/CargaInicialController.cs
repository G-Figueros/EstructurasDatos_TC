using Microsoft.AspNetCore.Mvc;
using clsEstructuraDatos.Modelos;
using System.Text.Json.Serialization;
using System.Text.Json;
using clsEstructuraDatos.Pila;
using clsEstructuraDatos.ListaSimple;
using clsEstructuraDatos.Interface;
using clsEstructuraDatos.Cola;
using System.Text;

namespace API_TC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CargaInicialController : Controller
    {
         
        private readonly ILogger<CargaInicialController> _logger;
        private readonly ITarjetaService _tarjetaService;
        public CargaInicialController(ILogger<CargaInicialController> logger, ITarjetaService tarjetaService)
        {
            _logger = logger;
            _tarjetaService = tarjetaService;
        }

        [HttpPost("PostTarjetas")]
        public string PostTarjetas(List<clsTarjeta> tarjetas)
        {
            foreach (var tarjeta in tarjetas)
            {
                _tarjetaService.ListaTarjeta.insertHeaderLista(tarjeta);
                _tarjetaService.ABBCuentas.insertar(tarjeta);
            }
    
            return "Tarjetas Cargadas Exitosamente A Estructuras En Memoria";
        }

        [HttpGet("GetSaldos")]
        public string GetSaldos(string numTarjeta)
        {
            double saldoActual = _tarjetaService.ListaTarjeta.getSaldo(numTarjeta);
            string saldoActualStr = saldoActual.ToString();
            return "El saldo pendiente de pago de la tarjeta " + numTarjeta + " es de: " + "Q " + saldoActualStr;
        }

        [HttpPatch("PatchRealizarPago")]
        public string PatchPagarSaldo(string numTarjeta, double abono)
        {
            double saldoPendiente = _tarjetaService.ListaTarjeta.getSaldo(numTarjeta);


            if (abono > saldoPendiente)
            {
                return "LA CANTIDAD INGRESA SUPERA EL SALDO A PAGAR";
            }

            clsMovimiento pagoSaldo = new clsMovimiento(numTarjeta, abono);

            _tarjetaService.ColaPago.pushCola(numTarjeta, pagoSaldo);
            _tarjetaService.ListaTarjeta.updateSaldo(numTarjeta, abono, false);

            if (abono == saldoPendiente)
            {
                return "PAGO COMPLETO EXITOSO - No. Tarjeta cargado en COLA DE PAGOS";
            }

            return "ABONO DE PAGO EXITOSO - No. Tarjeta cargado en COLA DE PAGOS";
        }

        [HttpGet("GetLiberarColaPagos")]
        public string GetLiberarColaPagos()
        {
            StringBuilder resultado = new StringBuilder();
            while (_tarjetaService.ColaPago.primero != null)
            {
                string tarjetaEliminada = _tarjetaService.ColaPago.deleteCola();
                resultado.AppendLine("Se liberó la tarjeta de la cola de pagos: " + tarjetaEliminada);
            }
            return resultado.ToString();
        }

        [HttpGet("GetEstadosCuenta")]
        public string GetEstadosCuenta(string numTarjeta)
        {
            clsTarjeta cxcTarjetaConsultada = (clsTarjeta)_tarjetaService.ABBCuentas.buscar(numTarjeta); //Busqueda organizada a traves de ABB
            string status = (cxcTarjetaConsultada.estatusActivo) ? "ACTIVA" : "BLOQUEADA";
            List<clsMovimiento> pagosRelacionados = _tarjetaService.ColaPago.BuscarPagosPorNumeroTarjeta(numTarjeta);
            StringBuilder pagosRelacionadosString = new StringBuilder();

            foreach (var pago in pagosRelacionados)
            {
                pagosRelacionadosString.AppendLine("Abono: Q. " + pago.monto);
            }

            string cxcTarjeta = "No. Tarjeta: " + cxcTarjetaConsultada.numeroTarjeta + "\n" +
                                "Propietario: " + cxcTarjetaConsultada.nombre + "\n" +
                                "Fecha Vencimiento: " + cxcTarjetaConsultada.fechaVencimiento + "\n" +
                                "Día Corte: " + cxcTarjetaConsultada.fechaCorte + "\n" +
                                "Día Pago: " + cxcTarjetaConsultada.fechaPago + "\n" + "\n" + "\n" +
                                "MOVIMIENTOS: " + "\n" + pagosRelacionadosString + "\n" + "\n" + "\n" +
                                "Saldo Actual: Q. " + cxcTarjetaConsultada.saldo + "\n" +
                                "Limite Credito: Q. " + cxcTarjetaConsultada.limiteCredito + "\n" +
                                "Estado: " + status + "\n";

            return cxcTarjeta;
        }

        [HttpPost("PostMovimientosPila")]
        public string PostMovimientosPila(string numTarjeta, double cargo)
        {
            clsMovimiento movimiento = new clsMovimiento(numTarjeta, cargo);
            _tarjetaService.PilaMovimientos.insertPila(numTarjeta, movimiento, true);
            _tarjetaService.ListaTarjeta.updateSaldo(numTarjeta, cargo, true);

            return "Movimiento guardado con exito";
        }

        [HttpGet("GetMovimientosPila")]
        public string GetMovimientosPila(string numTarjeta)
        {
            StringBuilder resultado = new StringBuilder();
            clsPila auxiliar = new clsPila();

            while (!_tarjetaService.PilaMovimientos.pilaVacia())
            {
              
                clsNodoPila nodo = (clsNodoPila)_tarjetaService.PilaMovimientos.deletePila();

                if (nodo.elemento == numTarjeta)
                {

                    resultado.AppendLine("Movimiento relacionado con el número de tarjeta: " + ((clsMovimiento)nodo.elementoObjeto).tarjetaPago + " Monto: Q. " + ((clsMovimiento)nodo.elementoObjeto).monto);
                    
                }
                
                auxiliar.insertPila(nodo.elemento, nodo.elementoObjeto, nodo.tipo);
            }

            while (!auxiliar.pilaVacia())
            {
                clsNodoPila nodo = (clsNodoPila)auxiliar.deletePila();
                _tarjetaService.PilaMovimientos.insertPila(nodo.elemento, nodo.elementoObjeto, nodo.tipo);
            }

            return resultado.ToString();
        }

        [HttpGet("GetLiberarColaNotificaciones")]
        public string GetLiberarColaNotificaciones(string numTarjeta)
        {


            return " ";
        }

        [HttpGet("GetLiberarAumentosCredito")]
        public string GetLiberarAumentosCredito()
        {
            /*StringBuilder resultado = new StringBuilder();
            while (!_tarjetaService.PilaNotificaciones.pilaVacia())
            {
                object elementoEliminado = _tarjetaService.PilaNotificaciones.deletePila();
                resultado.AppendLine("Se eliminó el elemento de la pila: " + elementoEliminado.ToString());
            }
            return resultado.ToString();*/
            return " ";
        }

    }
}
