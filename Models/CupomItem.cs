namespace OpenStore.Models
{
    public class CupomItem
    {

        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public float Quantity { get; set; }

        public CupomItem()
        {
            Code = "";
            Description = "";
        }

        public CupomItem(string code, string description, decimal price, float quantity)
        {
            Code = code;
            Description = description;
            Price = price;
            Quantity = quantity;
        }

        public static CupomItem NewCupomItem(Product product, float quantity)
        {
            return new CupomItem(product.Code, product.Description, product.RetailPrice, quantity);
        }

        public static CupomItem NewCupomItem(string code, string description, decimal price, float quantity)
        {
            return new CupomItem(code, description, price, quantity);
        }

        public decimal GetTotal()
        {
            return Price * (decimal)Quantity;
        }


    }
}
