using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class Producto
    {
        public Producto()
        {
            RelCuentaProductos = new HashSet<RelCuentaProducto>();
            RelProductoComplementos = new HashSet<RelProductoComplemento>();
            RelProductoReceta = new HashSet<RelProductoRecetum>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public decimal? PrecioCosto { get; set; }
        public decimal? PrecioVenta { get; set; }
        public bool? Activo { get; set; }
        public decimal? Descuento { get; set; }
        public string Descripcion { get; set; }
        public bool? IdProductoItself { get; set; }
        public string RutaImagen { get; set; }
        public bool EsMultiStock { get; set; }
        public int? Cantidad { get; set; }
        public int? Stock { get; set; }
        public string Complementos { get; set; }
        public string ComplementosSelect { get; set; }
        public string TipoMedicion { get; set; }
        public int IdCategoria { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdTipoComplemento { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual Proveedore IdProveedorNavigation { get; set; }
        public virtual Tipocomplemento IdTipoComplementoNavigation { get; set; }
        public virtual ICollection<RelCuentaProducto> RelCuentaProductos { get; set; }
        public virtual ICollection<RelProductoComplemento> RelProductoComplementos { get; set; }
        public virtual ICollection<RelProductoRecetum> RelProductoReceta { get; set; }
    }
}
