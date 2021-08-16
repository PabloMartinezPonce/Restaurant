using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant.Web
{
    public partial class Venta
    {
        public int Id { get; set; }
        public int? Unidades { get; set; }
        public decimal? PrecioVenta { get; set; }
        public decimal? Descuento { get; set; }
        public bool? EstaPagado { get; set; }
        public int IdProducto { get; set; }
        public int IdCuenta { get; set; }

        public virtual Cuenta IdCuentaNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
