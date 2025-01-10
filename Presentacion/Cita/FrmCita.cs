using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestiónDeClinica.Datos;
using GestiónDeClinica.Logica;

namespace GestiónDeClinica.Presentacion.Cita
{
    public partial class FrmCita : Form
    {
        public bool modoEdicion = false;
        public int id = 0;
        public FrmCita(string modo)
        {
            InitializeComponent();
            if (modo != "n")
            {
                this.modoEdicion = true;
                this.id = int.Parse(modo);
            }

        }
        /*PARA cmbMedico y cmbPciente de FrmCita*/
        public void cargamedico()
        {
            var listamedicos = new cls_medico();
            cmbMedico.DataSource = listamedicos.leer();
            cmbMedico.ValueMember = "IdMedico";
            cmbMedico.DisplayMember = "Nombre";
        }

        public void cargapaciente()
        {
            var listapacientes = new cls_paciente();
            cmbPaciente.DataSource = listapacientes.leer();
            cmbPaciente.ValueMember = "IdPaciente";
            cmbPaciente.DisplayMember = "Cedula";
        }
        private void FrmCita_Load(object sender, EventArgs e)
        {
            cargamedico();
            cargapaciente();
            if (!this.modoEdicion)
            {
                lblFrmCita.Text = "Ingreso de Nueva Cita";
            }
            else
            {
                lblFrmCita.Text = "Actualización de Cita";
                var clsCita = new cls_cita();
                var cita = clsCita.buscarPorId(this.id);
                dtpFechaCita.Value = cita.FechaCita;
                cmbMedico.SelectedValue = cita.IdMedico;
                cmbPaciente.SelectedValue = cita.IdPaciente;
                txtMotivo.Text = cita.Motivo;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (dtpFechaCita.Text == "" || cmbPaciente.Text == "" || cmbMedico.Text == "")
            {
                MessageBox.Show("Por favor, complete los campos");
                return;
            }
            var dto_cita = new Datos.dto_cita
            {
                FechaCita = dtpFechaCita.Value,
                IdPaciente = (int)cmbPaciente.SelectedValue,
                IdMedico = (int)cmbMedico.SelectedValue,
                Motivo = txtMotivo.Text,
            };
            var cls_cita = new cls_cita();
            try
            {
                if (!this.modoEdicion)
                {
                    var valorinsertar = cls_cita.insertarCita(dto_cita);
                    if (valorinsertar == "ok")
                    {
                        MessageBox.Show("Se guardó su cita con éxito");
                        this.Close();
                    }
                }
                else
                {
                    dto_cita.IdCita = this.id;
                    if (cls_cita.actualizarCita(dto_cita) == "ok")
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

    }
}
