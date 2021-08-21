using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class RelCuentaProducto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Precio { get; set; }
        public string Cantidad { get; set; }
        public string Descuento { get; set; }
        public int? IdProducto { get; set; }
        public int? IdCuenta { get; set; }

        public virtual Cuenta IdCuentaNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
