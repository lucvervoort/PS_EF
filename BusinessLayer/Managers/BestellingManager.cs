using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Managers
{
    public class BestellingManager: IManager<Bestelling>
    {
        #region Properties
        private Dictionary<long, Bestelling> _bestellingen = new Dictionary<long, Bestelling>();
        #endregion

        #region Methods
        /// <summary>
        /// Geef alle bestellingen terug
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<Bestelling> HaalOp()
        {
            return new List<Bestelling>(_bestellingen.Values).AsReadOnly();
        }

        public IReadOnlyList<Bestelling> HaalOp(Func<Bestelling, bool> predicate)
        {
            var selection = _bestellingen.Values.Where<Bestelling>(predicate).ToList();
            return (IReadOnlyList<Bestelling>)selection;
        }

        /// <summary>
        /// Voeg een bestelling toe
        /// </summary>
        /// <param name="bestelling"></param>
        public void VoegToe(Bestelling bestelling)
        {
            if (_bestellingen.ContainsKey(bestelling.BestellingId))
            {
                _bestellingen[bestelling.BestellingId] = bestelling;
            }
            else
            {
                _bestellingen.Add(bestelling.BestellingId, bestelling);
            }
        }

        /// <summary>
        /// Verwijder een bestelling
        /// </summary>
        /// <param name="bestelling"></param>
        public void Verwijder(Bestelling bestelling)
        {
            if (!_bestellingen.ContainsKey(bestelling.BestellingId))
            {
                throw new BestellingManagerException("VerwijderBestelling");
            }
            else
            {
                _bestellingen.Remove(bestelling.BestellingId);
            }
        }

        /// <summary>
        /// Geef een bestelling terug op basis van de id van de bestelling
        /// </summary>
        /// <param name="bestellingId"></param>
        /// <returns></returns>
        public Bestelling HaalOp(long bestellingId)
        {
            if (!_bestellingen.ContainsKey(bestellingId))
            {
                throw new BestellingManagerException("GeefBestelling");
            }
            else
            {
                return _bestellingen[bestellingId];
            }
        }
        #endregion
    }
}
