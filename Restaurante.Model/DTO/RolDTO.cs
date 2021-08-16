using System;
using System.Collections.Generic;

namespace Restaurante.Model.DTOs
{
    public class RolDTO
    {
        public RolDTO()
        {
            this.Permisos = new List<PermisoDTO>();
        }

        public int id { get; set; }
        public string rol { get; set; }
        public Nullable<bool> estatus { get; set; }
        public string descripcion { get; set; }

        public virtual List<PermisoDTO> Permisos { get; set; }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}