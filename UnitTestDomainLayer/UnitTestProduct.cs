using BusinessLayer.Exceptions;
using BusinessLayer.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestBusinessLayer
{
    [TestClass]
    public class UnitTestProduct
    {
        [TestMethod]
        public void Test_CTR_Naam()
        {
            string naam = "Westmalle";
            Product p = new Product(naam);

            Assert.AreEqual(naam, p.Naam);
            Assert.AreEqual(default(int), p.ProductId);
            Assert.AreEqual(default(double), p.Prijs);
        }
        [TestMethod]
        public void Test_CTR_NaamPrijs()
        {
            string naam = "Westmalle";
            double prijs = 4.2;
            Product p = new Product(naam,prijs);

            Assert.AreEqual(naam, p.Naam);
            Assert.AreEqual(default(int), p.ProductId);
            Assert.AreEqual(prijs, p.Prijs);
        }
        [TestMethod]
        public void Test_CTR_NaamPrijsId()
        {
            string naam = "Westmalle";
            double prijs = 4.2;
            int id = 100;
            Product p = new Product(id,naam,prijs);

            Assert.AreEqual(naam, p.Naam);
            Assert.AreEqual(id, p.ProductId);
            Assert.AreEqual(prijs, p.Prijs);
        }
        [TestMethod]
        public void Test_ZetNaam_Valid()
        {
            string naam = "Westmalle";
            string naamNew = "Westmalle Tripel";
            Product p = new Product(naam);

            Assert.AreEqual(naam, p.Naam);
            Assert.AreEqual(default(int), p.ProductId);
            Assert.AreEqual(default(double), p.Prijs);

            p.ZetNaam(naamNew);

            Assert.AreEqual(naamNew, p.Naam);
            Assert.AreEqual(default(int), p.ProductId);
            Assert.AreEqual(default(double), p.Prijs);
        }
        [TestMethod]
        public void Test_ZetPrijs_Valid()
        {
            string naam = "Westmalle";
            double prijs = 4.2;
            double prijsNew = 3.9;
            Product p = new Product(naam, prijs);

            Assert.AreEqual(naam, p.Naam);
            Assert.AreEqual(default(int), p.ProductId);
            Assert.AreEqual(prijs, p.Prijs);

            p.ZetPrijs(prijsNew);

            Assert.AreEqual(naam, p.Naam);
            Assert.AreEqual(default(int), p.ProductId);
            Assert.AreEqual(prijsNew, p.Prijs);
        }
        [TestMethod]
        public void Test_ZetProductId_Valid()
        {
            string naam = "Westmalle";
            double prijs = 4.2;            
            int productId = 1;
            int productIdNew = 2;
            Product p = new Product(productId,naam, prijs);

            Assert.AreEqual(naam, p.Naam);
            Assert.AreEqual(productId, p.ProductId);
            Assert.AreEqual(prijs, p.Prijs);

            p.ZetProductId(productIdNew);

            Assert.AreEqual(naam, p.Naam);
            Assert.AreEqual(productIdNew, p.ProductId);
            Assert.AreEqual(prijs, p.Prijs);
        }
        [TestMethod]
        public void Test_ZetNaam_ThrowProductException()
        {
            string naam = "Westmalle";
            string naamNew = "";
            Product p = new Product(naam);

            Assert.ThrowsException<ProductException>(()=>p.ZetNaam(naamNew));            
        }
        [TestMethod]
        public void Test_ZetPrijs_ThrowProductException()
        {
            string naam = "Westmalle";
            double prijs = 4.2;
            double prijsNew = 0;
            Product p = new Product(naam, prijs);

            Assert.ThrowsException<ProductException>(() => p.ZetPrijs(prijsNew));
        }
        [TestMethod]
        public void Test_ZetProductId_ThrowProductException()
        {
            string naam = "Westmalle";
            double prijs = 4.2;
            int productId = 1;
            int productIdNew = 0;
            Product p = new Product(productId,naam, prijs);

            Assert.ThrowsException<ProductException>(() => p.ZetProductId(productIdNew));
        }
        [TestMethod]
        public void Test_ctr_Naam_ThrowProductException()
        {
            string naam = "";
            Product p ;

            Assert.ThrowsException<ProductException>(() => p = new Product(naam));
        }
        [TestMethod]
        public void Test_ctr_Prijs_ThrowProductException()
        {
            string naam = "Westmalle";
            double prijs = -4.2;
  
            Product p;
            Assert.ThrowsException<ProductException>(() => p = new Product(naam,prijs));
            prijs = 0;
            Assert.ThrowsException<ProductException>(() => p = new Product(naam, prijs));
        }
        [TestMethod]
        public void Test_ctr_Id_ThrowProductException()
        {
            string naam = "Westmalle";
            double prijs = 4.2;
            int id = 0;

            Product p;
            Assert.ThrowsException<ProductException>(() => p = new Product(id,naam, prijs));
            id = -1;
            Assert.ThrowsException<ProductException>(() => p = new Product(id, naam, prijs));
        }
    }
}
