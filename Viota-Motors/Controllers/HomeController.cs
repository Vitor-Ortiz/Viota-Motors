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
        return View(carros);
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
