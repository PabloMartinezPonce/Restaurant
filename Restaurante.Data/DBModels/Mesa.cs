using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class Mesa
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string RutaImagen { get; set; }
        public string TipoLugar { get; set; }
        public bool? Ocupada { get; set; }
        public bool? Reservada { get; set; }
        public DateTime? Fechareservada { get; set; }
    }
}
