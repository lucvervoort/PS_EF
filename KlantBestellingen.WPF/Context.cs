// HOGENT 
using BusinessLayer.Interfaces;
using BusinessLayer.Managers;
using BusinessLayer.Model;
using BusinessLayer.Tools;
using System;
// Timer:
//using System;
//using System.Windows.Threading;

namespace KlantBestellingen.WPF
{
    public static class Context
    {
        #region Properties
        public static IDFactory IdFactory { get; } = new IDFactory(0, 100, 5000);
        // DbKlantManager!
        public static IManager<Klant> KlantManager { get; } = new DbKlantManager(); // Experimenteer: kan ook nog altijd KlantManager zijn!
        public static IManager<Product> ProductManager { get; } = new ProductManager();
        public static IManager<Bestelling> BestellingManager { get; } = new BestellingManager();
        #endregion

        // private static DispatcherTimer _timer; // is operationeel volledig los van WPF en de andere code

        public static void Populate()
        {
            /*
            // Test code: moet weg indien db opgevuld
            // ----------
            //KlantManager.VoegToe(KlantFactory.MaakKlant("klant 1", "adres 1", IdFactory));
            //KlantManager.VoegToe(KlantFactory.MaakKlant("klant 2", "adres 2", IdFactory));
            DbProductManager dbProductMgr = new DbProductManager();
            //dbProductMgr.VoegToe(ProductFactory.MaakProduct("Product 1", 5.6, IdFactory));
            //dbProductMgr.VoegToe(ProductFactory.MaakProduct("Product 2", 6.7, IdFactory));

            var klanten = KlantManager.HaalOp();
            var producten = dbProductMgr.HaalOp();

            DbBestellingManager testDbOrderMgr = new DbBestellingManager();
            {
                var counter = 1;
                Bestelling bestelling = new Bestelling(0, DateTime.Now) { Klant = klanten[0] };
                foreach (var p in producten)
                {
                    bestelling.VoegProductToe(p, counter++);
                }
                testDbOrderMgr.VoegToe(bestelling);
            }
            */

            /*
            // Test code: we initialiseren een timer die elke 10 seconden het adres aanpast - alsof dit op de business layer gebeurt
            // ----------
            _timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(10) }; // timer loopt af om de 10 seconden
            _timer.Tick += _timer_Tick; // voer method uit wanner timer afloopt
            _timer.Start();
            */
        }

        /*
        private static int _counter = 0;
        private static void _timer_Tick(object sender, EventArgs e)
        {
            // We passen aan op de business layer, maar owv INotify... volgt WPF de aanpassingen
            foreach (var klant in KlantManager.GeefKlanten())
            {
                ++_counter;
                klant.ZetAdres(klant.Adres + _counter.ToString());
            }
        }
        */
    }
}