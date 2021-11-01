using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Model
{
    public class EstadisticaProductoModel
    {
        [NotMapped]
        public decimal TotalVenta{ get; set; }
        [NotMapped]
        public decimal TotalCosto { get; set; }
        [NotMapped]
        public decimal TotalPropina{ get; set; }
        [NotMapped]
        public int TotalCantidadVentidos { get; set; }
    }
}