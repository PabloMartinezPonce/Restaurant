using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class RelProductoRecetum
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int IdProducto { get; set; }
        public int IdIngrediente { get; set; }

        public virtual Ingrediente IdIngredienteNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
