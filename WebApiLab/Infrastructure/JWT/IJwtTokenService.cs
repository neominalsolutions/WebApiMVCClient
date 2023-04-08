using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiLab.Dtos;

namespace WebAPISample.Infrastructure.JWT
{
    public interface IJwtTokenService
    {
        TokenDto CreateAccessToken(ClaimsIdentity identity);
    }
}
