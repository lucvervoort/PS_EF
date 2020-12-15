using BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Model
{

    public class Klant: Observable
    {
        #region Properties
        public long KlantId { get; set; } // PK
        public string Naam { get; private set; }
        public string Adres { get; private set; }

        private List<Bestelling> _bestellingen = new List<Bestelling>(); // FK
        #endregion

        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="naam"></param>
        /// <param name="adres"></param>
        public Klant(string naam, string adres)
        {
            ZetNaam(naam);
            ZetAdres(adres);
        }

        public Klant(long id, string naam, string adres)
        {
            KlantId = id;
            Naam = naam;
            Adres = adres;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="klantId"></param>
        /// <param name="naam"></param>
        /// <param name="adres"></param>
        /// <param name="bestellingen"></param>
        public Klant(int klantId, string naam, string adres, List<Bestelling> bestellingen) : this(klantId,naam, adres)
        {
            if (bestellingen == null) throw new KlantException("Klant - bestellingen null");
            _bestellingen = bestellingen;
            foreach (Bestelling b in bestellingen) b.ZetKlant(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="klantId"></param>
        /// <param name="naam"></param>
        /// <param name="adres"></param>
        public Klant(int klantId, string naam, string adres) : this(naam,adres)
        {
            KlantId = klantId;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="naam"></param>
        public void ZetNaam(string naam)
        {
            if (naam.Trim().Length < 1) throw new KlantException("Klant naam invalid");
            Naam = naam;
            NotifyPropertyChanged("Naam"); // string moet exact overeenkomen met property Naam
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adres"></param>
        public void ZetAdres(string adres)
        {
            if (adres.Trim().Length < 5) throw new KlantException("Klant adres invalid");
            Adres = adres;
            NotifyPropertyChanged("Adres"); // Gebruik juiste Property naam!!
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<Bestelling> GetBestellingen()
        {
            return _bestellingen.AsReadOnly();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bestelling"></param>
        public void VerwijderBestelling(Bestelling bestelling)
        {
            if (!_bestellingen.Contains(bestelling))
            {
                throw new KlantException("Klant : RemoveBestelling - bestelling does not exists");
            }
            else
            {
                if (bestelling.Klant==this)
                    bestelling.VerwijderKlant();
                _bestellingen.Remove(bestelling);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bestelling"></param>
        public void VoegToeBestelling(Bestelling bestelling)
        {
            if (_bestellingen.Contains(bestelling))
            {
                throw new KlantException("Klant : AddBestelling - bestelling already exists");
            }
            else
            {
                _bestellingen.Add(bestelling);
                if (bestelling.Klant!=this)
                    bestelling.ZetKlant(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bestelling"></param>
        /// <returns></returns>
        public bool HeeftBestelling(Bestelling bestelling)
        {
            if (_bestellingen.Contains(bestelling)) return true;
            else return false;
        }    

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Korting() //procent
        {
            if (_bestellingen.Count < 5) return 0;
            if (_bestellingen.Count < 10) return 10;
            else return 20;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[Klant] {KlantId},{Naam},{Adres},{_bestellingen.Count}";
        }

        /// <summary>
        /// 
        /// </summary>
        public void Show()
        {
            Console.WriteLine(this);
            foreach (Bestelling b in _bestellingen) Console.WriteLine($"    bestelling:{b}");
        }
        #endregion
    }
}
