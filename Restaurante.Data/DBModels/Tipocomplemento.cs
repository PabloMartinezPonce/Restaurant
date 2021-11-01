using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class Tipocomplemento
    {
        public Tipocomplemento()
        {
            Complementos = new HashSet<Complemento>();
            Productos = new HashSet<Producto>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Complemento> Complementos { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
