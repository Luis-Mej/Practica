using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtos;
using Dtos.UsuariosDTOS;
using Encryptar.Encriptador;
using JWT.JwtServicios;
using Microsoft.EntityFrameworkCore;
using Entidades.Context;
using Entidades.Models;

namespace Negocio.Servicios
{
    public class UsuarioServicios
    {
        private readonly PracticaContext _context;

        public UsuarioServicios(PracticaContext context)
        {  
            _context = context;
        }

        public async Task<ResponseBase<string>> Login(UsuarioLoginDTO loginDto, ITokenServicio tokenServicio)
        {
            var usuario = await _context.Usuarios
                .Where(x =>
                    (x.Nombre == loginDto.Nombre || x.CodigoUsuario == loginDto.Nombre) &&
                    x.Estado == "A")
                .FirstOrDefaultAsync();

            if (usuario == null || !Encriptador.CompararHash(loginDto.Contrasenia, usuario.Contrasenia))
            {
                return new ResponseBase<string>(400, "Credenciales incorrectas");
            }

            var usuarioDto = new UsuariosDT(usuario.Nombre, usuario.CodigoUsuario);
            string token = tokenServicio.CrearToken(usuarioDto);

            return new ResponseBase<string>(200, "Login exitoso", token);
        }

        public async Task<ResponseBase<List<UsuariosDT>>> GetUsuarioDTO()
        {
            var listaUsuarios = await _context.Usuarios.Select(x => new UsuariosDT()
            {
                CodigoUsuario = x.CodigoUsuario,
                Nombre = x.Nombre
            }).ToListAsync();

            return new ResponseBase<List<UsuariosDT>>(200, listaUsuarios);
        }

        public async Task<ResponseBase<UsuarioDTOs>> PostUsuarioDTO(UsuarioDTOs usuarioDTOs)
        {
            var usuarioExiste = await _context.Usuarios.FirstOrDefaultAsync(x=> x.Nombre==usuarioDTOs.Nombre);
            if (usuarioExiste != null)
            {
                return new ResponseBase<UsuarioDTOs>(400, "Usuario ya existente.");
            }

            var usuarioregistro = new Usuario
            {
                Nombre = usuarioDTOs.Nombre,
                CodigoUsuario = usuarioDTOs.CodigoUsuario,
                Contrasenia = usuarioDTOs.Contrasenia,
                Estado = "A"
            };

            _context.Usuarios.Add(usuarioregistro);
            await _context.SaveChangesAsync();

            return new ResponseBase<UsuarioDTOs>(200, "Usuario registrado.");
        }

        public async Task<ResponseBase<UsuarioDTOs>> PutUsuario(int id, UsuarioDTOs usuarioDTOs)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null || usuario.Estado != "A")
            {
                return new ResponseBase<UsuarioDTOs>(400, "El usuario no existe");
            }

            usuario.Nombre = usuarioDTOs.Nombre;
            usuario.CodigoUsuario = usuarioDTOs.CodigoUsuario;
            usuario.Contrasenia = usuarioDTOs.Contrasenia;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return new ResponseBase<UsuarioDTOs>(400, "El usuario no coincide con el id");
                }
                else
                {
                    throw;
                }
            }
            return new ResponseBase<UsuarioDTOs>(500, "No se encuentra al usuario");
        }

        public async Task<ResponseBase<UsuarioDTOs>> DeleteUsuarioDTO(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null || usuario.Estado != "A")
            {
                return new ResponseBase<UsuarioDTOs>(400, "El usuario no existe");
            }
            usuario.Estado = "I";
            await _context.SaveChangesAsync();
            return new ResponseBase<UsuarioDTOs>(200, "Usuario eliminado");
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
