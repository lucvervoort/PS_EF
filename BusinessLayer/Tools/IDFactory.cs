using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Tools
{
    public class IDFactory
    {
        private int _klantID;
        private int _bestellingID;
        private int _productID;

        public IDFactory(int klantID, int bestellingID, int productID)
        {
            _klantID = klantID;
            _bestellingID = bestellingID;
            _productID = productID;
        }

        public IDFactory()
        {
            _klantID = 0;
            _bestellingID = 0;
            _productID = 0;
        }

        public int MaakKlantID()
        {
            return ++_klantID;
        }
        public int MaakProductID()
        {
            return ++_productID;
        }
        public int MaakBestellingID()
        {
            return ++_bestellingID;
        }
    }
}
