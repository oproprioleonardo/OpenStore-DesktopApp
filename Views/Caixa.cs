using OpenStore.Models;
using OpenStore.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace OpenStore
{
    public partial class Caixa : Form
    {

        private readonly Cupom cupom;
        private readonly List<CupomItem> cupomItems = new List<CupomItem>();
        private readonly ProductService productService;
        private readonly CupomService cupomService;
        private readonly Form mainForm;


        public Caixa(Form mainForm, string cpf)
        {
            InitializeComponent();
            productService = AppModule.GetProductService();
            cupomService = AppModule.GetCupomService();
            cupom = Cupom.NewCupom(DateTime.Now, cpf ?? "Cliente Comum", cupomItems);
            this.mainForm = mainForm;
        }

        private async void BtnAddProduct_Click(object sender, EventArgs e)
        {
            string terms = this.TxtCode.Text;
            float amount = float.Parse(this.TxtAmount.Text);
            this.TxtCode.Text = "";
            if (cupomItems.Where(ci => ci.Code.Equals(terms) || ci.Description.ToLower().Contains(terms.ToLower())).Count() > 0)
            {
                CupomItem cupomItem = cupomItems.Where(ci => ci.Code.Equals(terms) || ci.Description.ToLower().Contains(terms.ToLower())).First();

                cupomItem.Quantity += amount;
                this.itemsGrid.Rows[cupomItems.IndexOf(cupomItem)].Cells[2].Value = cupomItem.Quantity;
                this.itemsGrid.Rows[cupomItems.IndexOf(cupomItem)].Cells[3].Value = cupomItem.GetTotal();
            }
            else
            {
                try
                {
                    Product product = await productService.SearchProduct(terms);
                    if (cupomItems.Where(ci => ci.Code.Equals(product.Code)).Count() > 0)
                    {
                        CupomItem cupomItem = cupomItems.Where(ci => ci.Code.Equals(terms)).First();
                        cupomItem.Quantity += amount;
                        this.itemsGrid.Rows[cupomItems.IndexOf(cupomItem)].Cells[2].Value = cupomItem.Quantity;
                        this.itemsGrid.Rows[cupomItems.IndexOf(cupomItem)].Cells[3].Value = cupomItem.GetTotal();
                    } else
                    {
                        CupomItem ci = CupomItem.NewCupomItem(product, amount);
                        cupomItems.Add(ci);
                        this.itemsGrid.Rows.Add(ci.Code, ci.Description, ci.Quantity, ci.GetTotal());
                    }

                    

                }
                catch (Exception) { }
            }

            this.TxtTotal.Text = "R$ " + cupom.GetTotal().ToString("N2");
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            cupomService.SaveCupom(cupom);

            this.Close();
            this.mainForm.Visible = true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.mainForm.Visible = true;
        }

        private void Caixa_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.mainForm.Visible = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.itemsGrid.Columns[e.ColumnIndex].HeaderText.Equals("Apagar"))
            {
                string code = this.itemsGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                cupomItems.Remove(cupomItems.Where(ci => ci.Code.Equals(code)).First());
                this.itemsGrid.Rows.RemoveAt(e.RowIndex);
                this.TxtTotal.Text = "R$ " + cupom.GetTotal().ToString("N2");
            }
        }

        private void Caixa_Load(object sender, EventArgs e)
        {
            this.TxtCode.Select();
        }
    }
}
