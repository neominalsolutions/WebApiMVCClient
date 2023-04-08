using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using MvcClientApp.Models;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace MvcClientApp.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

   


    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
      
    }

    public async Task<IActionResult> Index()
    {
      var result =  await HttpContext.AuthenticateAsync();

      if(result.Succeeded)
      {
        
      }
     
      return View();
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
}