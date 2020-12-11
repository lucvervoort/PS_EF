using BusinessLayer.Exceptions;
using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Tools
{
    public static class BestellingFactory
    {
        public static Bestelling MaakBestelling(IDFactory idFactory)
        {
            try
            {
                return new Bestelling(idFactory.MaakBestellingID(), DateTime.Now);
            }
            catch (BestellingException ex)
            {
                throw new BestellingFactoryException("MaakBestelling", ex);
            }
        }
        public static Bestelling MaakBestelling(Klant klant,IDFactory idFactory)
        {
            try
            {
                return new Bestelling(idFactory.MaakBestellingID(),klant, DateTime.Now);
            }
            catch (BestellingException ex)
            {
                throw new BestellingFactoryException("MaakBestelling", ex);
            }
        }
        public static Bestelling MaakBestelling(Klant klant, Dictionary<Product, int> producten, IDFactory idFactory)
        {
            try
            {
                return new Bestelling(idFactory.MaakBestellingID(), klant, DateTime.Now,producten);
            }
            catch (BestellingException ex)
            {
                throw new BestellingFactoryException("MaakBestelling", ex);
            }
        }
    }
}
