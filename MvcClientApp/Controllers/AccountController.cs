using Microsoft.AspNetCore.Mvc;
using MvcClientApp.Models;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MvcClientApp.Controllers
{
  public class AccountController : Controller
  {
    // IHttpClientFactory service ile ilgili HttpClient api bağlanabiliriz. instance alır.
    private readonly HttpClient apiClient;

    public AccountController(IHttpClientFactory httpClientFactory)
    {
      // apiclient instance al
      apiClient = httpClientFactory.CreateClient("apiClient");
    }

    public async Task<IActionResult> Login()
    {

      var postData = new LoginInputModel();
      postData.Email = "test@test.com";
      postData.Password = "123456";

      // elimizdeki nesneleri json string çevirdik.
      var payload = JsonSerializer.Serialize(postData);
      StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");

      // post işleminde 2. parametre olarak gönderdik.
      HttpResponseMessage tokenResponse = await apiClient.PostAsync("api/auth/token", content);

      // sunucudan bu mesaj döner
      // HttpResponseMessage

      // response body tokenResponse.Content, byte olarak binary formatında gelir

      var token = await tokenResponse.Content.ReadFromJsonAsync<TokenModel>();

      var handler = new JwtSecurityTokenHandler(); // JWT Decoder işlemleri için kullanacağız. // Token oluşturma ve içeriğini okumak için yukarıdaki sınıf kullanırı.z

      // token doğru bir valid bir formatta mı
      if (handler.CanReadToken(token.AccessToken))
      {
        // token oku dedikten sonra
        var jsonToken = handler.ReadJwtToken(token.AccessToken);

        // yukarıdaki kod ile token parşalanıp claimleri yakalanacak.

        // NameClaimType,RoleClaimType ezdik.

        var identity = new ClaimsIdentity(jsonToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "Name", "Role");
        ClaimsPrincipal principal = new ClaimsPrincipal(identity);


        // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name

        var authProps = new AuthenticationProperties();
        var tokens = new List<AuthenticationToken>();
        tokens.Add(new AuthenticationToken { Name = "access_token", Value = token.AccessToken });
        authProps.StoreTokens(tokens);

        // HttpContext.SignInAsync ile cookie based authentication yap
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProps);

        return Redirect("/");
      }

      return Unauthorized();

    }

    public async Task<IActionResult> LogOut()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

      return Redirect("/");
    }
  }
}
