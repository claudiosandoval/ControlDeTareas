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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnAcceder_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable Tabla = new DataTable();
                Tabla = NUsuario.Login(TxtEmail.Text.Trim(), TxtClave.Text.Trim());
                if (Tabla.Rows.Count<=0)
                {
                    MessageBox.Show("El email o la clave es incorrecta", "Acceso al sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //if (Convert.ToChar(Tabla.Rows[0][4])=='0')
                    //{
                    //   MessageBox.Show("Este usuario no esta activo", "Acceso al sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //else
                    //{
                        FrmPrincipal frm = new FrmPrincipal();
                        frm.id_usuario = Convert.ToInt32(Tabla.Rows[0][0]);
                        frm.id_rol = Convert.ToInt32(Tabla.Rows[0][1]);
                        frm.nombre = Convert.ToString(Tabla.Rows[0][2]);
                        frm.rol = Convert.ToString(Tabla.Rows[0][3]);
                        frm.Show();
                        this.Hide();
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
