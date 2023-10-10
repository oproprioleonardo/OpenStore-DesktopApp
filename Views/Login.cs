using OpenStore.Services;
using OpenStore.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenStore
{
    public partial class Login : Form
    {

        string usuario = "admin";
        string senha = "admin";


        public Login()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (this.txtUser.Text != usuario)
            {
                MessageBox.Show("Usuário inválido");
                return;
            }

            if (this.txtSenha.Text != senha)
            {
                MessageBox.Show("Senha inválida");
                return;
            }

            
            this.Visible = false;
            Views.MainMenu mm = new Views.MainMenu();
            mm.Show();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
