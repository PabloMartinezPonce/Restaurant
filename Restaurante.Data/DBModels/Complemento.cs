using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class Complemento
    {
        public Complemento()
        {
            RelProductoComplementos = new HashSet<RelProductoComplemento>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal? Precio { get; set; }
        public bool? Activo { get; set; }
        public string RutaImagen { get; set; }
        public int? IdTipoComplemento { get; set; }

        public virtual Tipocomplemento IdTipoComplementoNavigation { get; set; }
        public virtual ICollection<RelProductoComplemento> RelProductoComplementos { get; set; }
    }
}
