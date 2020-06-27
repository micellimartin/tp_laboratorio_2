using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Entidades
{
    public static class GuardarString
    {
        /// <summary>
        /// Metodo de extension de la clase String. Guarda en un archivo de texto la informacion de los paquetes.
        /// </summary>
        /// <param name="texto">Contenido del archivo de texto</param>
        /// <param name="archivo">Nombre del archivo de texto</param>
        /// <returns>true, si guardo con extio, false caso contrario</returns>
        public static bool Guardar(this string texto, string archivo)
        {
            bool retorno = false;

            if (!string.IsNullOrEmpty(archivo))
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + archivo;

                StreamWriter Swriter;

                //Si el archivo no existe lo creo, si ya existe le agrego los datos nuevos
                if (!File.Exists(path))
                {
                    Swriter = new StreamWriter(path);
                }
                else
                {
                    Swriter = new StreamWriter(path, true);
                }

                using (Swriter)
                {
                    Swriter.WriteLine(texto);
                }

                retorno = true;
            }
            else
            {
                retorno = false;
            }

            return retorno;
        }
    }
}
