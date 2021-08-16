using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant.Web
{
    public partial class Usuario
    {
        public Usuario()
        {
            Cuenta = new HashSet<Cuenta>();
            Reportemovimientos = new HashSet<Reportemovimiento>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public bool? Estatus { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string Descripcion { get; set; }
        public string RutaFoto { get; set; }
        public int? IdRol { get; set; }

        public virtual Role IdRolNavigation { get; set; }
        public virtual ICollection<Cuenta> Cuenta { get; set; }
        public virtual ICollection<Reportemovimiento> Reportemovimientos { get; set; }
    }
}
