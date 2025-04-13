using Microsoft.AspNetCore.Mvc;
using Dtos.UsuariosDTOS;
using Negocio.Servicios;

namespace Practica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginServicios _loginServicios;

        public LoginController(LoginServicios loginServicios)
        {
            _loginServicios = loginServicios;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO loginDto)
        {
            var respuesta = await _loginServicios.Login(loginDto);

            if (respuesta.StatusCode != 200)
                return BadRequest(respuesta);

            return Ok(respuesta);
        }
    }
}
