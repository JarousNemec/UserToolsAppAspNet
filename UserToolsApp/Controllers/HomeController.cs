using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserToolsApp.Models;

namespace UserToolsApp.Controllers;

public class HomeController : Controller
{
    public static OhmViewModel _ohmViewModel = new ();
    public static SvtViewModel _SvtViewModel = new ();
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult OptionSelected(OptionsModel model)
    {
        switch (model.Option)
        {
            case "geometry":
                return RedirectToAction("Geometry");
            case "ohm": 
                return RedirectToAction("Ohm");
            case "svt": 
                return RedirectToAction("Svt");
        }
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Ohm()
    {
        return View(_ohmViewModel);
    }
    
    [HttpGet]
    public IActionResult Svt()
    {
        return View(_SvtViewModel);
    }
    
    [HttpGet]
    public IActionResult Geometry()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CalculateOhmData(OhmViewModel model)
    {
        _ohmViewModel = model;
        switch (model.Result)
        {
            case "voltage":
                _ohmViewModel.Voltage = model.Resistance * model.Current;
                break;
            case "current": 
                _ohmViewModel.Current = model.Voltage / model.Resistance;
                break;
            case "resistance": 
                _ohmViewModel.Resistance = model.Voltage / model.Current;
                break;
        }
        return RedirectToAction("Ohm");
    }
    
    [HttpPost]
    public IActionResult CalculateSvtData(SvtViewModel model)
    {
        _SvtViewModel = model;
        switch (model.Result)
        {
            case "time":
                _SvtViewModel.Time = model.Distance / model.Velocity;
                break;
            case "distance": 
                _SvtViewModel.Distance = model.Velocity * model.Time;
                break;
            case "velocity": 
                _SvtViewModel.Velocity = model.Distance / model.Time;
                break;
        }
        return RedirectToAction("Svt");
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