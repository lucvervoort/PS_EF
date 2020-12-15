using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using EntityFrameworkRepository;
using EntityFrameworkRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Managers
{
    /*
    public class CustomerKlant
    {
        public Customer Item1 {get;set;}
        public Klant Item2 {get; set;}
    }
     */
    public class DbKlantManager: IManager<Klant>
    {
        #region Properties
        private EntityFrameworkRepository<Customer> _repository = new EntityFrameworkRepository<Customer>();
        /// <summary>
        /// An application of type Tuple (2 elementen als een eenheid): "alternatief" voor een aparte class CustomerKlant
        /// </summary>
        private Dictionary<long, (Customer, Klant)> _mappedObjects = new Dictionary<long, (Customer, Klant)>(); // key: long Id
        // private Dictionary<long, CustomerKlant> _mappedObjects;
        #endregion

        #region Ctor
        #endregion

        #region Methods
        public IReadOnlyList<Klant> HaalOp()
        {
            var klantenLijst = new List<Klant>();
            // An EF object is tracked ONCE!!
            _mappedObjects = _repository.List().ToDictionary( /*key: Customer*/ c => c.Id, 
                /*value: Tuple van Customer object en Klant object*/ c => (c, new Klant(c.Id, c.Name, c.Address)));
            foreach(var dbItem in _mappedObjects.Values)
            {
                klantenLijst.Add(dbItem.Item2);
            }
            return klantenLijst;
        }

        public IReadOnlyList<Klant> HaalOp(Func<Klant, bool> predicate)
        {
            var kltn = new List<Klant>();          
            foreach(var item in _mappedObjects.Values)
            {
                kltn.Add(item.Item2);
            }
            var selection = kltn.Where<Klant>(predicate).ToList();
            return (IReadOnlyList<Klant>)selection;
        }

        public void VoegToe(Klant klant)
        {
            // We mogen geen Id opgeven want database kent deze toe:
            var customer = new Customer { /*Id = klant.KlantId,*/ Name = klant.Naam, Address = klant.Adres };
            klant.KlantId = customer.Id = _repository.Insert(customer); // Customer wordt g-insert-eerd in de database; wordt direct weggeschreven wegens SaveChanges()
            _mappedObjects[klant.KlantId] = (customer, klant);
        }

        public void Verwijder(Klant klant)
        {
            _repository.Delete(_mappedObjects[klant.KlantId].Item1);
            _mappedObjects.Remove(klant.KlantId);
        }

        public Klant HaalOp(long klantId)
        {
            if (_mappedObjects.ContainsKey(klantId))
            {
                return _mappedObjects[klantId].Item2; // we geven Klant object terug dat al klaarstond, via Item2 van Tuple
            }
            var customer = _repository.GetById(klantId);
            _mappedObjects[klantId] = (customer, new Klant(customer.Id, customer.Name, customer.Address));
            return _mappedObjects[klantId].Item2; // we geven Klant object terug via Item2 van Tuple
        }
    }
    #endregion
}
