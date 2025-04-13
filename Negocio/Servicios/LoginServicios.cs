using Dtos.UsuariosDTOS;
using Dtos;
using Encryptar.Encriptador;
using Entidades.Context;
using JWT.JwtServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Negocio.Servicios
{
    public class LoginServicios
    {
        private readonly PracticaContext _context;
        private readonly ITokenServicio _tokenServicio;

        public LoginServicios(PracticaContext context, ITokenServicio tokenServicio)
        {
            _context = context;
            _tokenServicio = tokenServicio;
        }

        public async Task<ResponseBase<string>> Login(UsuarioLoginDTO loginDto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Nombre == loginDto.Nombre || x.CodigoUsuario == loginDto.Nombre);

            if (usuario == null || string.IsNullOrEmpty(usuario.Contrasenia) || usuario.Contrasenia != loginDto.Contrasenia)
            {
                return new ResponseBase<string>(400, "Credenciales incorrectas");
            }

            var usuarioDto = new UsuariosDT(usuario.Nombre, usuario.CodigoUsuario);
            string token = _tokenServicio.CrearToken(usuarioDto);
            return new ResponseBase<string>(200, "Login exitoso", token);
        }
    }
}
