using OpenStore.Models;
using System;
using System.Windows.Forms;

namespace OpenStore.Views
{
    public partial class EditProduct : Form
    {
        private readonly Product product;

        public EditProduct(Product product)
        {
            this.product = product;
            InitializeComponent();
        }

        private void EditProduct_Load(object sender, System.EventArgs e)
        {
            this.txtCode.Text = product.Code;
            this.txtInternCode.Text = product.InternCode;
            this.txtDescription.Text = product.Description;
            this.txtStock.Text = product.Stock.ToString();
            this.txtCusto.Text = product.CostPrice.ToString();
            this.txtRetailPrice.Text = product.RetailPrice.ToString();
            this.txtWholesalePrice.Text = product.WholesalePrice.ToString();
            this.txtWholesaleAmount.Text = product.WholesaleQuantity.ToString();
            this.optUnit.SelectedItem = product.Unit.ToString();

        }

        private void BtnSalvar_Click(object sender, System.EventArgs e)
        {
            if (double.TryParse(txtStock.Text, out double stock) &&
               decimal.TryParse(txtCusto.Text, out decimal cost) &&
               decimal.TryParse(txtRetailPrice.Text, out decimal retailPrice) &&
               decimal.TryParse(txtWholesalePrice.Text, out decimal wholesalePrice) &&
               double.TryParse(txtWholesaleAmount.Text, out double wholesaleAmount) &&
               Enum.TryParse(optUnit.Text, true, out ProductUnit unit))
            {

                product.UpdateProduct(
                    this.txtInternCode.Text,
                    this.txtDescription.Text,
                    unit,
                    stock,
                    cost,
                    retailPrice,
                    wholesalePrice,
                    wholesaleAmount
                    );

                AppModule.GetProductService().UpdateProduct(product);
                this.DialogResult = DialogResult.OK;
                this.Close();
                MessageBox.Show("Produto atualizado com sucesso!");
            }
            else
            {
                MessageBox.Show("Verifique os valores inseridos");
            }
        }

        private void EditProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
