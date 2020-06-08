using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Excepciones;

namespace Archivos
{
    public class Xml<T> : IArchivo<T>
    {
        /// <summary>
        /// Genera un archivo Xml con la informacion pasada como parametro
        /// </summary>
        /// <param name="archivo">Ruta del archivo a crear</param>
        /// <param name="datos">Informacion que contendra el archivo Xml</param>
        /// <returns>True si guardo con exito, false caso contrario</returns>
        public bool Guardar(string archivo, T datos)
        {
            bool retorno = false;

            try
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                TextWriter w = new StreamWriter(archivo);
                s.Serialize(w, datos);
                w.Close();

                retorno = true;
            }
            catch(Exception e)
            {
                throw new ArchivosException(e);
            }

            return retorno;           
        }

        /// <summary>
        /// Levanta la informacion de una rchivo Xml y la guarda en un tipo generico
        /// </summary>
        /// <param name="archivo">Ruta del archivo a leer</param>
        /// <param name="datos">Dato generico que contendra la informacion leida</param>
        /// <returns>True si leyo con exito, false caso contrario</returns>
        public bool Leer(string archivo,out T datos)
        {
            bool retorno = false;

            try
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                TextReader r = new StreamReader(archivo);
                datos = (T)s.Deserialize(r);
                r.Close();

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
