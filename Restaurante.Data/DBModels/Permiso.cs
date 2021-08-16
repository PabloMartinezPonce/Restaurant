using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant.Web
{
    public partial class Permiso
    {
        public Permiso()
        {
            RelRolesPermisos = new HashSet<RelRolesPermiso>();
        }

        public int Id { get; set; }
        public string Permiso1 { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<RelRolesPermiso> RelRolesPermisos { get; set; }
    }
}
