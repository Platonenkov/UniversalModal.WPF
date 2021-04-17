using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using MathCore.WPF.Converters;

namespace UniversalModal.WPF.Converters
{
    [MarkupExtensionReturnType(typeof(BoolToVisibility)), ValueConversion(typeof(bool), typeof(Visibility))]
    internal class BoolToVisibility : ValueConverter
    {
        public bool Collapse { get; set; }
        public bool Inverse { get; set; }

        protected override object Convert(object v, Type t, object parameter, CultureInfo c)
        {
            return v switch
            {
                bool bool_value when bool_value => Inverse ? Collapse ? Visibility.Collapsed : Visibility.Hidden : Visibility.Visible,
                bool bool_value when !bool_value => Inverse ? Visibility.Visible : Collapse ? Visibility.Collapsed : Visibility.Hidden,
                Visibility visible when visible == Visibility.Visible => !Inverse,
                Visibility visible when visible == Visibility.Hidden => Inverse,
                Visibility visible when visible == Visibility.Collapsed => Inverse,
                _ => null
            };
        }

        protected override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            return v switch
            {
                bool bool_value when bool_value => Inverse ? Collapse ? Visibility.Collapsed : Visibility.Hidden : Visibility.Visible,
                bool bool_value when !bool_value => Inverse ? Visibility.Visible : Collapse ? Visibility.Collapsed : Visibility.Hidden,
                Visibility visible when visible == Visibility.Visible => !Inverse,
                Visibility visible when visible == Visibility.Hidden => Inverse,
                Visibility visible when visible == Visibility.Collapsed => Inverse,
                _ => null
            };
        }

    }
}
