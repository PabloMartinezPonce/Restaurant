using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.Model
{
    public class ResponseModel
    {
        public int responseCode { get; set; }
        public string message { get; set; }
        public dynamic objectResponse { get; set; }
    }
}
