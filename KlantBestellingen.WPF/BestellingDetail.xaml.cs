using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace KlantBestellingen.WPF
{
    /// <summary>
    /// Interaction logic for BestellingDetail.xaml
    /// </summary>
    public partial class BestellingDetail : Window, INotifyPropertyChanged
    {
        #region For WPF interface INotifyProperyChanged
        // Deze code kan altijd in een class gecopieerd worden
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Properties
        // Belangrijk: in WPF moet iets dat in XAML gebruikt wordt, een public property zijn:
        private Klant _klant;
        public Klant Klant
        {
            get => _klant;

            set
            {
                if (_klant == value) // Performantietip - indien er niets verandert, namelijk het blijft dezelfde klant, dan moet ik niet WPF lastig vallen met hertekenen:
                {
                    return;
                }
                _klant = value;
                NotifyPropertyChanged("KlantNaam"); // Door dit te schrijven, zal XAML WPF de property KlantNaam terug opvragen
                NotifyPropertyChanged("KlantAdres");
            }
        }

        public string KlantNaam => _klant != null ? Klant.Naam : "";
        public string KlantAdres => _klant != null ? Klant.Adres : "";

        private double _prijs = 0.0;
        public double Prijs
        {
            get => _prijs;
            set
            {
                if (_prijs == value)
                {
                    return;
                }
                _prijs = value; NotifyPropertyChanged("Prijs");
            }
        }

        private bool _betaald = false;
        public bool Betaald
        {
            get => _betaald;

            set
            {
                if (_betaald == value)
                {
                    return;
                }
                _betaald = value;
                NotifyPropertyChanged("Betaald");
            }
        }

        public string TotalPrice 
        { 
            get 
            { 
                double total = 0.0; 
                foreach (var p in _orderProducts) 
                { 
                    total += p.Prijs; 
                } 
                return total.ToString() + " EUR"; 
            } 
        }

        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private ObservableCollection<Product> _orderProducts = new ObservableCollection<Product>();

        private Bestelling _order;
        public Bestelling Order
        {
            get => _order;
            set
            {
                if (_order == value)
                {
                    return;
                }                
                if(value == null)
                {
                    // We doen een reset van de nuttige inhoud op het bestellingsvenster:
                    // Bestelling bevat geen producten:
                    _orderProducts = new ObservableCollection<Product>();
                    DgProducts.ItemsSource = _orderProducts;
                    // Bestelling is default niet betaald:
                    CbPrijs.IsChecked = false;
                    // Totaal is default 0:
                    TbPrijs.Text = "0 EUR";
                    // Er is nog geen product geselecteerd:
                    CbProducts.SelectedItem = null;
                    // We zeggen tegen XAML WPF: pas je aan aan nieuwe data
                    NotifyPropertyChanged("Order");
                    return;
                }
                _order = value;
                var orders = _order.GeefProducten();
                _orderProducts = new ObservableCollection<Product>();
                foreach(var pkv in orders)
                {
                    for (int i = 0; i < pkv.Value; i++)
                    {
                        _orderProducts.Add(pkv.Key);
                    }
                }
                DgProducts.ItemsSource = _orderProducts;
                NotifyPropertyChanged("Order");
            }
        }
        #endregion

        #region Ctor
        public BestellingDetail()
        {
            InitializeComponent();
            DataContext = this;
            _products = new ObservableCollection<Product>(Context.ProductManager.HaalOp());
            CbProducts.ItemsSource = _products;
            DgProducts.ItemsSource = _orderProducts;
        }
        #endregion

        #region EventHandlers
        private void SlaBestellingOp_Click(object sender, RoutedEventArgs e)
        {
            var orderProducts = new Dictionary<Product, int>();
            var total = 0.0;
            foreach(var p in _orderProducts)
            {
                if(orderProducts.ContainsKey(p))
                {
                    orderProducts[p] += 1;
                }
                else
                {
                    orderProducts.Add(p, 1);
                }
                total += p.Prijs;
            }
            _order = new Bestelling(0, Klant, DateTime.Now, orderProducts) // Id 0 betekent voor database een primary key aanmaken want dit is een identity primary key
            {
                Betaald = (bool)CbPrijs.IsChecked,
                PrijsBetaald = total
            };
            Context.BestellingManager.VoegToe(_order);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CbProducts.SelectedIndex < 0)
                return;
            _orderProducts.Remove(CbProducts.SelectedItem as Product);
            NotifyPropertyChanged("TotalPrice"); // Doordat ik zeg: de totaalprijs is veranderd, zal XAML WPF deze property opnieuw ophalen om de user interface aan te passen
        }

        private void BtnProductAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CbProducts.SelectedIndex < 0)
                return;
            _orderProducts.Add(CbProducts.SelectedItem as Product);
            NotifyPropertyChanged("TotalPrice"); // Doordat ik zeg: de totaalprijs is veranderd, zal XAML WPF deze property opnieuw ophalen om de user interface aan te passen
        }
        #endregion
    }
}
