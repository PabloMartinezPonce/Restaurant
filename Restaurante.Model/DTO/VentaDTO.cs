using System;
using System.Collections.Generic;

namespace Restaurante.Model
{
    public class VentaDTO
    {
        public int id { get; set; }
        public Nullable<int> unidades { get; set; }
        public Nullable<decimal> precioVenta { get; set; }
        public Nullable<decimal> descuento { get; set; }
        public int idProducto { get; set; }
        public int idCuenta { get; set; }

        public virtual CuentaDTO Cuentas { get; set; }
        public virtual ProductoDTO Productos { get; set; }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}