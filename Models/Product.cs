using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStore.Models
{

    [JsonObject]
    public class Product
    {
        private long _id;

        public Product()
        {
            Code = "";
            InternCode = "";
            Description = "";
            Unit = ProductUnit.INDETERMINADO;
        }

        [JsonProperty("id")]
        public long Id
        {
            get => _id;
            set => _id = value;
        }

        public Product(long id, string code, string internCode, string description, ProductUnit productUnit, double stock,
            decimal costPrice, decimal retailPrice, decimal wholesalePrice, double wholesaleQuantity, bool isActive)
        {
            Id = id;
            Code = code;
            InternCode = internCode;
            Description = description;
            Unit = productUnit;
            Stock = stock;
            CostPrice = costPrice;
            RetailPrice = retailPrice;
            WholesalePrice = wholesalePrice;
            WholesaleQuantity = wholesaleQuantity;
            IsActive = isActive;
        }

        public static Product NewProduct(string code, string? internCode, string description, ProductUnit productUnit,
            double stock, decimal costPrice, decimal retailPrice, decimal wholesalePrice, double wholesaleQuantity)
        {
            return new Product(
                0,
                code,
                internCode ?? "",
                description,
                productUnit,
                stock,
                costPrice,
                retailPrice,
                wholesalePrice,
                wholesaleQuantity,
                true
            );
        }

        public void UpdateProduct(
            string? internCode, string description, ProductUnit productUnit,
            double stock, decimal costPrice, decimal retailPrice, decimal wholesalePrice, double wholesaleQuantity)
        {
            InternCode = internCode ?? "";
            Description = description;
            Unit = productUnit;
            Stock = stock;
            CostPrice = costPrice;
            RetailPrice = retailPrice;
            WholesalePrice = wholesalePrice;
            WholesaleQuantity = wholesaleQuantity;
        }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("internCode")]
        public string InternCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("unit")]
        public ProductUnit Unit { get; set; }

        [JsonProperty("stock")]
        public double Stock { get; set; }

        [JsonProperty("costPrice")]
        public decimal CostPrice { get; set; }

        [JsonProperty("retailPrice")]
        public decimal RetailPrice { get; set; }

        [JsonProperty("wholesalePrice")]
        public decimal WholesalePrice { get; set; }

        [JsonProperty("wholesaleQuantity")]
        public double WholesaleQuantity { get; set; }

        [JsonIgnore]
        public bool Wholesale => WholesaleQuantity > 0;

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (Product)obj;
            return Code.Equals(other.Code);
        }

       
        public override string ToString()
        {
            return $"ID: {Id}\n" +
               $"Code: {Code}\n" +
               $"InternCode: {InternCode}\n" +
               $"Description: {Description}\n" +
               $"ProductUnit: {Unit}\n" +
               $"Stock: {Stock}\n" +
               $"CostPrice: {CostPrice}\n" +
               $"RetailPrice: {RetailPrice}\n" +
               $"WholesalePrice: {WholesalePrice}\n" +
               $"WholesaleQuantity: {WholesaleQuantity}\n" +
               $"Wholesale: {Wholesale}\n" +
               $"IsActive: {IsActive}";
        }

        public override int GetHashCode()
        {
            int hashCode = 1563887335;
            hashCode = hashCode * -1521134295 + _id.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Code);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(InternCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + Unit.GetHashCode();
            hashCode = hashCode * -1521134295 + Stock.GetHashCode();
            hashCode = hashCode * -1521134295 + CostPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + RetailPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + WholesalePrice.GetHashCode();
            hashCode = hashCode * -1521134295 + WholesaleQuantity.GetHashCode();
            hashCode = hashCode * -1521134295 + Wholesale.GetHashCode();
            hashCode = hashCode * -1521134295 + IsActive.GetHashCode();
            return hashCode;
        }
    }
}
