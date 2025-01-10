using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestiónDeClinica.Logica;

namespace GestiónDeClinica.Presentacion.Paciente
{
    public partial class FrmPaciente : Form
    {
        public bool modoEdicion = false;
        public int id = 0;
        public FrmPaciente(String modo)
        {
            InitializeComponent();
            if (modo != "n")
            {
                this.modoEdicion = true;
                this.id = int.Parse(modo);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "" || txtCedula.Text == "" || txtTelefono.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Por favor, complete los campos requeridos");
                return;
            }
            var dto_paciente = new Datos.dto_paciente
            {
                Nombre = txtNombre.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Genero = cmbGenero.SelectedItem.ToString(),
                FechaNac = dtpFechaNac.Value.Date,
                Cedula = txtCedula.Text.Trim()
            };
            var cls_paciente = new cls_paciente();
            try
            {
                if (!this.modoEdicion)
                {
                    var valorinsertar = cls_paciente.insertarPaciente(dto_paciente);
                    if (valorinsertar == "ok")
                    {
                        MessageBox.Show("Se guardó el registro con éxito");
                        this.Close();
                    }
                }
                else
                {
                    dto_paciente.IdPaciente = this.id;
                    if (cls_paciente.actualizarPaciente(dto_paciente) == "ok")
                    {
                        MessageBox.Show("Se actualizo con exito");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void FrmPaciente_Load(object sender, EventArgs e)
        {
            if (!this.modoEdicion)
            {
                lblFrmPaciente.Text = "Ingreso de Nuevo Personal Médico";
            }
            else
            {
                lblFrmPaciente.Text = "Actualización de Personal Médico";
                var clsPaciente = new cls_paciente();
                var paciente = clsPaciente.buscarPorId(this.id);
                txtNombre.Text = paciente.Nombre;
                txtTelefono.Text = paciente.Telefono;
                txtDireccion.Text = paciente.Direccion;
                txtEmail.Text = paciente.Email;
                txtCedula.Text = paciente.Cedula;
                cmbGenero.SelectedItem = paciente.Genero;
                dtpFechaNac.Value = paciente.FechaNac;

            }
        }
    }
}
