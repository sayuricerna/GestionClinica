using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiónDeClinica.Presentacion
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void btnCita_Click(object sender, EventArgs e)
        {
            CUCita frmCita = new CUCita();
            PanelGeneral.Controls.Clear();
            frmCita.Dock = DockStyle.Fill;
            PanelGeneral.Controls.Add(frmCita);
        }

        private void btnPaciente_Click(object sender, EventArgs e)
        {
            CUPaciente frmPaciente = new CUPaciente();
            PanelGeneral.Controls.Clear();
            frmPaciente.Dock = DockStyle.Fill;
            PanelGeneral.Controls.Add(frmPaciente);
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            CUMedico frmMedico = new CUMedico();
            PanelGeneral.Controls.Clear();
            frmMedico.Dock = DockStyle.Fill;
            PanelGeneral.Controls.Add(frmMedico);
        }
    }
}
