using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CSharpProject.Models;

namespace CSharpProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
