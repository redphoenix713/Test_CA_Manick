using System;

namespace AnagramChecker
{
    public interface IProduct
    {
        Guid ProductId { get; set; }    
        ProductType Type { get; set; }                                           
    }

    public interface IProductAbstractFactory
    {
        IProduct GetProduct();
        IProduct GetProduct(Guid productId);
    }

    public class PhysicalProductFactory : IProductAbstractFactory
    {
        public IProduct GetProduct()
        {
            return new PhysicalProduct() {ProductId = new Guid(), ShippingAddress = "", Type = ProductType.PhysicalProduct};
        }

        public IProduct GetProduct(Guid productId)
        {
            return new PhysicalProduct() {ProductId = productId};
        }        
    }

    public class BookFactory : IProductAbstractFactory, IBook
    {
        public string ShippingAddress { get; set; }
        public IProduct GetProduct()
        {
            return new Book() { ProductId = new Guid(), Type = ProductType.Book };
        }

        public IProduct GetProduct(Guid productId)
        {
            return new Book() { ProductId = productId };
        }

        public void CreateDuplicatePackingSlip(string shippingAddress)
        {
            ShippingAddress = shippingAddress;
        }
    }    

    public interface IShippingSlip
    {
        string ShippingAddress { get; set; }
        void GenerateShippingSlip(string shippingAddress);
    }
    

    public interface IBook
    {
         string ShippingAddress { get; set; }
        void CreateDuplicatePackingSlip(string shippingAddress);
    }

    public class PhysicalProduct : IProduct, IShippingSlip
    {
        private Guid _productId;

        public Guid ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        public ProductType Type { get {return ProductType.PhysicalProduct;}  set {} }
        
        public string ShippingAddress { get; set; }
        public Guid GetProductId()
        {
            return Guid.NewGuid();
        }

        public void GenerateShippingSlip(string shippingAddress)
        {
            ShippingAddress = shippingAddress;
        }
    }

    public class Book : IProduct
    {
        private Guid _productId;

        public Guid ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        public ProductType Type { get { return ProductType.Book; } set { } }        
    }
}