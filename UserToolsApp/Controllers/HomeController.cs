using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserToolsApp.Models;

namespace UserToolsApp.Controllers;

public class HomeController : Controller
{
    public static OhmViewModel OhmModel = new();
    public static SvtViewModel SvtModel = new();
    public static GeometryViewModel GeometryModel = new();
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
        return View(OhmModel);
    }

    [HttpGet]
    public IActionResult Svt()
    {
        return View(SvtModel);
    }

    [HttpGet]
    public IActionResult Geometry()
    {
        return View(GeometryModel);
    }

    [HttpPost]
    public IActionResult CalculateOhmData(OhmViewModel model)
    {
        OhmModel = model;
        switch (model.Option)
        {
            case "voltage":
                OhmModel.Voltage = model.Resistance * model.Current;
                break;
            case "current":
                if (model.Resistance > 0)
                    OhmModel.Current = model.Voltage / model.Resistance;
                break;
            case "resistance":
                if (model.Current > 0)
                    OhmModel.Resistance = model.Voltage / model.Current;
                break;
        }

        return RedirectToAction("Ohm");
    }

    [HttpPost]
    public IActionResult CalculateSvtData(SvtViewModel model)
    {
        SvtModel = model;
        switch (model.Option)
        {
            case "time":
                if (model.Velocity > 0)
                    SvtModel.Time = model.Distance / model.Velocity;
                break;
            case "distance":
                SvtModel.Distance = model.Velocity * model.Time;
                break;
            case "velocity":
                if (model.Time > 0)
                    SvtModel.Velocity = model.Distance / model.Time;
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

    public IActionResult CalculateGeometryData(GeometryViewModel model)
    {
        GeometryModel = model;
        GeometryModel.Cubic = model.SideA * model.SideB * model.SideC;
        GeometryModel.Area =
            2 * ((model.SideA * model.SideB) + (model.SideA * model.SideC) + (model.SideA * model.SideC));
        return RedirectToAction("Geometry");
    }
}