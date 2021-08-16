using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant.Web
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
