using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MvcClientApp;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// mvc client istek atacaðý apilarý bu kýsýmda tanýmlar.
builder.Services.AddHttpClient("apiClient", opt =>
{
  opt.BaseAddress = new Uri("https://localhost:5002");
});


// cookie ile authenticate oluyoruz
var key = Encoding.ASCII.GetBytes(JWTSettings.SecretKey);
builder.Services.AddAuthentication(x =>
{
  x.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
  x.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(x => x.LoginPath = "/Account/Login");



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
