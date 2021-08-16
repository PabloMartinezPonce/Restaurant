using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant.Web
{
    public partial class Role
    {
        public Role()
        {
            RelRolesPermisos = new HashSet<RelRolesPermiso>();
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Rol { get; set; }
        public bool? Estatus { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<RelRolesPermiso> RelRolesPermisos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
