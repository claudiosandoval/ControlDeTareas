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
    public partial class FrmTarea : Form
    {
        public FrmTarea()
        {
            InitializeComponent();
        }

        private string DescAnterior;

        public void Formato()
        {
            DgvListado.Columns[0].Visible = false;
            BtnEliminar.Visible = false;
            DgvListado.Columns[2].Width = 200;
            //DgvListado.Columns[9].Width = 150;
            DgvListado.Columns[10].Width = 200;

        }
        public void Limpiar()
        {
            txtId.Clear();
            TxtDescripcion.Clear();
            TxtDuracionDias.Clear();
            BtnInsertar.Visible = true;
            BtnActualizar.Visible = false;
        }
        private void FrmTarea_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarFuncion();
            this.Formato();
            this.CargarFuncionBuscar();
            this.CargarDepaBuscar();
            DataGridViewButtonColumn btnEditarClm = new DataGridViewButtonColumn();
            btnEditarClm.Name = "Editar";
            DgvListado.Columns.Add(btnEditarClm);
        }
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NTarea.Listar();
                this.Limpiar();
                this.Formato();
                LblTotal.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtDescripcion.Text == string.Empty || DtpFechaInicio.Text == string.Empty || TxtDuracionDias.Text == string.Empty || CboFuncion.Text == string.Empty)
                {
                    this.MensajeError("Se deben rellenar todos los datos obligatorios, seran remarcados.");
                    ErrorIcono.SetError(TxtDescripcion, "Ingrese una descripcion.");
                    ErrorIcono.SetError(DtpFechaInicio, "Ingrese una fecha de inicio.");
                    ErrorIcono.SetError(TxtDuracionDias, "Ingrese una duracion de la tarea en dias.");
                    ErrorIcono.SetError(CboFuncion, "Ingrese una funcion");
                }
                else
                {
                    //string fecha = DtpFechaTermino.Value.ToShortTimeString();
                    Rpta = NTarea.Insertar(TxtDescripcion.Text.Trim(), Convert.ToDateTime(DtpFechaInicio.Value.ToShortDateString()), Convert.ToInt32(TxtDuracionDias.Text.Trim()), Convert.ToInt32(CboFuncion.SelectedValue));
                    //MessageBox.Show(fecha);
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
        private void BtnListar_Click(object sender, EventArgs e)
        {
            this.Listar();
        }
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Se eliminarán las tareas seleccionadas, desea realmente eliminarlas?", "Control de Tareas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string Rpta = "";

                    codigo = Convert.ToInt32(DgvListado.CurrentRow.Cells[1].Value.ToString());
                    Rpta = NTarea.Eliminar(codigo);

                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se elimino el registro: " + Convert.ToString(DgvListado.CurrentRow.Cells[1].Value));
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
        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtDescripcion.Text == string.Empty || DtpFechaInicio.Text == string.Empty || TxtDuracionDias.Text == string.Empty || CboFuncion.Text == string.Empty)
                {
                    this.MensajeError("Se deben rellenar todos los datos obligatorios, seran remarcados.");
                    ErrorIcono.SetError(TxtDescripcion, "Ingrese una descripcion.");
                    ErrorIcono.SetError(DtpFechaInicio, "Ingrese una fecha de inicio.");
                    ErrorIcono.SetError(TxtDuracionDias, "Ingrese un departamento.");
                    ErrorIcono.SetError(CboFuncion, "Ingrese una funcion");
                }
                else
                {
                    Rpta = NTarea.Actualizar(Convert.ToInt32(txtId.Text.Trim()), this.DescAnterior, TxtDescripcion.Text.Trim(), Convert.ToDateTime(DtpFechaInicio.Text.Trim()), Convert.ToInt32(TxtDuracionDias.Text.Trim()),Convert.ToInt32(CboFuncion.SelectedValue));
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
        private void CargarFuncion()
        {
            try
            {
                CboFuncion.DataSource = NFuncion.Listar();
                CboFuncion.ValueMember = "id_funcion";
                CboFuncion.DisplayMember = "Descripcion";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void CargarFuncionBuscar()
        {
            try
            {
                CboBuscar.DataSource = NFuncion.Listar();
                CboBuscar.ValueMember = "id_funcion";
                CboBuscar.DisplayMember = "Descripcion";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void CargarDepaBuscar()
        {
            try
            {
                CboBuscarDepa.DataSource = NDepartamento.Listar();
                CboBuscarDepa.ValueMember = "id";
                CboBuscarDepa.DisplayMember = "nombre";
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
            //Cell Content Click para el icono de editar
            if (e.ColumnIndex == DgvListado.Columns["Editar"].Index)
            {
                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = false;
                txtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["id_tarea"].Value);
                this.DescAnterior = Convert.ToString(DgvListado.CurrentRow.Cells["descripcion"].Value);
                TxtDescripcion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["descripcion"].Value);
                DtpFechaInicio.Text = Convert.ToString(DgvListado.CurrentRow.Cells["fecha_inicio"].Value);
                TxtDuracionDias.Text = Convert.ToString(DgvListado.CurrentRow.Cells["duracion_dias"].Value);
                CboFuncion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["funcion"].Value);
                TabGeneral.SelectedIndex = 1;
            }
        }
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DgvListado.DataSource = NTarea.Buscar(Convert.ToString(CboBuscar.Text));
                //this.Limpiar();
                //this.Formato();
                LblTotal.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void BtnBuscar2_Click(object sender, EventArgs e)
        {
            try
            {
                DgvListado.DataSource = NTarea.Buscar(Convert.ToString(CboBuscarDepa.Text));
                //this.Limpiar();
                //this.Formato();
                LblTotal.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            TabGeneral.SelectedIndex = 0;
            this.Limpiar();
        }
        private void DgvListado_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
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
