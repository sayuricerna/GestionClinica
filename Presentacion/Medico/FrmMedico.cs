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

namespace GestiónDeClinica.Presentacion.Medico
{
    public partial class FrmMedico : Form
    {
        public bool modoEdicion = false;
        public int id = 0;
        public FrmMedico(string modo)
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
            if (txtNombre.Text == "" || txtTelefono.Text == "" || txtDireccion.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Por favor, complete los campos requeridos");
                return;
            }
            var dto_medico = new Datos.dto_medico
            {
                Nombre = txtNombre.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Especialidad = cmbEspecialidad.SelectedItem.ToString(),
                Email = txtEmail.Text.Trim(),
            };
            var cls_medico = new cls_medico();
            try
            {
                if (!this.modoEdicion)
                {
                    var valorinsertar = cls_medico.insertarMedico(dto_medico);
                    if (valorinsertar == "ok")
                    {
                        MessageBox.Show("Se guardó el registro con éxito");
                        this.Close();
                    }
                }
                else
                {
                    dto_medico.IdMedico = this.id;
                    if (cls_medico.actualizarMedico(dto_medico) == "ok")
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

        private void FrmMedico_Load(object sender, EventArgs e)
        {
            if (!this.modoEdicion)
            {
                lblFrmMedico.Text = "Ingreso de Nuevo Personal Médico";
            }
            else
            {
                lblFrmMedico.Text = "Actualización de Personal Médico";
                var clsMedico = new cls_medico();
                var medico = clsMedico.buscarPorId(this.id);
                txtNombre.Text = medico.Nombre;
                txtTelefono.Text = medico.Telefono;
                txtDireccion.Text = medico.Direccion;
                cmbEspecialidad.SelectedValue = medico.IdMedico;
                txtEmail.Text = medico.Email;

            }
        }
    }
}
