using System;
using System.Collections.Generic;

namespace Restaurante.Model
{
    public class CategoriaDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}