using Microsoft.AspNetCore.Mvc;
using CSharpProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace CSharpProject.Controllers;

public class UserController : Controller
{
    private MyContext db;
    public UserController(MyContext context)
    {
        db = context;
    }
    private int? UUID
    {
        get
        {
            return HttpContext.Session.GetInt32("UUID");
        }
    }

    private bool loggedIn
    {
        get
        {
            return UUID != null;
        }
    }
    [HttpGet("/")]
    public IActionResult Index()
    {
        if(loggedIn)
        {
            return RedirectToAction("Dashboard", "List");
        }
        return View("Index");
    }


    [HttpPost("/register")]
    public IActionResult Register(User newUser)
    {
        if(ModelState.IsValid)
        {
            if(db.User.Any(user => user.email == newUser.email))
            {
                ModelState.AddModelError("email", "is taken");
            }
        }

        if(ModelState.IsValid == false)
        {
            return Index();
        }
        PasswordHasher<User> hashed = new PasswordHasher<User>();
        newUser.password = hashed.HashPassword(newUser, newUser.password);
        db.User.Add(newUser);
        db.SaveChanges();
        HttpContext.Session.SetInt32("UUID", newUser.userId);
        return RedirectToAction("Dashboard", "List");
    }


    [HttpGet("/login")]
    public IActionResult Enter()
    {
        if(loggedIn)
        {
            return RedirectToAction("Dashboard", "List");
        }
        return View("Index");
    }


    [HttpPost("/loggingin")]
    public IActionResult Login(LoginUser loginUser)
    {
        if(ModelState.IsValid == false)
        {
            return Enter();
        }

        User? dbUser = db.User.FirstOrDefault(loggedUser => loggedUser.email == loginUser.LoginEmail);

        if(dbUser == null)
        {
            ModelState.AddModelError("LoginEmail", "not found");
            return Enter();
        }

        PasswordHasher<LoginUser> hashed = new PasswordHasher<LoginUser>();
        PasswordVerificationResult pwCompare = hashed.VerifyHashedPassword(loginUser, dbUser.password, loginUser.LoginPassword);

        if(pwCompare == 0)
        {
            ModelState.AddModelError("LoginPassword", "is not correct");
            return Enter();
        }

        HttpContext.Session.SetInt32("UUID", dbUser.userId);
        return RedirectToAction("Dashboard", "List");
    }

    [HttpPost("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Index();
    }
}