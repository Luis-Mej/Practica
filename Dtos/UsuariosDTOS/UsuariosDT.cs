using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.UsuariosDTOS
{
    public class UsuariosDT
    {
        public UsuariosDT()
        {
        }

        public UsuariosDT(string nombre, string codigoUsuario)
        {
            Nombre = nombre;
            CodigoUsuario = codigoUsuario;
        }

        public string Nombre { get; set; } = string.Empty;
        public string CodigoUsuario { get; set; } = string.Empty;
    }
}
