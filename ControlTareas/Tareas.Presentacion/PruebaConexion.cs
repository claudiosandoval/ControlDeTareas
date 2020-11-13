using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;
//using Tareas.Datos;

namespace Tareas.Presentacion
{
    public partial class PruebaConexion : Form
    {
        public PruebaConexion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleConnection OraCon = new OracleConnection();
            try
            {
                //OraCon = Conexion.getInstancia().CrearConexion();
                OraCon.Open();
                MessageBox.Show("Conectado a la BD");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (OraCon.State == ConnectionState.Open) OraCon.Close();
            }
        }
    }
}
