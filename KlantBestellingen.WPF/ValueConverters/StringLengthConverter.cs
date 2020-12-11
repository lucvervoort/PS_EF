// HOGENT 
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace KlantBestellingen.WPF.ValueConverters
{
    public class StringLengthConverter : IValueConverter
    {
        // Van code behind naar gui
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value.ToString().Length);
        }

        // Van gui naar code behind
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
