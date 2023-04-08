using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using WebApiLab.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiLab.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class ProductsController : ControllerBase
  {
    // GET: api/<ProductsController>
    [HttpGet("list")]
    public IActionResult Get()
    {
      var plist = new List<ProductDto>();
      plist.Add(new ProductDto
      {
        Id = 1,
        Name = "P1",
        Price = 10,
        Stock = 10
      });

      return Ok(plist); // status code 200 döndürür
    }

    // GET api/<ProductsController>/5
    [HttpGet("detail/{id}")]
    [Consumes(MediaTypeNames.Application.Xml)] // json değil xml döner
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)] // 200 başarı kodu dönebilir. Dönüş Tipi ProductDtodur.
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] // 500 server hata kodu dönebilir
    public IActionResult Get(int? id)
    {
      if(id == null)
      {
        return NotFound();
      }

      var data = new ProductDto
      {
        Id = (int)id,
        Name = "test",
        Price = 10,
        Stock = 10
      };

      return Ok(data);
    }

    // POST api/<ProductsController>
    [HttpPost("create")]
    public IActionResult Post([FromBody] ProductDto model)
    {
      //[FromBody] data body üzerinden taşınacak anlamına gelir.
      // [FromRoute] ve [FromQuery] gibi farklı yoldan veri taşımayı sağlayan attributelarda vardır.

      if (!ModelState.IsValid)
      {
        
        return BadRequest(); // 400 statuscode param istenilen formatta değil
       
      }

      return Created($"api/products/detail/{model.Id}", model);
    }

    // Put methodları ve delete methodları NoContent 204 status code döner. standartı budur. 
    // bir çok şirket sonucu OK tipinde de döndürme eğilimindedir.
    // PUT api/<ProductsController>/5
    [HttpPut("update/{id}")]
    public IActionResult Put(int id, [FromBody] ProductUpdateDto model)
    {
    
      // id ile kaydı buluruz. put işlemlerine id koymak bir standarttır.
      // update işlemini yaparız

      return NoContent();
    }

    //  Delete işlemleri 204 status code döner. standartı budur. 
    // DELETE api/delete/5
    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
      // Delete işlemleri yapalım

      return NoContent();
    }



    // Enlem boylam bilgisini route üzerinden post edelim
    // api/products/change-size/10/20?id=1

    // https://localhost:7097/api/Products/change-size/1/3?id=1'

    [HttpPut("change-size/{height}/{width}")]
    public IActionResult ChangeProductSize(int id,[FromRoute] int height, [FromRoute] int width)
    {
      // Delete işlemleri yapalım
      // [FromRoute] Url üzerinden post edilecek bir data gönderilmek istenirse kullanırız.
      // genelde güvenlik sebebi ile tercih edilmez. [FromBody] kullanılır.

      return NoContent();
    }
  }
}
