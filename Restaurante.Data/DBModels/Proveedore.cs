using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant.Web
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            Productos = new HashSet<Producto>();
        }

        public int Id { get; set; }
        public string NombreContacto { get; set; }
        public string Compania { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
