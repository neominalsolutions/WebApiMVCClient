﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiLab.Dtos;

namespace WebAPISample.Infrastructure.JWT
{
  public class JwtTokenService : IJwtTokenService
  {
    private const double EXPIRE_HOURS = 1.0;

    // 1 saatlik access token üreten servis
    public TokenDto CreateAccessToken(ClaimsIdentity identity)
    {
      var key = Encoding.ASCII.GetBytes(JWTSettings.SecretKey);
      var tokenHandler = new JwtSecurityTokenHandler();
      var descriptor = new SecurityTokenDescriptor
      {
        Subject = identity,
        Expires = DateTime.UtcNow.AddHours(EXPIRE_HOURS),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(descriptor);
      var accessToken = tokenHandler.WriteToken(token);


      return new TokenDto
      {
        AccessToken = accessToken,
        TokenType = "bearer"
      };
    }
  }
}
