using BusinessLayer.Exceptions;
using System;

namespace BusinessLayer.Model
{
    public class Product: Observable
    {
        #region Properties
        public long ProductId { get; set; } // PK
        public string Naam { get; private set; }
        public double Prijs { get; private set; }
        #endregion

        #region Ctor
        public Product(string naam) => ZetNaam(naam);
        public Product(string naam, double prijs) : this(naam) => ZetPrijs(prijs);
        public Product(long productId, string naam, double prijs) : this(naam, prijs)
        {
            ZetProductId(productId);
        }
        #endregion

        #region Methods
        public void ZetPrijs(double prijs)
        {
            if (prijs <= 0) throw new ProductException("Product prijs invalid");
            Prijs = prijs;
        }

        public void ZetNaam(string naam)
        {
            if (naam.Trim().Length < 1) throw new ProductException("Product naam invalid");
            Naam = naam;
        }

        public void ZetProductId(long productId)
        {
            if (productId <= 0) throw new ProductException("ProductId invalid");
            ProductId = productId;
        }

        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   Naam == product.Naam;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Naam);
        }

        public override string ToString()
        {
            return $"[Product] {ProductId},{Naam},{Prijs}";
        }
        #endregion
    }
}
