using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Entidades
{
    public static class PaqueteDAO
    {
        public delegate void ErrorBaseDatosEventHandler(string mensajeError);
        public static event ErrorBaseDatosEventHandler ErrorBaseDatos;

        private static SqlCommand comando;
        private static SqlConnection conexion;

        static PaqueteDAO()
        {
            string conexionString = @"Data Source= .\SQLEXPRESS; Initial Catalog= correo-sp-2017; Integrated Security= True";
            conexion = new SqlConnection(conexionString);
            comando = new SqlCommand();

            comando.CommandType = System.Data.CommandType.Text;
            comando.Connection = conexion;
        }

        /// <summary>
        /// Guarda un objeto Paquete en la tabla Paquetes de la base de datos sql correo-sp-2017
        /// </summary>
        /// <param name="p">Paquete a guardar</param>
        /// <returns>true, si guardo con extio, false caso contrario</returns>
        public static bool Insertar(Paquete p)
        {
            bool retorno = false;

            try
            {
                string direccionPaquete = p.DireccionEntrega;
                string idPaquete = p.TrackingID;

                conexion.Open();
                comando.CommandText = $"INSERT INTO dbo.Paquetes(direccionEntrega,trackingID,alumno) VALUES('{direccionPaquete}','{idPaquete}','Martin Micelli')";               
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                ErrorBaseDatos.Invoke("No se pudo guardar la informacion en la base de datos");
            }
            finally
            {
                conexion.Close();
            }

            return retorno;
        }
    }
}
