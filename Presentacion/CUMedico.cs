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
    public partial class CUMedico : UserControl
    {
        public CUMedico()
        {
            InitializeComponent();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Medico.FrmMedico frmMedico = new Medico.FrmMedico("n");
            frmMedico.Text = "Formulario de Médico";
            frmMedico.ShowDialog();
        }

        public void cargaGrilla(int numero)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            var logicaMedico = new cls_medico();
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
                dataGridView1.DataSource = logicaMedico.listarMedicos();
            }
            else
            {
                dataGridView1.DataSource = logicaMedico.buscarMedico(txtBuscar.Text.Trim());
            }
            dataGridView1.Columns["Nombre"].HeaderText = "Nombre";
            dataGridView1.Columns["Telefono"].HeaderText = "Contacto";
            dataGridView1.Columns["Direccion"].HeaderText = "Dirreción";
            dataGridView1.Columns["Especialidad"].HeaderText = "Especialidad";
            dataGridView1.Columns["Email"].HeaderText = "Email";
            dataGridView1.Columns["IdMedico"].Visible = false;

            dataGridView1.Columns.Add(btnEditar);
            dataGridView1.Columns.Add(btnEliminar);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.cargaGrilla(2);
        }

        private void CUMedico_Load(object sender, EventArgs e)
        {
            this.cargaGrilla(1);
        }
        private void eliminarMedico(int id)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar a este Médico?",
                                                     "Eliminar Médico", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                var logicaMedico = new cls_medico();
                if (logicaMedico.eliminarMedico(id))
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                var filaSeleccionada = dataGridView1.Rows[e.RowIndex];
                var idMedico = filaSeleccionada.Cells["IdMedico"].Value;

                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Editar")
                {
                    editarMedico((int)idMedico);
                }
                else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Eliminar")
                {
                    eliminarMedico((int)idMedico);
                }
            }
        }
        private void editarMedico(int id)
        {
            Medico.FrmMedico frmMedico = new Medico.FrmMedico(id.ToString());
            frmMedico.ShowDialog();
            this.cargaGrilla(1);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.cargaGrilla(1);
        }
    }
}
