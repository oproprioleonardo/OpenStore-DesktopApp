using OpenStore.Models;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OpenStore.Services
{
    public class PrintService
    {

        private readonly string _path;

        public PrintService(string path)
        {
            _path = path;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        public void Print(Cupom cupom)
        {
            Document document = new Document();

            Page page = document.Pages.Add();
            
            page.PageInfo.Margin.Top = 2;
            page.PageInfo.Margin.Bottom = 2;
            page.PageInfo.Margin.Left = 2;
            page.PageInfo.Margin.Right = 2;
            page.PageInfo.Width = 204.22; // Aproximadamente 72 mm em pontos

            TextFragment textFragment = new TextFragment("OpenStore\n\n");
            textFragment.TextState.FontSize = 14;
            textFragment.TextState.FontStyle = FontStyles.Bold;
            textFragment.HorizontalAlignment = HorizontalAlignment.Center;
            page.Paragraphs.Add(textFragment);

            textFragment = new TextFragment($"CNPJ: 00.000.000/0000-00");
            textFragment.TextState.FontSize = 6;
            page.Paragraphs.Add(textFragment);

            textFragment = new TextFragment($"Endereco: Rua dos Bobos, 0, Casqueiro. Cubatao-SP\n");
            textFragment.TextState.FontSize = 6;
            page.Paragraphs.Add(textFragment);


            textFragment = new TextFragment($"Cupom: #{cupom.Id}");
            textFragment.TextState.FontSize = 6;
            page.Paragraphs.Add(textFragment);
            textFragment = new TextFragment($"Cliente: {cupom.Cliente}");
            textFragment.TextState.FontSize = 6;
            page.Paragraphs.Add(textFragment);
            textFragment = new TextFragment($"Data: {cupom.Date.ToShortDateString()}\n\n");
            textFragment.TextState.FontSize = 6;
            page.Paragraphs.Add(textFragment);

            Table table = new Table();
            table.Alignment = HorizontalAlignment.Center;
            table.DefaultCellTextState.FontSize = 4;
            table.DefaultCellBorder = new BorderInfo(BorderSide.Right, .2f, Color.Black);
            table.DefaultCellPadding = new MarginInfo(0, 5, 0, 0);
            table.DefaultCellTextState.HorizontalAlignment = HorizontalAlignment.Center;
            table.ColumnWidths = "40 70 30 30 35";
            page.Paragraphs.Add(table);

            Row row = table.Rows.Add();
            row.Cells.Add("Cod");
            row.Cells.Add("Desc");
            row.Cells.Add("Preco");
            row.Cells.Add("Qtd");
            row.Cells.Add("Total");

            cupom.Items.ForEach(item => AdicionarProdutoNaTabela(table, item));

            textFragment = new TextFragment($"\n\nTotal: R$ {cupom.GetTotal():N2}\n\n");
            textFragment.TextState.FontSize = 5;
            textFragment.TextState.FontStyle = FontStyles.Bold;
            page.Paragraphs.Add(textFragment);

            long milissegundos = cupom.Date.Ticks / TimeSpan.TicksPerMillisecond;
            page.PageInfo.Height = page.PageInfo.PureHeight + 10;
            new Task(() => document.Save($"{_path}cupom{milissegundos}_{cupom.Id}.pdf")).Start();
        }

        private void AdicionarProdutoNaTabela(Table table, CupomItem item)
        {
            Row row = table.Rows.Add();
            row.Cells.Add(item.Code);
            row.Cells.Add(item.Description);
            row.Cells.Add(item.Price.ToString("N2"));
            row.Cells.Add(item.Quantity.ToString("N3"));
            row.Cells.Add(item.GetTotal().ToString("N2"));
        }
    }

}
