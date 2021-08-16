
using System;

namespace Restaurant.Web.Common
{
    public class QueriesTxt
    {
        #region USER QUERIES
        public static readonly string userSelectByUser = @"SELECT * FROM [dbo].[Table] WHERE [nombreUsuario] = @nombreUsuario";
        public static readonly string insertUser = "INSERT INTO [dbo].[Usuarios] ([nombre],[apellido],[nombreUsuario],[contrasena],[estatus],[direccion],[correoElectronico],[telefono],[descripcion],[rutaFoto],[idRol])" +
                                                "VALUES (@nombre, @apellido, @nombreUsuario, @contrasena, @estatus, @direccion, @correoElectronico , @telefono  , @descripcion, @rutaFoto, @idRol)";

        public static readonly string updateUser = @"UPDATE [dbo].[Usuarios]
                                                      SET [nombre] = @nombre
                                                         ,[apellido] = @apellido
                                                         ,[nombreUsuario] =@nombreUsuario
                                                         ,[contrasena] = @contrasena
                                                         ,[estatus] = @estatus
                                                         ,[direccion] = @direccion
                                                         ,[correoElectronico] = @correoElectronico
                                                         ,[telefono] = @telefono
                                                         ,[descripcion] = @descripcion
                                                         ,[rutaFoto] = @rutaFoto
                                                         ,[idRol] = @idRol
                                                    WHERE [id] = @id";
        #endregion

        #region DYNAMIC QUERIES
        public static readonly string dynamicDelete = @"DELETE FROM [dbo].[Table] WHERE [id] = @id";
        public static readonly string dynamicSelectAll = @"SELECT * FROM [dbo].[Table]";
        public static readonly string dynamicSelectById = @"SELECT * FROM [dbo].[Table] WHERE [id] = @id";
        #endregion

       
    }
}