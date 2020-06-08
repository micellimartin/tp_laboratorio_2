using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excepciones;

namespace Archivos
{
    public class Texto : IArchivo<string>
    {
        /// <summary>
        /// Genera un archivo .txt con la informacion pasada como parametro
        /// </summary>
        /// <param name="archivo">Ruta del archivo a crear</param>
        /// <param name="datos">Informacion que contendra el archivo .txt</param>
        /// <returns>True si guardo con exito, false caso contrario</returns>
        public bool Guardar(string archivo, string datos)
        {
            bool retorno = false;

            try
            {
                StreamWriter sw = new StreamWriter(archivo);

                sw.WriteLine(datos);

                sw.Close();

                retorno = true;
            }
            catch (Exception e)
            {
                throw new ArchivosException(e);
            }

            return retorno;           
        }

        /// <summary>
        /// Levanta la informacion de una rchivo .txt y la guarda en un string
        /// </summary>
        /// <param name="archivo">Ruta del archivo a leer</param>
        /// <param name="datos">Cadena que contendra la informacion leida</param>
        /// <returns>True si leyo con exito, false caso contrario</returns>
        public bool Leer(string archivo, out string datos)
        {
            bool retorno = false;

            try
            {
                StreamReader sr = new StreamReader(archivo);

                datos = sr.ReadToEnd();

                sr.Close();

                retorno = true;
            }
            catch (Exception e)
            {
                throw new ArchivosException(e);
            }

            return retorno;
        }
    }
}
