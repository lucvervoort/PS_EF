using BusinessLayer.Model;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace KlantBestellingen.WPF
{

    /// <summary>
    /// Interaction logic for Producten.xaml
    /// </summary>
    public partial class Producten : Window
    {
        // Interface INotifyPropertyChanged
        private ObservableCollection<Product> _producten = null;  
        
        public Producten()
        {
            InitializeComponent();
            _producten = new ObservableCollection<Product>(Context.ProductManager.HaalOp());
            dgProducten.ItemsSource = _producten;
            _producten.CollectionChanged += _producten_CollectionChanged;
        }

        /// <summary>
        /// Doorgeven aan business laag dat klant werd toegevoegd of verwijderd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _producten_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Product product in e.OldItems)
                {
                    Context.ProductManager.Verwijder(product);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Product product in e.NewItems)
                {
                    Context.ProductManager.VoegToe(product);
                }
            }
        }

        /// <summary>
        /// Kruip tussen wanneer de gebruiker met de delete toets een rij verwijdert uit een DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgProducten_PreviewDeleteCommandHandler(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (!(MessageBox.Show("Zeker dat je het product wenst te verwijderen?", "Bevestig.", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
            {
                // Cancel Delete.
                e.Handled = true;
            }
        }

        private void BtnNieuwProduct_Click(object sender, RoutedEventArgs e)
        {
            // Preconditie
            if (string.IsNullOrEmpty(TbProductNaam?.Text) || string.IsNullOrEmpty(TbProductPrijs?.Text))
            {
                MessageBox.Show("Geef alle klantgegevens op!");
                return;
            }

            var klant = new Product(TbProductNaam.Text, Double.Parse(TbProductPrijs.Text));
            // Omdat we een ObservableCollection<Klant> gebruiken, wordt onze wijziging meteen doorgegeven naar de gui (.Items wijzigen zou threading problemen geven):
            // Omdat we ObservableCollection<Klant> gebruiken en er een event gekoppeld is aan delete/add hiervan, wordt ook de business layer aangepast!
            _producten.Add(klant);
        }

        private void Tb_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(TbProductNaam.Text) && !string.IsNullOrEmpty(TbProductPrijs.Text))
            {
                BtnNieuwProduct.IsEnabled = true;
            }
            else
            {
                BtnNieuwProduct.IsEnabled = false;
            }
        }
    }
}
