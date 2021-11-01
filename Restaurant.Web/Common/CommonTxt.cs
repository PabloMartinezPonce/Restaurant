
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
            var exeption = string.Empty;
            var message = string.Empty;

            if (ex.Message.Contains("See the inner exception"))
                exeption = ex.InnerException.Message;
            else
                exeption = ex.Message;


            if (exeption.Contains("Cannot delete or update a parent row"))
                message = "El registro no puede ser eliminado por que está siendo usado por el sistema. Puede realizar una eliminación lógica inactivando el registro.";
            else
                message = "La acción no fue procesada debido a : " + ex.Message;

            return new ResponseModel()
            {
                responseCode = 500,
                objectResponse = null,
                message = message,
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