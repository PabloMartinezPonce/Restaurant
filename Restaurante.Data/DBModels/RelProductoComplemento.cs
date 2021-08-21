using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class RelProductoComplemento
    {
        public int Id { get; set; }
        public int? IdProducto { get; set; }
        public int? IdComplemento { get; set; }

        public virtual Complemento IdComplementoNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
