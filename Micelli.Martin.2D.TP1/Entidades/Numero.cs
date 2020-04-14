using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Numero
    {
        private double numero;

        #region Constructores

        public Numero ()
        {
            this.numero = 0;
        }

        public Numero (double numero)
        {
            this.numero = numero;
        }

        public Numero (string strNumero)
        {
            SetNumero = strNumero;
        }

        #endregion

        #region Validacion

        private double ValidarNumero (string strNumero)
        {
            double retorno;
            double aux;

            if(double.TryParse(strNumero, out aux))
            {
                retorno = aux;
            }
            else
            {
                retorno = 0;
            }

            return retorno;
        }

        #endregion

        #region Set

        private string SetNumero
        {
            set
            {
                this.numero = ValidarNumero(value);
            }
        }

        #endregion

        #region Conversiones

        public string DecimalBinario (string numero)
        {
            string retorno;
            int aux;

            if(int.TryParse(numero, out aux))
            {
                aux = Math.Abs(aux);                 
                int resto;                               //Guardara los restos de las sucesivas divisiones.
                char[] charArray;                        //Se utilizara para revertir el orden de los restos.
                StringBuilder sb = new StringBuilder();  //Se usa para construir el numero binario.

                for (int i = 0; aux >= 1; i++)           //Aux es >=1 porque las divisiones tienen que frenar cuando el numero es menor que 2.
                {
                    resto = aux % 2;                     //Hago sucesivas divisiones por 2 y guardo el resto en la variable resto.
                    aux = aux / 2;                       //En cada iteracion el numero original se divide por 2.
                    sb.Append(resto.ToString());         //Construyo un string que va sumando los restos de las divisiones.
                }

                charArray = sb.ToString().ToCharArray(); //Transformo el stringbuilder en un char de arrays para luego poder revertir el orden de los restos.
                Array.Reverse(charArray);                //Invierto el orden de los restos para que quede el numero en orden correcto.
                retorno = new string(charArray);         //Transformo el char de arrays de nuevo a string.
            }
            else
            {
                retorno = "Valor inválido";
            }

            return retorno;
        }

        public string DecimalBinario (double numero)
        {
            return DecimalBinario(Math.Abs(numero).ToString());
        }

        public string BinarioDecimal (string binario)
        {
            string retorno;
            string aux_verficacion_bin = binario;
            int    aux_verifcacion_num;
            bool   verificacion_binario;
            bool   verificacion_numerica;

            //Verifico que el numero ingresado sea numerico.

            if(int.TryParse(binario, out aux_verifcacion_num))
            {
                verificacion_numerica = true;
            }
            else
            {
                verificacion_numerica = false;
            }

            //Verifico que el numero ingresado sea binario.
            //Si el numero es binario y remuevo todos los 0 y 1 deberia quedar una cadena vacia.
            //Si esto no se cumple es porque el numero pasado como argumento no es binario.

            if(aux_verficacion_bin.Replace("0", "").Replace("1", "").Length == 0)
            {
                verificacion_binario = true;
            }
            else
            {
                verificacion_binario = false;
            }

            //Si se pasan con exito las verificaciones hago la conversion.

            if (verificacion_numerica && verificacion_binario)
            {
                retorno = Math.Abs(Convert.ToInt32(binario, 2)).ToString();
            }
            else
            {
                retorno = "Valor inválido";
            }

            return retorno;
        }

        #endregion

        #region Operadores

        public static double operator +(Numero n1, Numero n2)
        {
            return n1.numero + n2.numero;
        }

        public static double operator - (Numero n1, Numero n2)
        {
            return n1.numero - n2.numero;
        }

        public static double operator *(Numero n1, Numero n2)
        {
            return n1.numero * n2.numero;
        }

        public static double operator /(Numero n1, Numero n2)
        {
            double retorno;

            if(n2.numero == 0)
            {
                retorno = double.MinValue;
            }
            else
            {
                retorno = n1.numero / n2.numero;
            }

            return retorno;
        }

        #endregion

    }
}
