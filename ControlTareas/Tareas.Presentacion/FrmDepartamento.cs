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
    public partial class FrmDepartamento : Form
    {
        private string nomAnterior; 
        public FrmDepartamento()
        {
            InitializeComponent();
        }

        //METODOS
        private void Formato()
        {
            DgvListado.Columns[2].Width = 200;
            DgvListado.Columns[3].Width = 150;
        }

        private void Limpiar()
        {
            TxtBuscar.Clear();
            txtId.Clear();
            TxtNombreDepto.Clear();
            BtnInsertar.Visible = true;
            BtnActualizar.Visible = false;
            ErrorIcono.Clear();

            DgvListado.Columns[0].Visible = false;
            BtnEliminar.Visible = false;
            ChkSeleccionar.Checked = false;
        }
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NDepartamento.Listar();
                this.Limpiar();
                LblTotal.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void CargarContrato()
        {
            try
            {
                try
                {
                    CboContrato.DataSource = NContrato.Listar();
                    CboContrato.ValueMember = "id_empresa";
                    CboContrato.DisplayMember = "nombre";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void CargarRolDepto()
        {
            try
            {
                try
                {
                    CboRol.DataSource = NDepartamento.CargarRolDepto();
                    CboRol.ValueMember = "id_rol";
                    CboRol.DisplayMember = "nombre";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void BtnListar_Click(object sender, EventArgs e)
        {
            this.Listar();
        }
        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtNombreDepto.Text == string.Empty || CboRol.Text == string.Empty || CboContrato.Text == string.Empty)
                {
                    this.MensajeError("Se deben rellenar todos los datos obligatorios, seran remarcados.");
                    ErrorIcono.SetError(TxtNombreDepto, "Ingrese un departamento.");
                    ErrorIcono.SetError(CboRol, "Ingrese un Rol");
                    ErrorIcono.SetError(CboContrato, "Ingrese un Contrato");
                }
                else
                {
                    Rpta = NDepartamento.Insertar(TxtNombreDepto.Text.Trim(), Convert.ToInt32(CboRol.SelectedValue), Convert.ToInt32(CboContrato.SelectedValue));
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se inserto de forma correcta el registro");
                        this.Limpiar();
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(Rpta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtNombreDepto.Text == string.Empty || CboRol.Text == string.Empty || CboContrato.Text == string.Empty)
                {
                    this.MensajeError("Se deben rellenar todos los datos obligatorios, seran remarcados.");
                    ErrorIcono.SetError(TxtNombreDepto, "Ingrese un departamento.");
                    ErrorIcono.SetError(CboRol, "Ingrese un Rol");
                    ErrorIcono.SetError(CboContrato, "Ingrese un Contrato");
                }
                else
                {
                    Rpta = NDepartamento.Actualizar(Convert.ToInt32(txtId.Text.Trim()), this.nomAnterior, TxtNombreDepto.Text.Trim(), Convert.ToInt32(CboRol.SelectedValue), Convert.ToInt32(CboContrato.SelectedValue));
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se actualizó de forma correcta el registro");
                        this.Limpiar();
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(Rpta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell Chkeliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                Chkeliminar.Value = !Convert.ToBoolean(Chkeliminar.Value);
            }

            //Cell content click para el icono de eliminar
            if (e.ColumnIndex == DgvListado.Columns["Eliminar"].Index)
            {
                try
                {
                    DialogResult opcion;
                    opcion = MessageBox.Show("Realmente deseas eliminar el registro?", "Control de Tareas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (opcion == DialogResult.OK)
                    {
                        int codigo;
                        string Rpta = "";

                        codigo = Convert.ToInt32(DgvListado.CurrentRow.Cells[3].Value.ToString());
                        Rpta = NDepartamento.Eliminar(codigo);

                        if (Rpta.Equals("OK"))
                        {
                            this.MensajeOk("Se elimino el registro: " + Convert.ToString(DgvListado.CurrentRow.Cells[4].Value));
                        }
                        else
                        {
                            this.MensajeError(Rpta);
                        }
                        this.Listar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }

            //Cell Content Click para el icono de editar
            if (e.ColumnIndex == DgvListado.Columns["Editar"].Index)
            {
                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = false;
                txtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["id"].Value);
                this.nomAnterior = Convert.ToString(DgvListado.CurrentRow.Cells["nombre"].Value);
                TxtNombreDepto.Text = Convert.ToString(DgvListado.CurrentRow.Cells["nombre"].Value);
                CboRol.Text = Convert.ToString(DgvListado.CurrentRow.Cells["rol"].Value);
                CboContrato.Text = Convert.ToString(DgvListado.CurrentRow.Cells["contrato"].Value);
                TabGeneral.SelectedIndex = 1;
            }
        }
        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;
                BtnEliminar.Visible = true;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;
                BtnEliminar.Visible = false;
            }
        }
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Realmente deseas eliminar el(los) registro(s)?", "Control de Tareas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[3].Value);
                            Rpta = NDepartamento.Eliminar(codigo);

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se elimino el registro: " + Convert.ToString(row.Cells[4].Value));
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DgvListado.DataSource = NDepartamento.Buscar(TxtBuscar.Text);
                this.Limpiar();
                LblTotal.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void DgvListado_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.DgvListado.Columns[e.ColumnIndex].Name == "Eliminar" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                DataGridViewButtonCell celBotonRemove = this.DgvListado.Rows[e.RowIndex].Cells["Eliminar"] as DataGridViewButtonCell;
                Icon icono = new Icon(Environment.CurrentDirectory + @"\\eliminar.ico");
                e.Graphics.DrawIcon(icono, e.CellBounds.Left + 8, e.CellBounds.Top + 3);

                this.DgvListado.Rows[e.RowIndex].Height = icono.Height + 8;
                this.DgvListado.Columns[e.ColumnIndex].Width = icono.Width + 16;

                e.Handled = true;
            }

            if (e.ColumnIndex >= 0 && this.DgvListado.Columns[e.ColumnIndex].Name == "Editar" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                DataGridViewButtonCell celBotonEdit = this.DgvListado.Rows[e.RowIndex].Cells["Editar"] as DataGridViewButtonCell;
                Icon icono = new Icon(Environment.CurrentDirectory + @"\\edit.ico");
                e.Graphics.DrawIcon(icono, e.CellBounds.Left + 8, e.CellBounds.Top + 3);

                this.DgvListado.Rows[e.RowIndex].Height = icono.Height + 8;
                this.DgvListado.Columns[e.ColumnIndex].Width = icono.Width + 16;

                e.Handled = true;
            }

        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            TabGeneral.SelectedIndex = 0;
        }
        private void FrmDepartamento_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarContrato();
            this.CargarRolDepto();
            this.Formato();
            DataGridViewButtonColumn btnEliminarClm = new DataGridViewButtonColumn();
            btnEliminarClm.Name = "Eliminar";
            DgvListado.Columns.Add(btnEliminarClm);
            DataGridViewButtonColumn btnEditarClm = new DataGridViewButtonColumn();
            btnEditarClm.Name = "Editar";
            DgvListado.Columns.Add(btnEditarClm);
        }

        //ERRORES
        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Control de tareas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOk(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Control de tareas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

       
    }
}
