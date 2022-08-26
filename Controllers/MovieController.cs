using CSharpProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSharpProject.Controllers;

public class MovieController : Controller
{
    [HttpGet("/movies/search")]
    public IActionResult SearchPage()
    {
        return View("SearchPage");
    }
}