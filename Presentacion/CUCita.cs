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
    public partial class CUCita : UserControl
    {
        public CUCita()
        {
            InitializeComponent();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Cita.FrmCita frmCita = new Cita.FrmCita("n");
            frmCita.Text = "Formulario de Cital";
            frmCita.ShowDialog();
        }
        //Grilla de CU
        public void cargaGrilla(int numero)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            var logicaCitas = new cls_cita();
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
                // Mostrar todas las citas
                dataGridView1.DataSource = logicaCitas.listarCitas();
            }
            else
            {
                // Buscar citas por criterio
                dataGridView1.DataSource = logicaCitas.buscarCita(txtBuscar.Text.Trim());
            }

            dataGridView1.Columns["FechaCita"].HeaderText = "Fecha de Cita";
            dataGridView1.Columns["IdPaciente"].HeaderText = "Paciente";
            dataGridView1.Columns["IdMedico"].HeaderText = "Médico";
            dataGridView1.Columns["Motivo"].HeaderText = "Motivo";
            dataGridView1.Columns["FechaCreacion"].HeaderText = "Fecha Agendada";
            dataGridView1.Columns["IdCita"].Visible = false;

            dataGridView1.Columns.Add(btnEditar);
            dataGridView1.Columns.Add(btnEliminar);

        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.cargaGrilla(2);
        }

        private void CUCita_Load(object sender, EventArgs e)
        {
            this.cargaGrilla(1);

        }

        private void eliminarCita(int id)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar esta cita?",
                                                     "Eliminar Cita", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                var logicaCitas = new cls_cita();
                if (logicaCitas.eliminarCita(id))
                {
                    MessageBox.Show("La cita se eliminó correctamente.");
                    this.cargaGrilla(1);
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al eliminar la cita.");
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                var filaSeleccionada = dataGridView1.Rows[e.RowIndex];
                var idCita = filaSeleccionada.Cells["IdCita"].Value;

                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Editar")
                {
                    editarCita((int)idCita);
                }
                else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Eliminar")
                {
                    eliminarCita((int)idCita);
                }
            }
        }

        private void editarCita(int id)
        {
            Cita.FrmCita frmCitas = new Cita.FrmCita(id.ToString());
            frmCitas.ShowDialog();
            this.cargaGrilla(1);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
            }

        }
        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            this.cargaGrilla(1);
        }
    }
}
