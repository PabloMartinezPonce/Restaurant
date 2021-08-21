using Restaurante.Model;
using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class Venta : VentaDTO
    {
        public int Id { get; set; }
        public decimal? Propina { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Total { get; set; }
        public string Metodopago { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdCuenta { get; set; }

        public virtual Cuenta IdCuentaNavigation { get; set; }
    }
}
