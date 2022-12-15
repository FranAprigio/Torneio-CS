using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TorneioCS.Models;
using TorneioCS.Repository;
using TorneioCS.Services;

namespace TorneioCS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICompetidorRepository _comrepository;
        private readonly ITorneioService _torneioService;
        protected readonly DbSet<Competidor> dbSet;

        private static ColocacaoTorneio _colocacao = new ColocacaoTorneio();

        public HomeController(ILogger<HomeController> logger, ICompetidorRepository competidorRepository, ITorneioService torneioService)
        {
            _logger = logger;
            _comrepository = competidorRepository;
            _torneioService = torneioService;
        }

        //Listar competidores na pagina Index
        public IActionResult Index()
        {
            return View(_comrepository.ListarCompetidores());
        }

        // Requisição da lista de ids
        [HttpPost]
        public JsonResult IniciarTorneio(IList<int> idsCompetidores) 
            //Dado recebido: [1, 2, 3, 4, 5]

        {
            try
            { 
                //inicio do processamento do torneio
                var listaParticipantes = _torneioService.OrganizarPorIdade(idsCompetidores);
                _colocacao = _torneioService.GetColocacaoFinal(listaParticipantes);

                return Json(new { url = $"Home/Resultado/" });
            }
            catch (Exception erro)
            {
                throw new Exception("Erro ao processar o torneio: " + erro.Message);
            }
        }

        //processamento da pagina resultado
        public IActionResult Resultado()
        {
            try
            {
                return View(_colocacao);
            }
            catch (Exception erro)
            {
                throw new Exception("Erro ao processar o resultado: " + erro.Message);
            }
        }

        //processamento da pagina sobre
        public IActionResult Sobre()
        {
            return View();
        }

        //Sem permissão para armazenar cache
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
