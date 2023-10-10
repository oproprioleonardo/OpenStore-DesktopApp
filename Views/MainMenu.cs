using System;
using System.Windows.Forms;

namespace OpenStore.Views
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnVerProdutos_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            new ProductViewer(this).Show(this);
        }

        private void btnAbrirCupom_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            new Caixa(this).Show(this);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
