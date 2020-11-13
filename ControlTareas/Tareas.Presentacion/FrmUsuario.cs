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
    public partial class FrmUsuario : Form
    {
        private string RutAnterior;
        private string EmailAnterior;
        public FrmUsuario()
        {
            InitializeComponent();
        }

        //METODOS
        private void Formato()
        {
            DgvListado.Columns[0].Visible = false;
            DgvListado.Columns[1].Visible = false;
            DgvListado.Columns[2].Visible = false;
            DgvListado.Columns[7].Width = 180;
            DgvListado.Columns[8].Width = 180;
            DgvListado.Columns[9].HeaderText= "TEL. MOVIL";
            DgvListado.Columns[10].HeaderText = "TEL. FIJO";
            DgvListado.Columns[14].Visible = false;

            BtnEliminar.Visible = false;
            BtnActualizar.Visible = false;
        }
        private void Limpiar()
        {
            TxtBuscar.Clear();
            ChkSeleccionar.Checked = false;
            txtId.Clear();
            TxtRut.Clear();
            TxtNombre.Clear();
            TxtApPaterno.Clear();
            TxtApMaterno.Clear();
            TxtDireccion.Clear();
            TxtTelefonoMovil.Clear();
            TxtTelefonoFijo.Clear();
            TxtEmail.Clear();
            TxtClave.Clear();


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
                DgvListado.DataSource = NUsuario.Listar();
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
        private void Buscar()
        {
            try
            {
                DgvListado.DataSource = NUsuario.Buscar(TxtBuscar.Text);
                LblTotal.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            this.Limpiar();
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

                if (string.IsNullOrEmpty(TxtTelefonoFijo.Text))
                {
                    TxtTelefonoFijo.Text = "0";
                }

                if (TxtRut.Text == string.Empty || TxtNombre.Text == string.Empty || TxtApPaterno.Text == string.Empty || TxtApMaterno.Text == string.Empty || TxtDireccion.Text == string.Empty || TxtEmail.Text == string.Empty || TxtTelefonoMovil.Text == string.Empty || CboComuna.Text == string.Empty || CboRol.Text == string.Empty || CboContrato.Text == string.Empty || TxtClave.Text == string.Empty)
                {
                    this.MensajeError("Debe rellenar todos los datos obligatorios (*), seran remarcados.");
                    ErrorIcono.SetError(TxtRut, "Ingrese un rut.");
                    ErrorIcono.SetError(TxtNombre, "Ingrese su Nombre.");
                    ErrorIcono.SetError(TxtApPaterno, "Ingrese un Apellido Paterno.");
                    ErrorIcono.SetError(TxtApMaterno, "Ingrese su Apellido Materno.");
                    ErrorIcono.SetError(TxtDireccion, "Ingrese su Direccion.");
                    ErrorIcono.SetError(TxtEmail, "Ingrese un Email.");
                    ErrorIcono.SetError(TxtTelefonoMovil, "Ingrese un Telefono Movil.");
                    ErrorIcono.SetError(CboComuna, "Ingrese una Comuna.");
                    ErrorIcono.SetError(CboRol, "Ingrese un Rol.");
                    ErrorIcono.SetError(CboContrato, "Ingrese un Contrato.");
                    ErrorIcono.SetError(TxtClave, "Ingrese una Clave");
                }
                else
                {
                    Rpta = NUsuario.Insertar(TxtRut.Text.Trim(), TxtNombre.Text.Trim(), TxtApPaterno.Text.Trim(), TxtApMaterno.Text.Trim(), TxtDireccion.Text.Trim(), TxtEmail.Text.Trim(), Convert.ToInt32(TxtTelefonoMovil.Text), Convert.ToInt32(TxtTelefonoFijo.Text.Trim()), Convert.ToInt32(CboComuna.SelectedValue), Convert.ToInt32(CboRol.SelectedValue), Convert.ToInt32(CboContrato.SelectedValue), TxtClave.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se inserto de forma correcta el registro");
                        this.Listar();
                        this.Limpiar();
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
                opcion = MessageBox.Show("Se eliminarán el(los) contrato(s) y el(los) departamento(s) asociado(s), realmente deseas eliminar el(los) registro(s)?", "Control de Tareas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[3].Value);  
                            Rpta = NUsuario.Eliminar(codigo);

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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
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
                    opcion = MessageBox.Show("Se eliminará el contrato y todos los departamentos asociados a el, realmente deseas eliminar el registro?", "Control de Tareas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (opcion == DialogResult.OK)
                    {
                        int codigo;
                        string Rpta = "";

                        codigo = Convert.ToInt32(DgvListado.CurrentRow.Cells[3].Value);
                        Rpta = NUsuario.Eliminar(codigo);

                        if (Rpta.Equals("OK"))
                        {
                            this.MensajeOk("Se elimino el registro: " + Convert.ToString(DgvListado.CurrentRow.Cells[5].Value));
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
                this.Listar();
            }
            //Cell Content Click para el icono de editar
            if (e.ColumnIndex == DgvListado.Columns["Editar"].Index)
            {
                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = false;
                txtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["id_funcionario"].Value);
                this.RutAnterior = Convert.ToString(DgvListado.CurrentRow.Cells["rut"].Value);
                TxtRut.Text = Convert.ToString(DgvListado.CurrentRow.Cells["rut"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["nombres"].Value);
                TxtApPaterno.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ap_paterno"].Value);
                TxtApMaterno.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ap_materno"].Value);
                TxtDireccion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["direccion"].Value);
                this.EmailAnterior = Convert.ToString(DgvListado.CurrentRow.Cells["email"].Value);
                TxtEmail.Text = Convert.ToString(DgvListado.CurrentRow.Cells["email"].Value);
                TxtTelefonoMovil.Text = Convert.ToString(DgvListado.CurrentRow.Cells["movil"].Value);
                TxtTelefonoFijo.Text = Convert.ToString(DgvListado.CurrentRow.Cells["fijo"].Value);
                CboComuna.Text = Convert.ToString(DgvListado.CurrentRow.Cells["comuna"].Value);
                CboRol.Text = Convert.ToString(DgvListado.CurrentRow.Cells["rol"].Value);
                CboContrato.Text = Convert.ToString(DgvListado.CurrentRow.Cells["contrato"].Value);
                //TxtClave.Text = Convert.ToString(DgvListado.CurrentRow.Cells["password"].Value);
                
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
        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";

                if (string.IsNullOrEmpty(TxtTelefonoFijo.Text))
                {
                    TxtTelefonoFijo.Text = "0";
                }

                if (TxtRut.Text == string.Empty || TxtNombre.Text == string.Empty || TxtApPaterno.Text == string.Empty || TxtApMaterno.Text == string.Empty || TxtDireccion.Text == string.Empty || TxtEmail.Text == string.Empty || TxtTelefonoMovil.Text == string.Empty || CboComuna.Text == string.Empty || CboRol.Text == string.Empty || CboContrato.Text == string.Empty)
                {
                    this.MensajeError("Debe rellenar todos los datos obligatorios (*), seran remarcados.");
                    ErrorIcono.SetError(TxtRut, "Ingrese un rut.");
                    ErrorIcono.SetError(TxtNombre, "Ingrese su Nombre.");
                    ErrorIcono.SetError(TxtApPaterno, "Ingrese un Apellido Paterno.");
                    ErrorIcono.SetError(TxtApMaterno, "Ingrese su Apellido Materno.");
                    ErrorIcono.SetError(TxtDireccion, "Ingrese su Direccion.");
                    ErrorIcono.SetError(TxtEmail, "Ingrese un Email.");
                    ErrorIcono.SetError(TxtTelefonoMovil, "Ingrese un Telefono Movil.");
                    ErrorIcono.SetError(CboComuna, "Ingrese una Comuna.");
                    ErrorIcono.SetError(CboRol, "Ingrese un Rol.");
                    ErrorIcono.SetError(CboContrato, "Ingrese un Contrato.");
                    
                }
                else
                {
                    Rpta = NUsuario.Actualizar(Convert.ToInt32(txtId.Text.Trim()), this.RutAnterior,TxtRut.Text.Trim(), TxtNombre.Text.Trim(), TxtApPaterno.Text.Trim(), TxtApMaterno.Text.Trim(), TxtDireccion.Text.Trim(), this.EmailAnterior,TxtEmail.Text.Trim(), Convert.ToInt32(TxtTelefonoMovil.Text), Convert.ToInt32(TxtTelefonoFijo.Text.Trim()), Convert.ToInt32(CboComuna.SelectedValue), Convert.ToInt32(CboRol.SelectedValue), Convert.ToInt32(CboContrato.SelectedValue), TxtClave.Text.Trim());
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
        //PARA FORMATIAR EL RUT
        private void TxtNumDocumento_KeyDown(object sender, KeyEventArgs e)
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
        //PARA CARGAR EL ROL EN EL COMBOBOX
        private void CargarRol()
        {
            try
            {
                CboRol.DataSource = NRol.Listar();
                CboRol.ValueMember = "id_rol";
                CboRol.DisplayMember = "nombre";
            }
            catch(Exception ex)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void CargarContrato()
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
            TabGeneral.SelectedIndex = 0;
            this.Limpiar();
        }
        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.Formato();
            this.CargarRol();
            this.CargarComuna();
            this.CargarContrato();
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

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable filter = (DataTable)DgvListado.DataSource;
                
                
                LblTotal.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
