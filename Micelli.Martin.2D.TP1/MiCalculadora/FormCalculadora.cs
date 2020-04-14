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

namespace MiCalculadora
{
    public partial class FormCalculadora : Form
    {
        #region Manejadores de eventos

        public FormCalculadora()
        {
            InitializeComponent();

            //Cargo la lista de elementos del combobox

            cmbOperador.Items.Add("+");
            cmbOperador.Items.Add("-");
            cmbOperador.Items.Add("*");
            cmbOperador.Items.Add("/");
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnOperar_Click(object sender, EventArgs e)
        {
            string operador;

            //Si no se eligio ninguna operacion de la lista de la combobox, hago una suma.

            if (cmbOperador.SelectedItem == null)
            {
                operador = "+";
            }
            else
            {
                operador = cmbOperador.SelectedItem.ToString();
            }

            //Hago la operacion

            double resultadoOperacion = FormCalculadora.Operar(txtNumero1.Text, txtNumero2.Text, operador);

            //Si se dividio por 0 muestra un mensaje de error en la calculadora

            if (resultadoOperacion == double.MinValue)
            {
                lblResultado.Text = "No se puede divividir entre 0";
            }
            else
            {
                lblResultado.Text = resultadoOperacion.ToString();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConvertirABinario_Click(object sender, EventArgs e)
        {
            //Instancio un objeto Numero porque los metodos de conversion son de instancia

            Numero n = new Numero();
            double aux_validacion;

            //Valido que el valor de lblResultado sea un numero porque puede contener el mensaje
            //de error que se muestra al dividir por 0, o el mensaje de valor invalido.

            if (double.TryParse(lblResultado.Text, out aux_validacion))
            {
                lblResultado.Text = n.DecimalBinario(lblResultado.Text);
            }
        }

        private void btnConvertirADecimal_Click(object sender, EventArgs e)
        {
            //Instancio un objeto Numero porque los metodos de conversion son de instancia

            Numero n = new Numero();
            double aux_validacion;

            //Valido que el valor de lblResultado sea un numero porque puede contener el mensaje
            //de error que se muestra al dividir por 0, o el mensaje de valor invalido.

            if (double.TryParse(lblResultado.Text, out aux_validacion))
            {
                lblResultado.Text = n.BinarioDecimal(lblResultado.Text);
            }
        }

        #endregion

        #region Metodos auxiliares

        private void Limpiar ()
        {
            lblResultado.Text = "";           
            txtNumero1.Text = "";
            txtNumero2.Text = "";
            cmbOperador.SelectedIndex = 0;
        }

        private static double Operar (string numero1, string numero2, string operador)
        {
            double retorno;
            Numero n1 = new Numero(numero1);
            Numero n2 = new Numero(numero2);

            retorno = Calculadora.Operar(n1, n2, operador);

            return retorno;
        }

        #endregion
    }
}
