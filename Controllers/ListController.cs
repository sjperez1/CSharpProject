using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CSharpProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpProject.Controllers;

public class ListController : Controller
{
    private int? userid
    {
        get{return HttpContext.Session.GetInt32("UUID");}
    }

    private bool isLoggedIn
    {
        get
        {
            return userid != null;
        }
    }

    // the following context things are needed to inject the context service into the controller
    private MyContext _context;

    public ListController(MyContext context)
    {
        _context = context;
    }

    [HttpGet("/dashboard")]
    public IActionResult Dashboard()
    {
        if(!isLoggedIn || userid == null)
        {
            return RedirectToAction("Index", "User");
        }

        List<List> allLists = _context.Lists
        .Where(list => list.UserId == userid)
        .ToList();

        return View("AllLists", allLists);
    }

    [HttpGet("/list/new")]
    public IActionResult NewList()
    {
        if(!isLoggedIn)
        {
            return RedirectToAction("Index", "User");
        }
        return View("NewList");
    }

    [HttpPost("/list/create")]
    public IActionResult CreateList(List newList)
    {
        if(!isLoggedIn || userid == null)
        {
            return RedirectToAction("Index", "User");
        }

        if(ModelState.IsValid == false)
        {
            return NewList();
        }

        newList.UserId = (int)userid;
        _context.Lists.Add(newList);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }
}