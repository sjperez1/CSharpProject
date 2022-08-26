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

        ViewBag.listsByDate = _context.Lists
        .Where(list => list.UserId == userid).OrderBy(list => list.MarathonDate)
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

    [HttpGet("/list/{listId}/edit")]
    public IActionResult EditList(int listId)
    {
        if(!isLoggedIn)
        {
            return RedirectToAction("Index", "User");
        }
        List? oneList = _context.Lists.FirstOrDefault(list => list.ListId == listId);
        if(oneList == null)
        {
            return RedirectToAction("Dashboard");
        }

        return View("EditList", oneList);
    }

    [HttpPost("/list/{listId}/update")]
    public IActionResult UpdateList(int listId, List updatedList)
    {
        if(!isLoggedIn)
        {
            return RedirectToAction("Index", "User");
        }

        if(ModelState.IsValid == false)
        {
            return EditList(listId);
        }

        List? ListToBeUpdated = _context.Lists.FirstOrDefault(list => list.ListId == listId);

        if(ListToBeUpdated == null)
        {
            return RedirectToAction("Dashboard");
        }

        ListToBeUpdated.Title = updatedList.Title;
        ListToBeUpdated.Description = updatedList.Description;
        ListToBeUpdated.AddFilm = updatedList.AddFilm;
        ListToBeUpdated.MarathonDate = updatedList.MarathonDate;
        ListToBeUpdated.UpdatedAt = DateTime.Now;

        _context.Lists.Update(ListToBeUpdated);
        _context.SaveChanges();

        return RedirectToAction("Dashboard");
    }

    [HttpGet("delete/{listId}")]
    public IActionResult DeleteList(int listId)
    {
        if(!isLoggedIn)
        {
            return RedirectToAction("Index", "User");
        }

        // using Single rather than First because the latter can sometimes cause errors with the delete function
        List? OneList = _context.Lists.SingleOrDefault(list => list.ListId == listId);
        if(OneList == null)
        {
            return RedirectToAction("Dashboard");
        }
        _context.Lists.Remove(OneList);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    [HttpGet("/list/{listId}")]
    public IActionResult OneList(int listId)
    {
        if(!isLoggedIn)
        {
            return RedirectToAction("Index", "User");
        }
        List? oneList = _context.Lists.FirstOrDefault(list => list.ListId == listId);
        if(oneList == null)
        {
            return RedirectToAction("Dashboard");
        }
        return View("OneList", oneList);
    }
}