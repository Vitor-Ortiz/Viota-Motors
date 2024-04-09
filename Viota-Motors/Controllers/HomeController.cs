using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Viota_Motors.Models;

namespace Viota_Motors.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
       List<Carro> carros = GetCarros();
       List<Tipo> tipos = GetTipos();
       ViewData["Tipos"] = tipos;
       return View(carros);
    }
        public IActionResult Details (int id)
        {
            List<Carro> carros = GetCarros();
            List<Tipo> tipos = GetTipos();
            DetailsVM details = new(){
                Tipos = tipos,
                Atual = carros.FirstOrDefault(p => p.Numero == id),
                Anterior = carros.OrderByDescending(p => p.Numero).FirstOrDefault(p => p.Numero < id),
                Proximo = carros.OrderBy(p => p.Numero).FirstOrDefault(p => p.Numero > id),
            };
            return View(details);
        }

        private List<Carro> GetCarros()
        {
            using (StreamReader leitor = new ("Data\\carros.json"))
            {
                string dados = leitor.ReadToEnd();
                return JsonSerializer.Deserialize<List<Carro>>(dados);
            }
        }

        private List<Tipo> GetTipos()
        {
            using (StreamReader leitor = new ("Data\\tipos.json"))
            {
                string dados = leitor.ReadToEnd();
                return JsonSerializer.Deserialize<List<Tipo>>(dados);
            }
        }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
