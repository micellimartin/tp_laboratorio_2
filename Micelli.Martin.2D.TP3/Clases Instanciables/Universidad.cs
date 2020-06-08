using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;
using Archivos;

namespace Clases_Instanciables
{
    public class Universidad
    {
        public enum EClases { Programacion, Laboratorio, Legislacion, SPD };

        private List<Alumno> alumnos;
        private List<Jornada> jornadas;
        private List<Profesor> profesores;

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

        public List<Jornada> Jornadas
        {
            get
            {
                return this.jornadas;
            }
            set
            {
                this.jornadas = value;
            }
        }

        public List<Profesor> Instructores
        {
            get
            {
                return this.profesores;
            }
            set
            {
                this.profesores = value;
            }
        }

        public Jornada this[int i]
        {
            get
            {
                return this.jornadas[i];
            }
            set
            {
                this.jornadas[i] = value;
            }
        }

        #endregion

        #region Metodos

        public Universidad()
        {
            this.alumnos = new List<Alumno>();
            this.jornadas = new List<Jornada>();
            this.profesores = new List<Profesor>();
        }

        /// <summary>
        /// Devuelve una cadena con la informacion de la universidad que recibe como parametro
        /// </summary>
        /// <param name="uni">Universidad cuya informacion se devolvera en un string</param>
        /// <returns>Cadena con la informacion de la universidad</returns>
        private static string MostrarDatos(Universidad uni)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("JORNADA: ");

            foreach (Jornada j in uni.Jornadas)
            {
                sb.Append(j.ToString());
                sb.AppendLine("<---------------------------------------------->\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Hace publica la informacion de la universidad que devuelve el metodo MostrarDatos
        /// </summary>
        /// <returns>Cadena con la informacion de la universidad</returns>
        public override string ToString()
        {
            return MostrarDatos(this);
        }

        /// <summary>
        /// Serializa la informacion del objeto universidad en un archivo Xml
        /// </summary>
        /// <param name="uni">Objeto universidad a serializar</param>
        /// <returns>true si serializo con exito, false caso contrario</returns>
        public static bool Guardar(Universidad uni)
        {
            bool retorno = false;

            try
            {                
                Xml<Universidad> auxiliarXml = new Xml<Universidad>();

                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Universidad.xml";

                retorno = auxiliarXml.Guardar(path, uni);
            }
            catch(Exception e)
            {
                throw new ArchivosException(e);
            }
            
            return retorno;
        }

        /// <summary>
        /// Desrializa la informacion de un archivo Xml y la guarda en un objeto universidad
        /// </summary>
        /// <returns>El objeto universidad contenido en el archivo Xml</returns>
        public static Universidad Leer()
        {
            Universidad retorno = null;

            try
            {
                Xml<Universidad> auxiliarXml = new Xml<Universidad>();

                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Universidad.xml";

                auxiliarXml.Leer(path, out retorno);
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
        /// Verifica si son iguales una universidad y un alumno:
        /// Una universidad es igual a un alumno si el alumno esta en la lista de alumnos de la universidad
        /// </summary>
        /// <param name="g">universidad a comparar</param>
        /// <param name="a">alumno a comparar</param>
        /// <returns>true si son iguales, false caso contrario</returns>
        public static bool operator == (Universidad g, Alumno a)
        {
            bool retorno = false;

            if(!(a is null))
            {
                foreach(Alumno item in g.alumnos)
                {
                    if(item == a)
                    {
                        retorno = true;
                        break;
                    }
                }
            }

            return retorno;
        }

        public static bool operator !=(Universidad g, Alumno a)
        {
            return (!(g == a));
        }

        /// <summary>
        /// Agrega un alumno a la lista de alumnos de la universidad siempre y cuando este
        /// no pertenezca a dicha lista
        /// </summary>
        /// <param name="u">universidad a la que sera agregado el alumno</param>
        /// <param name="a">alumno a agregar</param>
        /// <returns>La universidad recibida como parametro con el alumno agregado</returns>
        public static Universidad operator +(Universidad u, Alumno a)
        {
            if(u != a)
            {
                u.alumnos.Add(a);
            }
            else
            {
                throw new AlumnoRepetidoException();
            }

            return u;
        }

        /// <summary>
        /// Verifica si son iguales una universidad y un profesor:
        /// Una universidad es igual a un profesor si este esta en la lista de profesores de la universidad
        /// </summary>
        /// <param name="g">universidad a comparar</param>
        /// <param name="i">profesor a comparar</param>
        /// <returns>true si son iguales, false caso contrario</returns>
        public static bool operator ==(Universidad g, Profesor i)
        {
            bool retorno = false;

            if (!(i is null))
            {
                foreach (Profesor item in g.profesores)
                {
                    if (item == i)
                    {
                        retorno = true;
                        break;
                    }
                }
            }

            return retorno;
        }

        public static bool operator !=(Universidad g, Profesor i)
        {
            return (!(g == i));
        }

        /// <summary>
        /// Agrega un profesor a la lista de profesors de la universidad siempre y cuando este
        /// no pertenezca a dicha lista
        /// </summary>
        /// <param name="u">universidad a la que sera agregado el profesor</param>
        /// <param name="i">profesor a agregar</param>
        /// <returns>La universidad con el profesor agregado si paso la validacion</returns>
        public static Universidad operator +(Universidad u, Profesor i)
        {
            if (u != i)
            {
                u.profesores.Add(i);
            }

            return u;
        }

        /// <summary>
        /// Verifica si son iguales una universidad y una clase:
        /// Una universidad es igual a un clase si existe un profesor dentro de la lista de profesores
        /// que de esa clase
        /// </summary>
        /// <param name="u">universidad a comparar</param>
        /// <param name="clase"></param>
        /// <returns>Devuelve un profesor que de esa clase, de no existir ese profesor sera nulo</returns>
        public static Profesor operator == (Universidad u, EClases clase)
        {
            Profesor retorno = null;

            foreach (Profesor item in u.profesores)
            {
                if (item == clase)
                {
                    retorno = item;
                    break;
                }
            }

            if (retorno is null)
            {
                throw new SinProfesorException();
            }

            return retorno;
        }

        public static Profesor operator !=(Universidad u, EClases clase)
        {
            Profesor retorno = null;

            foreach (Profesor item in u.profesores)
            {
                if (item != clase)
                {
                    retorno = item;
                    break;
                }
            }

            if (retorno is null)
            {
                throw new SinProfesorException();
            }

            return retorno;
        }

        /// <summary>
        /// Genera una nueva jornada y la agrega a la lista de jornadas de la universidad
        /// </summary>
        /// <param name="g">Universidad a la que se le agrega la jornada</param>
        /// <param name="clase">La clase dictada durante la jornada</param>
        /// <returns>La universidad pasada comp parametro con la jornada agregada</returns>
        public static Universidad operator +(Universidad g, EClases clase)
        {
            Profesor p = new Profesor();
            Jornada j = new Jornada(clase, (g == clase));

            foreach (Alumno item in g.Alumnos)
            {
                if (item == clase)
                {
                    j.Alumnos.Add(item);
                }
            }

            g.Jornadas.Add(j);

            return g;
        }

        #endregion
    }
}
