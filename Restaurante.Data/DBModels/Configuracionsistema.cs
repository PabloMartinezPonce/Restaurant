using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class Configuracionsistema
    {
        public int Id { get; set; }
        public string Llave { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
    }
}
