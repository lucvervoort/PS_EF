using BusinessLayer.Model;
using System.Collections.Generic;
using BusinessLayer.Exceptions;
using System.Linq;
using BusinessLayer.Interfaces;
using System;

namespace BusinessLayer.Managers
{
    public class ProductManager: IManager<Product>
    {
        private Dictionary<string, Product> _producten = new Dictionary<string, Product>();

        public IReadOnlyList<Product> HaalOp()
        {
            return new List<Product>(_producten.Values).AsReadOnly();
        }

        public IReadOnlyList<Product> HaalOp(Func<Product, bool> predicate)
        {
            var selection = _producten.Values.Where<Product>(predicate).ToList();
            return (IReadOnlyList<Product>)selection;
        }

        public void VoegToe(Product product)
        {
            if (_producten.ContainsKey(product.Naam))
            {
                _producten[product.Naam] = product;
            }
            else
            {
                _producten.Add(product.Naam, product);
            }
        }

        public void Verwijder(Product product)
        {
            if (!_producten.ContainsKey(product.Naam))
            {
                throw new ProductManagerException("VerwijderProduct");
            }
            else
            {
                _producten.Remove(product.Naam);
            }
        }

        public Product HaalOp(string naam)
        {
            if (!_producten.ContainsKey(naam))
            {
                throw new ProductManagerException("GeefProduct");
            }
            else
            {
                return _producten[naam];
            }
        }

        public Product HaalOp(long productId)
        {
            if (!_producten.Values.Any(x=>x.ProductId==productId))
            {
                throw new ProductManagerException("GeefProduct");
            }
            else
            {
                return _producten.Values.First(x => x.ProductId == productId);
            }
        }
    } 
}
