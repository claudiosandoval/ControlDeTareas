using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarea.Negocio;

namespace Tareas.Presentacion
{
    public partial class FrmContrato : Form
    {

        private string NombreAnt;
        public FrmContrato()
        {
            InitializeComponent();
        }

        //METODOS
        private void Formato()
        {
            DgvListado.Columns[1].Visible = false;
            DgvListado.Columns[2].HeaderText= "RUT EMPRESA";
            DgvListado.Columns[2].Width = 150;
            DgvListado.Columns[3].HeaderText = "CONTRATO";
            //DgvListado.Columns[2].Width = 150;
            //DgvListado.Columns[3].Width = 150;
            //TxtBuscar.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtBuscar.Text);
            //DgvListado.Columns[3].HeaderText = "DESCRIPCIÓN";
        }
        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtRut.Clear();
            TxtNombre.Clear();
            TxtDireccion.Clear();
            txtId.Clear();
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
                DgvListado.DataSource = NContrato.Listar();
                //this.Formato();
                this.Limpiar();
                LblTotal.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void CargarComuna()
        {
            try
            {
                CboComuna.DataSource = NContrato.ListarComuna();
                CboComuna.ValueMember = "id_comuna";
                CboComuna.DisplayMember = "nombre";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void BtnListar_Click(object sender, EventArgs e)
        {
            this.Listar();
        }
        private void Buscar()
        {
            try
            {
                DgvListado.DataSource = NContrato.Buscar(TxtBuscar.Text);
                this.Limpiar();
                LblTotal.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }
        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtRut.Text == string.Empty || TxtNombre.Text == string.Empty || TxtDireccion.Text == string.Empty || CboComuna.Text == string.Empty)
                {
                    this.MensajeError("Se deben rellenar todos los datos obligatorios, seran remarcados.");
                    ErrorIcono.SetError(TxtRut, "Ingrese un rut de empresa.");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre.");
                    ErrorIcono.SetError(TxtDireccion, "Ingrese una direccion.");
                    ErrorIcono.SetError(CboComuna, "Ingrese una comuna.");
                }
                else
                {
                    Rpta = NContrato.Insertar(TxtRut.Text.Trim(), TxtNombre.Text.Trim(), TxtDireccion.Text.Trim(), Convert.ToInt32(CboComuna.SelectedValue));
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
                if (TxtRut.Text == string.Empty || TxtNombre.Text == string.Empty || TxtDireccion.Text == string.Empty || CboComuna.Text == string.Empty)
                {
                    this.MensajeError("Se deben rellenar todos los datos obligatorios, seran remarcados.");
                    ErrorIcono.SetError(TxtRut, "Ingrese un rut de empresa.");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre.");
                    ErrorIcono.SetError(TxtDireccion, "Ingrese una direccion.");
                    ErrorIcono.SetError(CboComuna, "Ingrese una comuna.");
                }
                else
                {
                    Rpta = NContrato.Actualizar(Convert.ToInt32(txtId.Text.Trim()) ,TxtRut.Text.Trim(), this.NombreAnt,TxtNombre.Text.Trim(), TxtDireccion.Text.Trim(), Convert.ToInt32(CboComuna.SelectedValue));
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
        /*private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = false;
                txtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["id_contrato"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["nombre_empresa"].Value);
                TxtDireccion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["direccion"].Value);
                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception)
            {
                MessageBox.Show("Seleccione desde la celda nombre para modificar");
            }

        }*/
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Se eliminarán el(los) funcionarios(s) y el(los) departamento(s) asociado(s) a este contrato, realmente deseas eliminar el(los) registro(s)?", "Control de Tareas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[3].Value);
                            Rpta = NContrato.Eliminar(codigo);

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se elimino el registro: " + Convert.ToString(row.Cells[5].Value));
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
                    opcion = MessageBox.Show("Se eliminarán el(los) funcionarios(s) y el(los) departamento(s) asociado(s) a este contrato, realmente deseas eliminar el(los) registro(s) ? ", "Control de Tareas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (opcion == DialogResult.OK)
                    {
                        int codigo;
                        string Rpta = "";

                        codigo = Convert.ToInt32(DgvListado.CurrentRow.Cells[3].Value.ToString());
                        Rpta = NContrato.Eliminar(codigo);

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se elimino el registro: " + Convert.ToString(DgvListado.CurrentRow.Cells[5].Value));
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
                txtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["id_empresa"].Value);
                TxtRut.Text = Convert.ToString(DgvListado.CurrentRow.Cells["rut_empresa"].Value);
                this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["nombre"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["nombre"].Value);
                TxtDireccion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["direccion"].Value);
                TabGeneral.SelectedIndex = 1;
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
        
        //Formatear el rut de la empresa
        private void TxtRut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Back)
            {
                if (TxtRut.Text.ToString().Length == 8)
                {
                    TxtRut.Text = TxtRut.Text.ToString() + "-";
                    TxtRut.Select(TxtRut.Text.Length, 0);
                }
            }
        }
        private void FrmContrato_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.Formato();
            this.CargarComuna();
            DataGridViewButtonColumn btnEliminarClm = new DataGridViewButtonColumn();
            btnEliminarClm.Name = "Eliminar";
            DgvListado.Columns.Add(btnEliminarClm);
            DataGridViewButtonColumn btnEditarClm = new DataGridViewButtonColumn();
            btnEditarClm.Name = "Editar";
            DgvListado.Columns.Add(btnEditarClm);
        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            TabGeneral.SelectedIndex = 0;
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
