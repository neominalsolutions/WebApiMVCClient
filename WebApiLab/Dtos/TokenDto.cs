using System.Text.Json.Serialization;

namespace WebApiLab.Dtos
{
  public class TokenDto
  {
    public string AccessToken { get; set; }
    public string TokenType { get; set; }

  }
}
