using System;
using System.Collections.Generic;

namespace Restaurante.Model
{
    public class MesaDTO
    {
        public int id { get; set; }
        public string mesa { get; set; }
        public string descripcion { get; set; }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}