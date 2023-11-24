using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserToolsApp.Models;

namespace UserToolsApp.Controllers;

public class HomeController : Controller
{
    private OhmViewModel _ohmViewModel;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _ohmViewModel = new OhmViewModel();
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult OptionSelected(OptionsModel model)
    {
        return RedirectToAction("Ohm");
    }
    [HttpGet]
    public IActionResult Ohm()
    {
        return View(_ohmViewModel);
    }
    
    [HttpPost]
    public IActionResult CalculateOhmData(OhmViewModel model)
    {
        Console.WriteLine($"R data:{model.Current},{model.Voltage},{model.Resistance}");
        return RedirectToAction("Ohm");
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