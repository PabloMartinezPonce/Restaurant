using System;
using System.Collections.Generic;

namespace Restaurante.Model
{
    public class PermisoDTO
    {
        public int id { get; set; }
        public string permiso { get; set; }
        public string descripcion { get; set; }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}