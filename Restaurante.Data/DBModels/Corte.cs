using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class Corte
    {
        public int Id { get; set; }
        public decimal TotalVentasEfectivo { get; set; }
        public decimal TotalVentasTarjeta { get; set; }
        public decimal TotalPropinas { get; set; }
        public decimal TotalSalidas { get; set; }
        public decimal TotalEntradas { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime FechaCierre { get; set; }
        public int IdUsuario { get; set; }
    }
}
