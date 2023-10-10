using OpenStore.Models;
using System;
using System.Windows.Forms;

namespace OpenStore.Views
{
    public partial class InsertProduct : Form
    {
        public InsertProduct()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnInserir_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtStock.Text, out double stock) &&
                decimal.TryParse(txtCusto.Text, out decimal cost) &&
                decimal.TryParse(txtRetailPrice.Text, out decimal retailPrice) &&
                decimal.TryParse(txtWholesalePrice.Text, out decimal wholesalePrice) &&
                double.TryParse(txtWholesaleAmount.Text, out double wholesaleAmount))
            {

                Product product = Product.NewProduct(
                txtCode.Text,
                txtInternCode.Text,
                txtDescription.Text,
                (ProductUnit)Enum.Parse(typeof(ProductUnit), optUnit.SelectedText),
                stock,
                cost,
                retailPrice,
                wholesalePrice,
                wholesaleAmount);

                AppModule.GetProductService().SaveProduct(product);
                this.DialogResult = DialogResult.OK;
                this.Close();
                MessageBox.Show("Produto inserido com sucesso!");
            }
            else
            {
                MessageBox.Show("Verifique os valores inseridos");
            }


        }

        private void tableLayoutPanel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void InsertProduct_Load(object sender, EventArgs e)
        {

        }

        private void InsertProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
