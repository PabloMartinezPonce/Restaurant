using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class Proveedore
    {
        public int Id { get; set; }
        public string NombreContacto { get; set; }
        public string Compania { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
    }
}
