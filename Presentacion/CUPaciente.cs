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

namespace GestiónDeClinica.Presentacion
{
    public partial class CUPaciente : UserControl
    {
        public CUPaciente()
        {
            InitializeComponent();
        }
        public void cargaGrilla(int numero)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            var logicaPaciente = new cls_paciente();
            var autoincremento = new DataGridViewTextBoxColumn
            {
                HeaderText = "N.-",
                ReadOnly = true
            };
            dataGridView1.Columns.Add(autoincremento);

            var btnEditar = new DataGridViewButtonColumn
            {
                HeaderText = "Editar",
                Text = "Editar",
                UseColumnTextForButtonValue = true
            };

            var btnEliminar = new DataGridViewButtonColumn
            {
                HeaderText = "Eliminar",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true
            };

            if (numero == 1)
            {
                dataGridView1.DataSource = logicaPaciente.listarPacientes();
            }
            else
            {
                dataGridView1.DataSource = logicaPaciente.buscarPaciente(txtBuscar.Text.Trim());
            }
            dataGridView1.Columns["Nombre"].HeaderText = "Nombre";
            dataGridView1.Columns["Email"].HeaderText = "Email";
            dataGridView1.Columns["Direccion"].HeaderText = "Dirección";
            dataGridView1.Columns["Telefono"].HeaderText = "Teléfono";
            dataGridView1.Columns["Genero"].HeaderText = "Género";
            dataGridView1.Columns["Cedula"].HeaderText = "Cédula";
            dataGridView1.Columns["FechaNac"].HeaderText = "Fecha de Nacimiento";
            dataGridView1.Columns["IdPaciente"].Visible = false;

            dataGridView1.Columns.Add(btnEditar);
            dataGridView1.Columns.Add(btnEliminar);
        }
        private void eliminarPaciente(int id)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar a este Paciente?",
                                                     "Eliminar Paciente", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                var logicaPaciente = new cls_paciente();
                if (logicaPaciente.eliminarPaciente(id))
                {
                    MessageBox.Show("El Registro se eliminó Correctamente");
                    this.cargaGrilla(1);
                }
                else
                {
                    MessageBox.Show("Error al Eliminar el registro");
                }
            }
        }
        private void editarPaciente(int id)
        {
            Paciente.FrmPaciente frmPaciente = new Paciente.FrmPaciente(id.ToString());
            frmPaciente.ShowDialog();
            this.cargaGrilla(1);
        }
        private void CUPaciente_Load(object sender, EventArgs e)
        {
            this.cargaGrilla(1);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Paciente.FrmPaciente frmPaciente = new Paciente.FrmPaciente("n");
            frmPaciente.Text = "Formulario de Paciente";
            frmPaciente.ShowDialog();
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.cargaGrilla(1);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                var filaSeleccionada = dataGridView1.Rows[e.RowIndex];
                var idPaciente = filaSeleccionada.Cells["IdPaciente"].Value;

                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Editar")
                {
                    editarPaciente((int)idPaciente);
                }
                else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Eliminar")
                {
                    eliminarPaciente((int)idPaciente);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.cargaGrilla(2);
        }
    }
}
