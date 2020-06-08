using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;
using Archivos;

namespace Clases_Instanciables
{
    public class Jornada
    {
        private List<Alumno> alumnos;
        private Universidad.EClases clase;
        private Profesor instructor;

        #region Propiedades

        public List<Alumno> Alumnos
        {
            get
            {
                return this.alumnos;
            }
            set
            {
                this.alumnos = value;
            }
        }

        public Universidad.EClases Clase
        {
            get
            {
                return this.clase;
            }
            set
            {
                this.clase = value;
            }
        }
        
        public Profesor Instructor
        {
            get
            {
                return this.instructor;
            }
            set
            {
                this.instructor = value;
            }
        }

        #endregion

        #region Constructores

        private Jornada()
        {
            this.alumnos = new List<Alumno>();
        }

        public Jornada(Universidad.EClases clase, Profesor instructor)
            : this()
        {
            this.clase = clase;
            this.instructor = instructor;
        }

        #endregion       

        #region Metodos

        /// <summary>
        /// Hace publica la informacion de la jornada
        /// </summary>
        /// <returns>Una cadena con la informacion de la jornada</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("CLASE DE " + this.clase.ToString() + " POR " + this.instructor.ToString());
            sb.AppendLine("ALUMNOS: ");

            foreach (Alumno item in this.alumnos)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Genera un archivo .txt con la informacion de la jornada
        /// </summary>
        /// <param name="jornada">Objeto jornada a guardar en un .txt</param>
        /// <returns>true si guardo con exito, false caso contrario</returns>
        public static bool Guardar(Jornada jornada)
        {
            bool retorno = false;

            try
            {
                Texto t = new Texto();
                //Ruta del archivo a crear. Especifico que se cree en el escritorio
                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Jornada.txt";

                retorno = t.Guardar(path, jornada.ToString());
            }
            catch(Exception e)
            {
                throw new ArchivosException(e);
            }

            return retorno;
        }

        /// <summary>
        /// Levanta un archivo .txt de la ruta especificada, lo guarda en una cadena y la devuelve
        /// </summary>
        /// <returns>La cadena con la informacion del archivo .txt</returns>
        public static string Leer()
        {
            string retorno = string.Empty;

            try
            {
                Texto t = new Texto();
                //Ruta del archivo a leer
                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Jornada.txt";
                t.Leer(path,out retorno);
            }
            catch (Exception e)
            {
                throw new ArchivosException(e);
            }

            return retorno;
        }

        #endregion

        #region Operadores

        /// <summary>
        /// Verifica si una jornada y un alumno son iguales:
        /// Esto se da cuando el alumno forma parte de la lista de alumnos de la jornada
        /// </summary>
        /// <param name="j">jornada a comparar</param>
        /// <param name="a">alumno a comparar</param>
        /// <returns>true si son iguales, false caso contrario</returns>
        public static bool operator ==(Jornada j, Alumno a)
        {
            bool retorno = false;

            if(!(j is null))
            {
                foreach (Alumno item in j.alumnos)
                {
                    if (item == a)
                    {
                        retorno = true;
                        break;
                    }
                }
            }
            
            return retorno;
        }

        public static bool operator !=(Jornada j, Alumno a)
        {
            return (!(j == a));
        }

        /// <summary>
        /// Agrega un alumno a la lista de alumnos de la jornada si el mismo no forma parte de la lista
        /// </summary>
        /// <param name="j">jornada que contiene la lista de alumnos</param>
        /// <param name="a">alumno a agregar</param>
        /// <returns>la jornada pasada como parametro con el alumno agregado si tuvo exito</returns>
        public static Jornada operator +(Jornada j, Alumno a)
        {
            if (j != a)
            {
                j.alumnos.Add(a);
            }
            else
            {
                throw new AlumnoRepetidoException();
            }

            return j;
        }

        #endregion
    }
}
