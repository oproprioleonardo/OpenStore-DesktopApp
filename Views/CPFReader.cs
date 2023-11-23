using System;
using System.Windows.Forms;

namespace OpenStore.Views
{
    public partial class CPFReader : Form
    {

        public string CPF { get; set; }

        public CPFReader()
        {
            InitializeComponent();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            CPF = txtCPF.Text;
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void CPFReader_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                DialogResult = DialogResult.Cancel;
        }
    }
}
