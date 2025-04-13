using System;
using System.Collections.Generic;

namespace Practica.Models;

public partial class Usuario
{

    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string CodigoUsuario { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public string Estado { get; set; } = null!;
}
