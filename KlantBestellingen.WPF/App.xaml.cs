using KlantBestellingen.WPF.Languages;
using System.Windows;

// Github referentie: https://github.com/lucvervoort/PS_EF

namespace KlantBestellingen.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private App()
        {
            // Wisselen van taal:
            Translations.Culture = new System.Globalization.CultureInfo("nl-BE"); // en-US nl-BE
            Context.Populate();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: "
+ e.Exception.Message, Translations.ExceptionRaised, MessageBoxButton.OK, MessageBoxImage.Warning);
            // We zeggen hier dat de exception door ons afgehandeld is
            e.Handled = true;
        }
    }
}
