using BusinessLayer.Interfaces;
using BusinessLayer.Managers;
using BusinessLayer.Model;
using BusinessLayer.Tools;
using System;

namespace ConsoleAppKlantBestellingen
{
    class Program
    {
        static void Main(string[] args)
        {
            IDFactory idF = new IDFactory(0,100,5000); //klant,bestelling,product
            var kM = new /*Db*/KlantManager() as IManager<Klant>;
            var pM = new ProductManager() as IManager<Product>;
            var bM = new BestellingManager() as IManager<Bestelling>;

            pM.VoegToe(ProductFactory.MaakProduct("product 1", 10.0, idF));
            pM.VoegToe(ProductFactory.MaakProduct("product 2", 12.0, idF));
            pM.VoegToe(ProductFactory.MaakProduct("product 3", 13.0, idF));
            foreach (var x in pM.HaalOp()) Console.WriteLine(x);

            kM.VoegToe(KlantFactory.MaakKlant("klant 1", "adres 1", idF));
            kM.VoegToe(KlantFactory.MaakKlant("klant 2", "adres 2", idF));
            foreach (var x in kM.HaalOp()) //Console.WriteLine(x);
                x.Show();

            bM.VoegToe(BestellingFactory.MaakBestelling(idF));
            bM.VoegToe(BestellingFactory.MaakBestelling(kM.HaalOp(1),idF));

            Bestelling b = bM.HaalOp(101);
            //b.VoegProductToe(pM.HaalOp("product 2"),8);
            //b.VoegProductToe(pM.HaalOp("product 1"), 7);
            Console.WriteLine($"Prijs:{b.Kostprijs()}, {b.PrijsBetaald}");
            b.ZetBetaald();
            Console.WriteLine($"Prijs:{b.Kostprijs()}, {b.PrijsBetaald}");

            foreach (var x in bM.HaalOp()) //Console.WriteLine(x);
                x.Show();
            foreach (var x in kM.HaalOp()) //Console.WriteLine(x);
                x.Show();
            Console.WriteLine("-----------------");
            Klant k1 = kM.HaalOp(1);
            k1.Show();
            k1.VoegToeBestelling(b);
            k1.Show();
        }
    }
}
