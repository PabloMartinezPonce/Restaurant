using System;

namespace Restaurante.Model
{
    public class FTPModel
    {
        public String idUsuario { get; set; }
        public string nombreArchivo { get; set; }
        public string URL { get; set; }
        public bool result { get; set; }
        public bool blobExist { get; set; }
    }
}