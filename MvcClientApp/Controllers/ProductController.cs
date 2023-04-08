using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MvcClientApp.Models;
using System.Net.Http.Headers;

namespace MvcClientApp.Controllers
{
  public class ProductController : Controller
  {

    private readonly HttpClient apiClient;


    public ProductController(IHttpClientFactory httpClientFactory)
    {
      apiClient = httpClientFactory.CreateClient("apiClient");
    }
    public async Task<IActionResult> List()
    {
      var result1 = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

      // accessToken cookie üzerinde saklı olduğundan private bir resource erişimde access token gönderilecektir.
      var accessToken = await HttpContext.GetTokenAsync(CookieAuthenticationDefaults.AuthenticationScheme, "access_token");
      apiClient.DefaultRequestHeaders.Authorization
                      = new AuthenticationHeaderValue("Bearer", accessToken);
      // header a token ekle


      var result = await apiClient.GetAsync("api/products/list");

      if (result.IsSuccessStatusCode)
      {
        var response = await result.Content.ReadAsStringAsync();

        var data = System.Text.Json.JsonSerializer.Deserialize<List<ProductViewModel>>(response);

        return View(data);
      }

      return Unauthorized();

    }
  }
}
