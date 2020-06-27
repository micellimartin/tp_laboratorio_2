using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Entidades
{
    public class Correo : IMostrar<List<Paquete>>
    {
        private List<Thread> mockPaquetes;
        private List<Paquete> paquetes;

        public List<Paquete> Paquetes
        {
            get
            {
                return this.paquetes;
            }

            set
            {
                this.paquetes = value;
            }
        }

        public Correo()
        {
            this.mockPaquetes = new List<Thread>();
            this.paquetes = new List<Paquete>();
        }

        #region Metodos

        public string MostrarDatos(IMostrar<List<Paquete>> elementos)
        {
            Correo auxiliarCorreo = (Correo)elementos;
            string retorno = string.Empty;

            foreach (Paquete p in auxiliarCorreo.paquetes)
            {
                retorno += string.Format("{0} para {1} ({2}) \n", p.TrackingID, p.DireccionEntrega, p.Estado.ToString());
            }

            return retorno;
        }

        public void FinEntregas()
        {
            foreach (Thread t in this.mockPaquetes)
            {
                if (t != null && t.IsAlive)
                {
                    t.Abort();
                }
            }
        }

        #endregion

        #region Operadores

        public static Correo operator +(Correo c, Paquete p)
        {
            if (c.paquetes is null)
            {
                c.paquetes.Add(p);
            }
            else
            {
                foreach (Paquete item in c.paquetes)
                {
                    if (item == p)
                    {
                        throw new TrackingIdRepetidoException("El paquete ya se encuentra en la lista");
                    }
                }

                c.paquetes.Add(p);
            }

            Thread t = new Thread(p.MockCicloDeVida);
            c.mockPaquetes.Add(t);
            t.Start();

            return c;
        }

        #endregion
    }
}
