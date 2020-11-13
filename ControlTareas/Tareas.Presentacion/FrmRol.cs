using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarea.Negocio;

namespace Tareas.Presentacion
{
    public partial class FrmRol : Form
    {
        public FrmRol()
        {
            InitializeComponent();
        }
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NRol.Listar();
                this.Formato();
                LblTotal.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void Formato()
        {
            
            DgvListado.Columns[0].HeaderText = "ID ROL";
            DgvListado.Columns[1].Width = 200;
            //DgvListado.Columns[3].Visible = false;
            //DgvListado.Columns[2].DefaultCellStyle.BackColor = Color.Red;

            /*for (int i = 0; i < DgvListado.Rows.Count; i++)
            {
                string estado = DgvListado.Rows[i].Cells[3].Value.ToString();
                if (estado == "1")
                {
                    DgvListado.Rows[i].Cells[3].Value = "A";
                }
                else
                {
                    DgvListado.Rows[i].Cells[3].Value = "I";
                }
            }*/

        }
        private void FrmRol_Load(object sender, EventArgs e)
        {
            this.Listar();
        }
    }
}
