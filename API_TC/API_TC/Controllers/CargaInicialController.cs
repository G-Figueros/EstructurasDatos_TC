using Microsoft.AspNetCore.Mvc;
using clsEstructuraDatos.Modelos;
using System.Text.Json.Serialization;
using System.Text.Json;
using clsEstructuraDatos.Pila;
using clsEstructuraDatos.ListaSimple;
using clsEstructuraDatos.Interface;

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
        public string Post(List<clsTarjeta> tarjetas)
        {
            foreach (var tarjeta in tarjetas)
            {
                _tarjetaService.ListaTarjeta.insertHeaderLista(tarjeta);
            }
    
            return "Tarjetas procesadas exitosamente";
        }

        [HttpGet("GetSaldos")]
        public Double Get(string numTarjeta)
        {
            return _tarjetaService.ListaTarjeta.getSaldo(numTarjeta);
        }
    }
}
