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
    public partial class FrmFuncion : Form
    {
        public FrmFuncion()
        {
            InitializeComponent();
        }

        private string DescAnterior;
        private void FrmFuncion_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.Formato();
            this.CargarDepto();
            this.CargarDeptoBuscar();
            DataGridViewButtonColumn btnEliminarClm = new DataGridViewButtonColumn();
            btnEliminarClm.Name = "Eliminar";
            DgvListado.Columns.Add(btnEliminarClm);
            DataGridViewButtonColumn btnEditarClm = new DataGridViewButtonColumn();
            btnEditarClm.Name = "Editar";
            DgvListado.Columns.Add(btnEditarClm);
        }
        private void Formato()
        {
            DgvListado.Columns[2].Width = 300;
            DgvListado.Columns[3].Width = 130;
            DgvListado.Columns[4].Width = 130;
            DgvListado.Columns[5].Width = 140;
            DgvListado.Columns[6].Width = 120;
        }
        private void Limpiar()
        {
            TxtId.Clear();
            TxtDescripcion.Clear();
            BtnInsertar.Visible = true;
            BtnActualizar.Visible = false;
            ErrorIcono.Clear();

            DgvListado.Columns[0].Visible = false;
            BtnEliminar.Visible = false;
        }
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NFuncion.Listar();
                this.Limpiar();
                //this.Formato();
                LblTotal.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
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
                    opcion = MessageBox.Show("Se eliminarán el(las) Tarea(s) asociado(s) a esta funcion, realmente deseas eliminar el(los) registro(s) ? ", "Control de Tareas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (opcion == DialogResult.OK)
                    {
                        int codigo;
                        string Rpta = "";

                        codigo = Convert.ToInt32(DgvListado.CurrentRow.Cells[3].Value.ToString());
                        Rpta = NFuncion.Eliminar(codigo);

                        if (Rpta.Equals("OK"))
                        {
                            this.MensajeOk("Se elimino el registro: " + Convert.ToString(DgvListado.CurrentRow.Cells[3].Value));
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
                TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["id_funcion"].Value);
                this.DescAnterior = Convert.ToString(DgvListado.CurrentRow.Cells["descripcion"].Value);
                TxtDescripcion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["descripcion"].Value);
                DtpFechaTermino.Text = Convert.ToString(DgvListado.CurrentRow.Cells["fecha_termino"].Value);
                CboDepto.Text = Convert.ToString(DgvListado.CurrentRow.Cells["nombre_depto"].Value);
                TabGeneral.SelectedIndex = 1;
            }
        }
        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtDescripcion.Text == string.Empty || DtpFechaTermino.Text == string.Empty || CboDepto.Text == string.Empty)
                {
                    this.MensajeError("Se deben rellenar todos los datos obligatorios, seran remarcados.");
                    ErrorIcono.SetError(TxtDescripcion, "Ingrese una descripcion.");
                    ErrorIcono.SetError(DtpFechaTermino, "Ingrese una fecha.");
                    ErrorIcono.SetError(CboDepto, "Ingrese un departamento.");
                }
                else
                {
                    //string fecha = DtpFechaTermino.Value.ToShortTimeString();
                    Rpta = NFuncion.Insertar(TxtDescripcion.Text.Trim(), Convert.ToDateTime(DtpFechaTermino.Value.ToShortDateString()), Convert.ToInt32(CboDepto.SelectedValue));
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
        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtDescripcion.Text == string.Empty || DtpFechaTermino.Text == string.Empty || CboDepto.Text == string.Empty)
                {
                    this.MensajeError("Se deben rellenar todos los datos obligatorios, seran remarcados.");
                    ErrorIcono.SetError(TxtDescripcion, "Ingrese una descripcion.");
                    ErrorIcono.SetError(DtpFechaTermino, "Ingrese una fecha de termino.");
                    ErrorIcono.SetError(CboDepto, "Ingrese un departamento.");
                }
                else
                {
                    Rpta = NFuncion.Actualizar(Convert.ToInt32(TxtId.Text.Trim()), this.DescAnterior, TxtDescripcion.Text.Trim(), Convert.ToDateTime(DtpFechaTermino.Text.Trim()), Convert.ToInt32(CboDepto.SelectedValue));
                    if (Rpta.Equals("OK"))
                    {
                        MessageBox.Show(this.DescAnterior);
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
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Se eliminarán el(las) Tarea(s) asociado(s) a esta funcion, realmente deseas eliminar el(los) registro(s) ? ", "Control de Tareas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string Rpta = "";

                    codigo = Convert.ToInt32(DgvListado.CurrentRow.Cells[3].Value.ToString());
                    Rpta = NFuncion.Eliminar(codigo);

                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se elimino el registro: " + Convert.ToString(DgvListado.CurrentRow.Cells[3].Value));
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
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DgvListado.DataSource = NFuncion.Buscar(Convert.ToInt32(CboBuscar.SelectedValue));
                this.Limpiar();
                //this.Formato();
                LblTotal.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void CargarDepto()
        {
            try
            {
                CboDepto.DataSource = NDepartamento.Listar();
                CboDepto.ValueMember = "id";
                CboDepto.DisplayMember = "nombre";
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void CargarDeptoBuscar()
        {
            try
            {
                CboBuscar.DataSource = NDepartamento.Listar();
                CboBuscar.ValueMember = "id";
                CboBuscar.DisplayMember = "nombre";


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

        //MENSAJES DE ERROR
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
