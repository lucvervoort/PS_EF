using BusinessLayer.Exceptions;
using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Tools
{
    public static class KlantFactory
    {
        public static Klant MaakKlant(string naam,string adres,IDFactory idFactory)
        {
            try
            {
                return new Klant(idFactory.MaakKlantID(),naam.Trim(),adres.Trim());
            }
            catch (KlantException ex)
            {
                throw new KlantFactoryException("MaakKlant", ex);
            }
        }
        public static Klant MaakKlant(string naam, string adres, List<Bestelling> bestellingen, IDFactory idFactory)
        {
            try
            {
                return new Klant(idFactory.MaakKlantID(), naam.Trim(), adres.Trim(),bestellingen);
            }
            catch (KlantException ex)
            {
                throw new KlantFactoryException("MaakKlant", ex);
            }
        }
    }
}
