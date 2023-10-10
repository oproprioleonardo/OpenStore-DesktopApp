using OpenStore.Services;

namespace OpenStore
{
    public class AppModule
    {
        private static readonly ProductService _productService;
        private static readonly CupomService _cupomService;

        static AppModule()
        {
            _productService = new ProductService("http://localhost:5000/v1/product");
            _cupomService = new CupomService("http://localhost:5000/v1/cupom");
        }

        public static ProductService GetProductService()
        {
            return _productService;
        }

        public static CupomService GetCupomService()
        {
            return _cupomService;
        }
    }
}
