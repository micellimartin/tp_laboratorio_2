using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using static Entidades.PaqueteDAO;

namespace Micelli.Martin._2D.TP4
{
    public partial class FrmPpal : Form
    {
        private Correo correo;

        public FrmPpal()
        {
            InitializeComponent();
            this.correo = new Correo();
            PaqueteDAO.ErrorBaseDatos += PaqueteDAOErrorBaseDatos;
        }

        #region Metodos Auxiliares
     
        private void MostrarInformacion<T>(IMostrar<T> elemento)
        {
            if (!(elemento is null))
            {
                this.rtbMostrar.Text = elemento.MostrarDatos(elemento);

                try
                {
                    this.rtbMostrar.Text.Guardar("salida.txt");
                }
                catch (Exception)
                {
                    MessageBox.Show("No se puedo guardar el archivo de texto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ActualizarEstados()
        {
            this.lstEstadoIngresado.Items.Clear();
            this.lstEstadoEnViaje.Items.Clear();
            this.lstEstadoEntregado.Items.Clear();

            foreach (Paquete paquete in this.correo.Paquetes)
            {
                switch (paquete.Estado)
                {
                    case Paquete.EEstado.Ingresado:
                        {
                            lstEstadoIngresado.Items.Add(paquete);
                        }
                        break;

                    case Paquete.EEstado.EnViaje:
                        {
                            lstEstadoEnViaje.Items.Add(paquete);
                        }
                        break;

                    case Paquete.EEstado.Entregado:
                        {
                            lstEstadoEntregado.Items.Add(paquete);
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        private void paq_InformaEstado(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                Paquete.DelegadoEstado d = new Paquete.DelegadoEstado(paq_InformaEstado);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                this.ActualizarEstados();
            }
        }

        private void PaqueteDAOErrorBaseDatos(string mensaje)
        {
            if (this.InvokeRequired)
            {
                ErrorBaseDatosEventHandler d = new ErrorBaseDatosEventHandler(PaqueteDAOErrorBaseDatos);
                this.Invoke(d, new object[] { mensaje });
            }
            else
            {
                MessageBox.Show(mensaje, "Surgio un error con la base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Manejadores de eventos

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (this.txtDireccion.Text != string.Empty && this.mtxtTrackingId.Text != string.Empty)
            {
                Paquete p = new Paquete(this.txtDireccion.Text, this.mtxtTrackingId.Text);

                try
                {
                    this.correo += p;
                    p.InformaEstado += paq_InformaEstado;
                    this.ActualizarEstados();
                }
                catch (TrackingIdRepetidoException)
                {
                    MessageBox.Show("El paquete ya se agrego al listado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("El paquete debe tener direccion e ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
     
        private void btnMostrarTodos_Click(object sender, EventArgs e)
        { 
            this.MostrarInformacion<List<Paquete>>((IMostrar<List<Paquete>>)correo);
        }

        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MostrarInformacion<Paquete>((IMostrar<Paquete>)lstEstadoEntregado.SelectedItem);
        }
    
        private void FrmPpal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Esta realmente seguro de cerrar?", "Cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void FrmPpal_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.correo.FinEntregas();
        }

        #endregion
    }
}
