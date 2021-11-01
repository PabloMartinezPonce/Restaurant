using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class Cajachica
    {
        public int Id { get; set; }
        public string Motivo { get; set; }
        public string Tipo { get; set; }
        public decimal? Cantidad { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdCorte { get; set; }
    }
}
