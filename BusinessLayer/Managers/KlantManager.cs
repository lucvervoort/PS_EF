using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using System.Collections.Generic;

namespace BusinessLayer.Managers
{
    public class KlantManager : IManager<Klant>
    {
        #region Properties
        private Dictionary<long, Klant> _klanten = new Dictionary<long, Klant>();
        #endregion

        #region Methods
        public IReadOnlyList<Klant> HaalOp()
        {
            return new List<Klant>(_klanten.Values).AsReadOnly();
        }

        public void VoegToe(Klant klant)
        {
            if (_klanten.ContainsKey(klant.KlantId))
            {
                throw new KlantManagerException("VoegKlantToe");
            }
            else
            {
                _klanten.Add(klant.KlantId, klant);
            }
        }

        public void Verwijder(Klant klant)
        {
            if (!_klanten.ContainsKey(klant.KlantId))
            {
                throw new KlantManagerException("VerwijderKlant");
            }
            else
            {
                _klanten.Remove(klant.KlantId);
            }
        }

        public Klant HaalOp(long klantId)
        {            
            if (!_klanten.ContainsKey(klantId))
            {
                throw new KlantManagerException("GeefKlant");
            }
            else
            {
                return _klanten[klantId];
            }
        }
        #endregion
    }
}