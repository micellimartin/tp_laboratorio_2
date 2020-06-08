using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;

namespace EntidadesAbstractas
{
    public abstract class Persona
    {
        public enum ENacionalidad { Argentino, Extranjero };

        private string apellido;
        private int dni;
        private ENacionalidad nacionalidad;
        private string nombre;

        #region Propiedades

        public string Apellido
        {
            get
            {
                return this.apellido;
            }
            set
            {
                //Si la validacion falla se devuelve un string vacio
                if(ValidarNombreApellido(value).Length>0)
                {
                    this.apellido = value;
                }
            }
        }

        public int Dni
        {
            get
            {
                return this.dni;
            }
            set
            {
                this.dni = ValidarDni(this.nacionalidad, value);
            }
        }

        public ENacionalidad Nacionalidad
        {
            get
            {
                return this.nacionalidad;
            }
            set
            {
                this.nacionalidad = value;
            }
        }

        public string Nombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                //Si la validacion falla se devuelve un string vacio
                if (ValidarNombreApellido(value).Length > 0)
                {
                    this.nombre = value;
                }
            }
        }

        public string StringToDni
        {
            set
            {
                this.dni = ValidarDni(this.nacionalidad, value);               
            }
        }

        #endregion

        #region Constructores

        public Persona()
        {

        }

        public Persona(string nombre, string apellido, ENacionalidad nacionalidad)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.nacionalidad = nacionalidad;
        }

        public Persona(string nombre, string apellido, int dni ,ENacionalidad nacionalidad)
            : this(nombre, apellido, nacionalidad)
        {
            this.dni = dni;
        }

        public Persona(string nombre, string apellido, string dni, ENacionalidad nacionalidad)
            : this(nombre, apellido, nacionalidad)
        {
            this.StringToDni = dni;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Retorna un string con los datos de la persona
        /// </summary>
        /// <returns>cadena con los datos de la persona</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("NOMBRE COMPLETO: " + this.apellido + ", " + this.nombre);
            sb.AppendLine("NACIONALIDAD: " + this.nacionalidad);

            return sb.ToString();
        }

        /// <summary>
        /// Valida el DNI segun el tipo de nacionalidad
        /// </summary>
        /// <param name="nacionalidad">nacionalidad de la persona</param>
        /// <param name="dato">dni a validar</param>
        /// <returns>el dni, siempre y cuando haya pasado las validaciones</returns>
        private int ValidarDni(ENacionalidad nacionalidad, int dato)
        {
            int retorno = dato;

            switch(nacionalidad)
            {
                case ENacionalidad.Argentino:
                    {
                        if(dato < 1 || dato > 89999999)
                        {
                            throw new NacionalidadInvalidaException();
                        }
                    }
                    break;

                case ENacionalidad.Extranjero:
                    {
                        if(dato < 90000000 || dato > 99999999)
                        {
                            throw new NacionalidadInvalidaException();
                        }
                    }
                    break;
            }

            return retorno;
        }

        /// <summary>
        /// Valida que el dni no tenga errores de formato: caracteres que no sean numeros o mas de 8 numeros en total
        /// </summary>
        /// <param name="nacionalidad">tipo de nacionalidad que se le pasa a la segunda etapa de la validacion</param>
        /// <param name="dato">Dni a validar</param>
        /// <returns>el dni validado por ambos metodos de validacion</returns>
        private int ValidarDni(ENacionalidad nacionalidad, string dato)
        {
            int retorno = 0;
            int auxValidacion = 0;

            bool cantCharsValida = dato.Length <= 8;
            bool esNumerico = int.TryParse(dato, out auxValidacion);

            if(esNumerico && cantCharsValida)
            {
                retorno = ValidarDni(nacionalidad, auxValidacion);
            }
            else
            {
                throw new DniInvalidoException();
            }

            return retorno;
        }

        /// <summary>
        /// Valida que el campo nombre o apellido no contengan caracteres invalidos
        /// </summary>
        /// <param name="dato">nombre o apellido a validar</param>
        /// <returns>el parametro dato si paso la validacion con exito o un string vacio si no es el caso</returns>
        private string ValidarNombreApellido(string dato)
        {
            string retorno = string.Empty;

            if(ValidarStringSoloLetras(dato))
            {
                retorno = dato;
            }

            return retorno;
        }

        /// <summary>
        /// Valida que el string recibido como parametro NO este vacio y que SOLO contenga letras
        /// </summary>
        /// <param name="cadena">cadena a validar</param>
        /// <returns>true si la cadena solo tiene letras, false si no es el caso</returns>
        public static bool ValidarStringSoloLetras(string cadena)
        {
            char[] alfabeto = "abcdefghijklmñnopqrstuvwxyz".ToCharArray();
            bool retorno = true;
            int contador = 0;

            //Verifico que la cadena NO este vacia.

            if (cadena == string.Empty)
            {
                retorno = false;
            }
            else
            {
                foreach (char c in cadena)
                {
                    for (int i = 0; i < alfabeto.Length; i++)
                    {
                        //Verifico que la cadena SOLO contenga letras

                        if (c.ToString().ToLower() == alfabeto[i].ToString())
                        {
                            contador++;
                        }
                    }
                }

                //Si estos 2 parametros NO coinciden es porque algun caracter de la cadena NO era una letra.

                if (contador != cadena.Length)
                {
                    retorno = false;
                }
            }

            return retorno;
        }

        #endregion
    }
}       