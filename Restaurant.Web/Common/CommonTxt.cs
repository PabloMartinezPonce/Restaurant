
using Restaurante.Model;
using System;

namespace Restaurant.Web.Common
{
    public class CommonTxt
    {

        #region Métodos String
        public static string GetMessage(Exception ex)
        {
            string message = string.Empty;
            try
            {
                message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                //MvcApplication.log.Error(message);
            }
            catch (Exception exc)
            {
                message = "No se pudo detectar el error en la excepción: " + ex + ". debido a: " + exc;
                //MvcApplication.log.Error(message);
            }
            return message;
        }

        public static ResponseModel GetResponseError(Exception ex)
        {
            return new ResponseModel()
            {
                responseCode = 500,
                objectResponse = null,
                message = GetMessage(ex),
            };
        }

        public static ResponseModel GetNewResponse(int code, string message, dynamic response)
        {
            return new ResponseModel()
            {
                responseCode = code,
                objectResponse = response,
                message = message,
            };
        }
        #endregion

    }
}