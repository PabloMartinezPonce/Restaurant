using Restaurante.Model;
using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class Cuenta : CuentaDTO
    {
        public Cuenta()
        {
            RelCuentaProductos = new HashSet<RelCuentaProducto>();
            Venta = new HashSet<Venta>();
        }

        public int Id { get; set; }
        public bool? CuentaActiva { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime FechaCierre { get; set; }
        public string Descripcion { get; set; }
        public int? CantidadPersonas { get; set; }
        public decimal? Propina { get; set; }
        public int IdMesa { get; set; }
        public int IdEmpleado { get; set; }
        public int? IdCorte { get; set; }

        public virtual ICollection<RelCuentaProducto> RelCuentaProductos { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
