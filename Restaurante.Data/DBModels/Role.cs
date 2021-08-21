using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class Role
    {
        public Role()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Rol { get; set; }
        public bool? Estatus { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
