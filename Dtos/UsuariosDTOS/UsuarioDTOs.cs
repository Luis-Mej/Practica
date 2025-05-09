﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;


namespace Dtos.UsuariosDTOS
{
    public class UsuarioDTOs
    {
        public UsuarioDTOs(string nombre, string codigoUsuario, string contrasenia)
        {
            Nombre = nombre;
            CodigoUsuario = codigoUsuario;
            Contrasenia = contrasenia;
        }

        public string Nombre { get; set; } = string.Empty;
        public string CodigoUsuario { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;

    }
}
