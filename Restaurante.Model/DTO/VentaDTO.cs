using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Model
{
    public class VentaDTO
    {
        [NotMapped]
        public decimal TotalTarjeta { get; set; }
        [NotMapped]
        public decimal TotalEfectivo { get; set; }
        [NotMapped]
        public decimal PropinaDiaria { get; set; }
    }
}