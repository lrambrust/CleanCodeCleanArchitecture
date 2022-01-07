using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ECommerceApp.Controllers
{
    [EnableCors()]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult RetornarDadosTable()
        {
            return Ok(CriarLista());
        }

        

        private List<Teste> CriarLista()
        {
            var lista = new List<Teste>();

            lista.Add(new Teste(58, "Punção", "A CADA 4 DIAS", "-10:00:00", 0, new DateTime(2021, 11, 01, 13, 10, 0), "100:00:00"));
            lista.Add(new Teste(22, "Plato", "A CADA 4 DIAS", "-2:00:00", 0, new DateTime(2021, 11, 13, 9, 27, 0), "90:00:00"));
            lista.Add(new Teste(58, "Coroa", "A CADA 4 DIAS", "3:00:00", 50, new DateTime(2021, 11, 24, 21, 18, 0), "80:00:00"));
            lista.Add(new Teste(58, "PM (pré-molde)", "A CADA 4 DIAS", "6:00:00", 99, new DateTime(2021, 11, 10, 10, 53, 0), "50:00:00"));

            return lista;
        }


        public class Teste
        {
            public Teste(int numeroEquipamento, string nomeEquipamento, string frequenciaTroca, string horasRestantes, int indicadorPercentual, DateTime dataHoraEntrada, string vidaUtilTotal)
            {
                NumeroEquipamento = numeroEquipamento;
                NomeEquipamento = nomeEquipamento;
                FrequenciaTroca = frequenciaTroca;
                HorasRestantes = horasRestantes;
                IndicadorPercentual = indicadorPercentual;
                DataEntrada = dataHoraEntrada.ToString("dd/MM/yyyy");
                HoraEntrada = dataHoraEntrada.ToString("HH:mm");
                VidaUtilTotal = vidaUtilTotal;
            }

            public int NumeroEquipamento { get; set; }
            public string NomeEquipamento { get; set; }
            public string FrequenciaTroca { get; set; }
            public string HorasRestantes { get; set; }
            public int IndicadorPercentual { get; set; }
            public string DataEntrada { get; set; }
            public string HoraEntrada { get; set; }
            public string VidaUtilTotal { get; set; }
        }
    }
}
