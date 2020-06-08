using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EntidadesAbstractas;

namespace Clases_Instanciables
{
    public sealed class Profesor : Universitario
    {
        private Queue<Universidad.EClases> clasesDelDia;
        private static Random random;

        #region Constructores

        static Profesor()
        {
            random = new Random();
        }

        public Profesor()
        {

        }

        public Profesor(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad)
            : base(id, nombre, apellido, dni, nacionalidad)
        {
            this.clasesDelDia = new Queue<Universidad.EClases>();
            //Le asigno 2 clases aleatorias al profesor
            this._randomClases();
            this._randomClases();
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Asigna una clase al azar al profesor
        /// </summary>
        private void _randomClases()
        {
            //Los indicies del enum EClases van del 0 al 3
            switch (random.Next(4))
            {
                case ((int)Universidad.EClases.Programacion):
                    {
                        this.clasesDelDia.Enqueue(Universidad.EClases.Programacion);
                    }
                    break;

                case ((int)Universidad.EClases.Laboratorio):
                    {
                        this.clasesDelDia.Enqueue(Universidad.EClases.Laboratorio);
                    }
                    break;

                case ((int)Universidad.EClases.Legislacion):
                    {
                        this.clasesDelDia.Enqueue(Universidad.EClases.Legislacion);
                    }
                    break;

                case ((int)Universidad.EClases.SPD):
                    {
                        this.clasesDelDia.Enqueue(Universidad.EClases.SPD);
                    }
                    break;
            }
        }

        /// <summary>
        /// Devuelve un string con todas las clases que da el profesor
        /// </summary>
        /// <returns></returns>
        protected override string PartiparEnClase()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CLASES DEL DIA: ");

            foreach (Universidad.EClases item in this.clasesDelDia)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Retorna un string con los datos del profesor
        /// </summary>
        /// <returns>cadena con la informacion del profesor</returns>
        protected override string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.MostrarDatos());
            sb.AppendLine(this.PartiparEnClase());

            return sb.ToString();
        }

        /// <summary>
        /// Hace publica la informacion del profesor llamando a MostrarDatos
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.MostrarDatos();
        }

        #endregion

        #region Comparaciones

        /// <summary>
        /// Verifica si un profesor da una clase especifica o no
        /// </summary>
        /// <param name="i">profesor a comparar</param>
        /// <param name="clase">clase a comparar</param>
        /// <returns>true si el profesor da la clase, false caso contrario</returns>
        public static bool operator == (Profesor i, Universidad.EClases clase)
        {
            bool retorno = false;

            foreach(Universidad.EClases item in i.clasesDelDia)
            {
                if(item == clase)
                {
                    retorno = true;
                    break;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Verifica si un profesor NO da la clase especificada
        /// </summary>
        /// <param name="i">profesor a comparar</param>
        /// <param name="clase">clase a comparar</param>
        /// <returns>true si NO da la clase, false en caso de que SI de la clase</returns>
        public static bool operator !=(Profesor i, Universidad.EClases clase)
        {
            return (!(i == clase));
        }

        #endregion
    }
}
