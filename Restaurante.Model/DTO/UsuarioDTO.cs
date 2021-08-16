using Restaurante.Model.DTOs;
using System;
using System.Collections.Generic;

namespace Restaurante.Model
{
    public class UsuarioDTO 
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string nombreUsuario { get; set; }
        public string contrasena { get; set; }
        public Nullable<bool> estatus { get; set; }
        public string direccion { get; set; }
        public string descripcion { get; set; }
        public string rutaFoto { get; set; }
        public string telefono { get; set; }
        public string correoElectronico { get; set; }
        public Nullable<int> idRol { get; set; }
        public virtual RolDTO Roles { get; set; }

        // Additional fields
        public string token { get; set; }
    }
}