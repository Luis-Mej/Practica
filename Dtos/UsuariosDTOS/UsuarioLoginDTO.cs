using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.UsuariosDTOS
{
    public class UsuarioLoginDTO
    {
        public UsuarioLoginDTO(string nombre, string contrasena)
        {
            Nombre = nombre;
            Contrasena = contrasena;
        }

        public string Nombre { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}
