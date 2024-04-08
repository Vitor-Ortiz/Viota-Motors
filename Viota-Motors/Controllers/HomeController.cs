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
        List<Carro> carros = [];
        using (StreamReader leitor = new("Data\\carros.json"))
        {
            string dados = leitor.ReadToEnd();
            carros = JsonSerializer.Deserialize<List<Carro>>(dados);
        }

        List<Tipo> tipos = [];
        using (StreamReader leitor = new("Data\\tipos.json"))
        {
            string dados = leitor.ReadToEnd();
            tipos = JsonSerializer.Deserialize<List<Tipo>>(dados);
        }
        ViewData["Tipos"] = tipos;
        return View(carros);
    }
        public IActionResult Details (int id)
        {
            List<Carro> carros = [];
            using(StreamReader leitor = new("Data\\carros.json"))
            {
                string dados = leitor.ReadToEnd();
                carros = JsonSerializer.Deserialize<List<Carro>>(dados);
            }
            List<Tipo> tipos = [];
            using(StreamReader leitor = new("Data\\tipos.json"))
            {
                string dados = leitor.ReadToEnd();
                tipos = JsonSerializer.Deserialize<List<Tipo>>(dados);
            }
            DetailsVM details = new(){
                Tipos = tipos,
                Atual = carros.FirstOrDefault(c => c.Numero == id),
                Anterior = carros.OrderByDescending(c => c.Numero).FirstOrDefault(c => c.Numero < id),
                Proximo = carros.OrderBy(c => c.Numero).FirstOrDefault(c => c.Numero > id),
            };
            return View(details);
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
