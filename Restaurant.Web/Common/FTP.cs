using FluentFTP;
using Microsoft.AspNetCore.Http;
using Restaurante.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Restaurante.Web.Common
{
    public static class FTP
    {
        private static FtpClient CreateFtpClient()
        {
            return new FtpClient("ftp.site4now.net", new NetworkCredential { UserName = "ftplaspaseras", Password = "LPB2021." });
        }

        public static FTPModel FtpUploadAsync(IFormFile postedFile)
        {
            try
            {
                using (FtpClient ftp = CreateFtpClient())
                {
                    Stream str = postedFile.OpenReadStream();
                    ftp.Upload(str, postedFile.FileName);
                }

                var result = new FTPModel
                {
                    result = true,
                    nombreArchivo = postedFile.FileName,
                    URL = "http://pablomtzponce-001-site1.ctempurl.com/images/productos/" + postedFile.FileName,
                    idUsuario = postedFile.FileName.Split(' ')[0].ToString(),
                    blobExist = true,
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}