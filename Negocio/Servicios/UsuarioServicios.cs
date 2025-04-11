using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtos;
using Dtos.UsuariosDTOS;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Practica.Context;
using Practica.Models;

namespace Negocio.Servicios
{
    public class UsuarioServicios
    {
        private readonly PracticaContext _context;

        public UsuarioServicios(PracticaContext context)
        {  
            _context = context;
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
            var usuarioExiste = _context.Usuarios.FirstOrDefaultAsync(x=> x.Nombre==usuarioDTOs.Nombre);
            if (usuarioExiste == null)
            {
                return new ResponseBase<UsuarioDTOs>(400, "Usuario ya existente.");
            }

            Usuario usuarioregistro = new Usuario(usuarioDTOs.Nombre, usuarioDTOs.CodigoUsuario, usuarioDTOs.Contrasenia);
            _context.Usuarios.Add(usuarioregistro);
            await _context.SaveChangesAsync();

            return new ResponseBase<UsuarioDTOs>(200, "Usuario registrado.");
        }

        public async Task<ResponseBase<Usuario>> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return new ResponseBase<Usuario>(400, "El usuario no existe");
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return new ResponseBase<Usuario>(400, "El usuario no coincide con el id");
                }
                else
                {
                    throw;
                }
            }
            return new ResponseBase<Usuario>(500, "No se encuentra al usuario");
        }

        //public async Task<ResponseBase<Usuario>> DeleteUsuario(int id)
        //{
        //    var usuario = await _context.Usuarios.FindAsync(id);
        //    if (usuario == null)
        //    {
        //        return new ResponseBase<Usuario>(400, "El usuario no existe");
        //    }
        //    _context.Usuarios.Remove(usuario);
        //    await _context.SaveChangesAsync();
        //    return new ResponseBase<Usuario>(200, "Usuario eliminado");
        //}

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
