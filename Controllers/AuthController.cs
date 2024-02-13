
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.services;


namespace PrimeiraAPI.Controllers
{

    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {

        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "andre" && password == "123456")
            {
                var token = TokenService.GenerateToken(new Model.Employee());
                return Ok(token);
            }
            return BadRequest("Invalid username or password");
        }
    }
}