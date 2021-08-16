using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant.Web
{
    public partial class RelRolesPermiso
    {
        public int IdRol { get; set; }
        public int IdPermiso { get; set; }

        public virtual Permiso IdPermisoNavigation { get; set; }
        public virtual Role IdRolNavigation { get; set; }
    }
}
