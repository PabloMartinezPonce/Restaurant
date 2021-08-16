using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant.Web
{
    public partial class Producto
    {
        public Producto()
        {
            Venta = new HashSet<Venta>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int? Stock { get; set; }
        public decimal? PrecioCosto { get; set; }
        public decimal? PrecioVenta { get; set; }
        public bool? Activo { get; set; }
        public decimal? Descuento { get; set; }
        public string Descripcion { get; set; }
        public string RutaImagen { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdProveedor { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual Proveedore IdProveedorNavigation { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
