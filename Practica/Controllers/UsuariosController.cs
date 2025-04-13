using Microsoft.AspNetCore.Mvc;
using Dtos.UsuariosDTOS;
using Negocio.Servicios;
using JWT.JwtServicios;

namespace Practica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioServicios _usuarioServicio;
        private readonly ITokenServicio _tokenServicio;

        public UsuariosController(UsuarioServicios usuarioServicio, ITokenServicio tokenServicio)
        {
            _usuarioServicio = usuarioServicio;
            _tokenServicio = tokenServicio;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var resultado = await _usuarioServicio.GetUsuarioDTO();
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioDTOs usuarioDto)
        {
            var resultado = await _usuarioServicio.PostUsuarioDTO(usuarioDto);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] UsuarioDTOs usuarioDto)
        {
            var resultado = await _usuarioServicio.PutUsuario(id, usuarioDto);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var resultado = await _usuarioServicio.DeleteUsuarioDTO(id);
            return StatusCode(resultado.StatusCode, resultado);
        }
    }
}
