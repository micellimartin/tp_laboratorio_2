using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class Calculadora
    {
        #region Validacion

        private static string ValidarOperador (string operador)
        {
            string retorno;

            if(operador != "+" && operador != "-" && operador != "*" && operador != "/")
            {
                retorno = "+";
            }
            else
            {
                retorno = operador;
            }

            return retorno;
        }

        #endregion

        #region Operar

        public static double Operar (Numero num1, Numero num2, string operador) //Revisar si hay que validar en division
        {
            double retorno = 0;
            string aux_validacion = ValidarOperador(operador);

            if(aux_validacion == "+")
            {
                retorno = num1 + num2;
            }

            if (aux_validacion == "-")
            {
                retorno = num1 - num2;
            }

            if (aux_validacion == "*")
            {
                retorno = num1 * num2;
            }

            if (aux_validacion == "/")
            {
                retorno = num1 / num2;
            }

            return retorno;
        }

        #endregion
    }
}
