using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Model
{
    public class CajaChicaModel
    {
        [NotMapped]
        public decimal TotalSalidas{ get; set; }
        [NotMapped]
        public decimal TotalEntradas { get; set; }
    }
}