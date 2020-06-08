using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class DniInvalidoException : Exception
    {
        public DniInvalidoException() : base("DNI invalido")
        {

        }

        public DniInvalidoException(string mensaje) : base(mensaje)
        {

        }

        public DniInvalidoException(Exception innerException) : base(innerException.Message, innerException)
        {

        }

        public DniInvalidoException(string mensaje, Exception innerException) : base(mensaje, innerException)
        {

        }
    }
}
