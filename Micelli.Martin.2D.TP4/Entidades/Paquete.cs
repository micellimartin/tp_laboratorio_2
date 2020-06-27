using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Paquete : IMostrar<Paquete>
    {
        public enum EEstado { Ingresado, EnViaje, Entregado };

        public delegate void DelegadoEstado(object sender, EventArgs e);
        public event DelegadoEstado InformaEstado;

        private string direccionEntrega;
        private EEstado estado;
        private string trackingID;

        public Paquete (string direccionEntrega, string trackingID)
        {
            this.estado = Paquete.EEstado.Ingresado;
            this.direccionEntrega = direccionEntrega;
            this.trackingID = trackingID;        
        }

        #region Propiedades

        public string DireccionEntrega
        {
            get
            {
                return this.direccionEntrega;
            }
            set
            {
                this.direccionEntrega = value;
            }
        }

        public EEstado Estado
        {
            get
            {
                return this.estado;
            }
            set
            {
                this.estado = value;
            }
        }

        public string TrackingID
        {
            get
            {
                return this.trackingID;
            }
            set
            {
                this.trackingID = value;
            }
        }

        #endregion

        #region Metodos

        public void MockCicloDeVida()
        {
            while (this.estado != Paquete.EEstado.Entregado)
            {
                System.Threading.Thread.Sleep(4000);

                switch (this.estado)
                {
                    case EEstado.Ingresado:
                        this.estado = EEstado.EnViaje;
                        break;
                    case EEstado.EnViaje:
                        this.estado = EEstado.Entregado;
                        break;
                }

                InformaEstado.Invoke(this, new EventArgs());
            }

            PaqueteDAO.Insertar(this);
        }

        public string MostrarDatos(IMostrar<Paquete> elemento)
        {
            Paquete p = (Paquete)elemento;

            string retorno = String.Format("{0} para {1}\n", p.trackingID, p.direccionEntrega);

            return retorno;
        }

        public override string ToString()
        {
            return this.MostrarDatos(this);
        }
      
        #endregion

        #region Comparaciones

        public static bool operator ==(Paquete p1, Paquete p2)
        {
            return p1.trackingID == p2.trackingID;
        }

        public static bool operator !=(Paquete p1, Paquete p2)
        {
            return !(p1 == p2);
        }

        #endregion
    }
}
