using System;
using System.Collections.Generic;

namespace Practica.Models;

public partial class Usuario
{
    public Usuario(string nombre, string codigoUsuario, string contrasenia)
    {
        Nombre = nombre;
        CodigoUsuario = codigoUsuario;
        Contrasenia = contrasenia;
    }

    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string CodigoUsuario { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public string Estado { get; set; } = null!;
}
