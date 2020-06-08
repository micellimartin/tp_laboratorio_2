using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesAbstractas
{
    public abstract class Universitario : Persona
    {
        private int legajo;

        #region Constructores

        public Universitario()
        {

        }

        public Universitario(int legajo, string nombre, string apellido, string dni, ENacionalidad nacionalidad)
            : base(nombre, apellido, dni, nacionalidad)
        {
            this.legajo = legajo;
        }

        #endregion

        #region Metodos

        protected abstract string PartiparEnClase();

        /// <summary>
        /// Determina si la instancia actual de la clase Universitario es igual al objeto pasado como parametro
        /// </summary>
        /// <param name="obj">objeto a comparar con la instancia actual</param>
        /// <returns>true si son iguales, false caso contrario</returns>
        public override bool Equals(object obj)
        {
            return this == (Universitario)obj;
        }

        /// <summary>
        /// Retorna un string con los datos del universitario
        /// </summary>
        /// <returns>cadena con la informacion del universitario</returns>
        protected virtual string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine("LEGAJO NUMERO: " + this.legajo);

            return sb.ToString();
        }

        #endregion

        #region Comparaciones

        /// <summary>
        /// Verifica si 2 universitarios sean iguales
        /// </summary>
        /// <param name="pg1">universitario numero 1 a comparar</param>
        /// <param name="pg2">universitario numero 2 a comparar</param>
        /// <returns>true si son iguales, false caso contrario</returns>
        public static bool operator == (Universitario pg1, Universitario pg2)
        {
            bool retorno = false;

            if(pg1.GetType() == pg2.GetType())
            {
                if(pg1.Dni == pg2.Dni || pg1.legajo == pg2.legajo)
                {
                    retorno = true;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Verifica si 2 universitarios son distintos
        /// </summary>
        /// <param name="pg1">universitario numero 1 a comparar</param>
        /// <param name="pg2">universitario numero 2 a comparar</param>
        /// <returns>true si son distintos, false si son iguales</returns>
        public static bool operator !=(Universitario pg1, Universitario pg2)
        {
            return (!(pg1 == pg2));
        }

        #endregion
    }
}
