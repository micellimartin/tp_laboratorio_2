using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;

namespace Clases_Instanciables
{
    public sealed class Alumno : Universitario
    {
        public enum EEstadoCuenta { AlDia, Deudor, Becado };

        private Universidad.EClases claseQueToma;
        private EEstadoCuenta estadoCuenta;

        #region Constructores

        public Alumno()
        {

        }

        public Alumno(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad, Universidad.EClases claseQueToma)
            : base(id, nombre, apellido, dni, nacionalidad)
        {
            this.claseQueToma = claseQueToma;
        }

        public Alumno(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad, Universidad.EClases claseQueToma, EEstadoCuenta estadoCuenta)
            : this(id, nombre, apellido, dni, nacionalidad, claseQueToma)
        {
            this.estadoCuenta = estadoCuenta;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Retorna un string con los datos del alumno
        /// </summary>
        /// <returns>cadena con la informacion del alumno</returns>
        protected override string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.MostrarDatos());
            sb.AppendLine("ESTADO DE LA CUENTA: " + this.estadoCuenta);
            sb.AppendLine(this.PartiparEnClase());

            return sb.ToString();
        }

        /// <summary>
        /// Hace publica la informacion del alumno llamando a MostrarDatos
        /// </summary>
        /// <returns>cadena con la informacion del alumno</returns>
        public override string ToString()
        {
            return this.MostrarDatos();
        }

        /// <summary>
        /// Retorna una cadena con el nombre de la clase que toma el alumno
        /// </summary>
        /// <returns>cadena con la informacion de la clase</returns>
        protected override string PartiparEnClase()
        {
            return "TOMA CLASE DE " + this.claseQueToma;
        }

        #endregion

        #region Comparaciones

        /// <summary>
        /// Verfiica si un alumno toma una clase especifica o no
        /// </summary>
        /// <param name="a">alumno a comparar</param>
        /// <param name="clase">clase a comparar</param>
        /// <returns>true si toma la clase, false caso contrario</returns>
        public static bool operator == (Alumno a, Universidad.EClases clase)
        {
            bool retorno = false;

            if(a.claseQueToma == clase && a.estadoCuenta != EEstadoCuenta.Deudor)
            {
                retorno = true;
            }

            return retorno;
        }

        /// <summary>
        /// Verifica si un alumno NO toma una clase espeicifica
        /// </summary>
        /// <param name="a">alumno a comparar</param>
        /// <param name="clase">clase a comparar</param>
        /// <returns>true si NO toma la clase, false en caso de que SI tome la clase</returns>
        public static bool operator !=(Alumno a, Universidad.EClases clase)
        {
            return (!(a == clase));
        }

        #endregion
    }
}
