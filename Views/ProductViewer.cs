using OpenStore.Models;
using OpenStore.Views;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OpenStore
{
    public partial class ProductViewer : Form
    {

        private readonly Form mainMenu;
        private List<Product> produtos;


        public ProductViewer(Form mainMenu)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
        }

        private void ProductViewer_Load(object sender, EventArgs e)
        {
            produtosGrid.Columns[0].ReadOnly = true;
            UpdateProductsGrid();
        }

        private void ProdutosGrid_SelectionChanged(object sender, EventArgs e)
        {
            produtosGrid.ClearSelection();
        }

        private async void UpdateProductsGrid()
        {
            produtos = await AppModule.GetProductService().GetAllProducts();

            produtosGrid.Rows.Clear();
  
            foreach (Product prod in produtos)
            {
                this.produtosGrid.Rows.Add(prod.Code, prod.InternCode, prod.Description, prod.Unit.ToString(), prod.RetailPrice, prod.Stock);
            }

            produtosGrid.ClearSelection();
        }

        private async void ProdutosGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.produtosGrid.Columns[e.ColumnIndex].HeaderText.Equals("Editar"))
            {
                string code = this.produtosGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                Product product = await AppModule.GetProductService().GetProductByCode(code);
                DialogResult rs = new EditProduct(product).ShowDialog();
                
                if (rs == DialogResult.OK)
                {
                    UpdateProductsGrid();
                }
            }
        }

        private void ProductViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.mainMenu.Visible = true;
        }

        private void produtosGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.produtosGrid.Columns[e.ColumnIndex].HeaderText.Equals("Deletar"))
            {
                Product produto = produtos[e.RowIndex];
                AppModule.GetProductService().DeleteProductById(produto.Id);
                produtosGrid.Rows.RemoveAt(e.RowIndex);
                MessageBox.Show("Produto removido com sucesso!");
            }
        }

        private void BtnInserir_Click(object sender, EventArgs e)
        {
            DialogResult rs = new InsertProduct().ShowDialog();
            if (rs == DialogResult.OK)
            {
                UpdateProductsGrid();
            }
        }
    }
}
