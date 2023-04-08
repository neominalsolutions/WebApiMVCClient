using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApiLab.Dtos;
using WebAPISample.Infrastructure.JWT;

namespace WebApiLab.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IJwtTokenService _tokenService;

        public TokensController(IJwtTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // attributed based routing feature
        [HttpPost("token")]
        public IActionResult CreateToken([FromBody] LoginDto loginDto)
        {


            if(loginDto.Email == "test@test.com" && loginDto.Password == "123456")
            {
                var identity = new ClaimsIdentity(new Claim[]
            {
                    new Claim("Name", loginDto.Email),
                    new Claim("Role", "Admin"),
                    new Claim("UserId",Guid.NewGuid().ToString())
            });


                var token = _tokenService.CreateAccessToken(identity);

                return Ok(token);

            }

            return Unauthorized();

        }
    }
}
