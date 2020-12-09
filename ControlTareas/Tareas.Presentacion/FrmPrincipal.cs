using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tareas.Presentacion
{
    public partial class FrmPrincipal : Form
    {
        private int childFormNumber = 0;
        public int id_usuario;
        public int id_rol;
        public string nombre;
        public string rol;
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Opcion;
            Opcion = MessageBox.Show("Deseas salir del sistema?", "Control De Tareas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (Opcion == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void contratosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmContrato frm = new FrmContrato();
            frm.MdiParent = this;
            frm.Show();
        }

        private void departamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDepartamento frm = new FrmDepartamento();
            frm.MdiParent = this;
            frm.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRol frm = new FrmRol();
            frm.MdiParent = this;
            frm.Show();
        }

        private void usuariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmUsuario frm = new FrmUsuario();
            frm.MdiParent = this;
            frm.Show();
        }
        private void funcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFuncion frm = new FrmFuncion();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tareasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTarea frm = new FrmTarea();
            frm.MdiParent = this;
            frm.Show();
        }

        private void cargaDeTrabajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCargaTareas frm = new FrmCargaTareas();
            frm.MdiParent = this;
            frm.Show();
        }
        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            StBarraInferior.Text = "Desarrollado por Alumnos Duoc, Usuario: " + this.nombre;
            MessageBox.Show("Bienvenido a Control de Tareas: " + this.nombre, "Control de Tareas", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (this.rol.Equals("Administrador"))
            {
                MnuAdministrador.Enabled = true;
                MnuProcesos.Enabled = false;
                MnuFuncionario.Enabled = false;
                MnuReportes.Enabled = true;
            }
            else
            {
                if (this.rol.Equals("Funcionario"))
                {
                    MnuAdministrador.Enabled = false;
                    MnuProcesos.Enabled = false;
                    MnuFuncionario.Enabled = true;
                    MnuReportes.Enabled = false;
                }
                else
                {
                    if(this.rol.Equals("Diseñador de Procesos"))
                    {
                        MnuAdministrador.Enabled = false;
                        MnuProcesos.Enabled = true;
                        MnuFuncionario.Enabled = true;
                        MnuReportes.Enabled = false;
                    }
                }
            }
        }

       
    }
}
