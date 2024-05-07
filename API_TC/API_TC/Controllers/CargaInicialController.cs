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

            clsMovimiento movimientoABONO = new clsMovimiento(numTarjeta, abono, "ABONO");

            _tarjetaService.ColaPago.pushCola(numTarjeta, movimientoABONO);
            _tarjetaService.ColaNotificaciones.pushCola(numTarjeta, movimientoABONO);
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
                clsNodoCola<string> nodo = _tarjetaService.ColaPago.deleteCola();
                resultado.AppendLine("Pago de la tarjeta: " + nodo.dato + " Monto: " + nodo.pago.monto);
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
                                "Estado: " + status + "\n" +
                                "PIN: " + cxcTarjetaConsultada.pin + "\n";

            return cxcTarjeta;
        }

        [HttpPost("PostMovimientosPila")]
        public string PostMovimientosPila(string numTarjeta, double cargo)
        {
            clsMovimiento movimientoCARGO = new clsMovimiento(numTarjeta, cargo, "CARGO");
            _tarjetaService.PilaMovimientos.insertPila(numTarjeta, movimientoCARGO);
            _tarjetaService.ColaNotificaciones.pushCola(numTarjeta, movimientoCARGO);
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
                auxiliar.insertPila(nodo.elemento, nodo.elementoObjeto);
            }

            while (!auxiliar.pilaVacia())
            {
                clsNodoPila nodo = (clsNodoPila)auxiliar.deletePila();
                _tarjetaService.PilaMovimientos.insertPila(nodo.elemento, nodo.elementoObjeto);
            }

            return resultado.ToString();
        }

        [HttpGet("GetLiberarPilaMovimientos")]
        public string GetLiberarPilaMovimientos()
        {
            StringBuilder resultado = new StringBuilder();

            while (!_tarjetaService.PilaMovimientos.pilaVacia())
            {
                clsNodoPila nodo = (clsNodoPila)_tarjetaService.PilaMovimientos.deletePila();
                resultado.AppendLine("Movimiento relacionado con el número de tarjeta: " + ((clsMovimiento)nodo.elementoObjeto).tarjetaPago + " Monto: Q. " + ((clsMovimiento)nodo.elementoObjeto).monto + " LIBERADO DE LA PILA");
            }

            return resultado.ToString();
        }

        [HttpGet("GetLiberarColaNotificaciones")]
        public string GetLiberarColaNotificaciones()
        {
            StringBuilder resultado = new StringBuilder();

            while (_tarjetaService.ColaNotificaciones.primero != null)
            {
                clsNodoCola<string> nodo = _tarjetaService.ColaNotificaciones.deleteCola();
                resultado.AppendLine("Notificacion por: " + nodo.pago.tipo + " - Tarjeta no: " + nodo.dato + " - Monto: Q." + nodo.pago.monto);
            }

            return resultado.ToString();
        }

        [HttpPost("PostCambioPin")]
        public string PostCambioPin(string numTarjeta, string PIN)
        {
            
            clsTarjeta tarjetaConsultada = _tarjetaService.ListaTarjeta.getTarjeta(numTarjeta);
            clsCambioPin gestionPIN = new clsCambioPin(tarjetaConsultada, tarjetaConsultada.pin, PIN);
            _tarjetaService.ListaPin.insertHeaderLista(gestionPIN);
            _tarjetaService.ListaTarjeta.UpdatePin(gestionPIN);

            return "PIN Actualizado Correctamente";
        }


        [HttpPatch("PatchEstadoTarjeta")]
        public string PatchEstadoBloqueo(string numTarjeta)
        {
            clsTarjeta tarjetaEncontrada = (clsTarjeta)_tarjetaService.ABBCuentas.buscar(numTarjeta);

            if(tarjetaEncontrada.estatusActivo == true)
            {
                tarjetaEncontrada.estatusActivo = false;
                return "TARJETA BLOQUEADA TEMPORALMENTE";
            }
            else
            {
                tarjetaEncontrada.estatusActivo = true;
                return "TARJETA ACTIVADA";
            }
        }

        [HttpPost("PostSolicitudLimiteCredito")]
        public string PostSolicitudLimiteCredito(string numTarjeta, double limiteNuevo)
        {
            clsSolicitudesLimite solicitudLimite = new clsSolicitudesLimite(numTarjeta, limiteNuevo);
            _tarjetaService.AumentoLimite.insertPila(numTarjeta, solicitudLimite);

            return "Movimiento guardado con exito";
        }


        [HttpGet("GetProcesarLimiteCredito")]
        public string GetLiberarAumentosCredito()
        {
            StringBuilder resultado = new StringBuilder();

            while (!_tarjetaService.AumentoLimite.pilaVacia())
            {
                clsNodoPila nodo = (clsNodoPila)_tarjetaService.AumentoLimite.deletePila();
                clsSolicitudesLimite solicitud = (clsSolicitudesLimite)nodo.elementoObjeto;
                _tarjetaService.ListaTarjeta.UpdateLimiteCredito(solicitud.numeroTarjeta, solicitud.limiteNuevo);
                resultado.AppendLine("Solicitud de Aumento de Limite PROCESADO - Tarjeta No. " + solicitud.numeroTarjeta + " - Nuevo Limite: Q. " + solicitud.limiteNuevo + " LIBERADO DE LA PILA");
            }

            return resultado.ToString();
        }

    }
}
