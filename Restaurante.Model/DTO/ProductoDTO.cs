using System;
using System.Collections.Generic;

namespace Restaurante.Model
{
    public class ProductoDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
        public Nullable<int> stock { get; set; }
        public Nullable<decimal> precioCosto { get; set; }
        public Nullable<decimal> precioVenta { get; set; }
        public Nullable<bool> activo { get; set; }
        public Nullable<decimal> descuento { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> idCategoria { get; set; }
        public Nullable<int> idProveedor { get; set; }

        public virtual CategoriaDTO Categorias { get; set; }
        public virtual ProveedorDTO Proveedores { get; set; }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}