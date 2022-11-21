using System.Windows;
using System.Windows.Controls;

namespace UniversalModal.WPF.Views
{
    public partial class EnumerableUniversalModalContainerView :UserControl
    {
        public EnumerableUniversalModalContainerView() => InitializeComponent();


        #region IsReadOnly : bool - Read only collection

        /// <summary>Read only collection</summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(
                nameof(IsReadOnly),
                typeof(bool),
                typeof(EnumerableUniversalModalContainerView),
                new PropertyMetadata(default(bool)));

        /// <summary>Read only collection</summary>
        public bool IsReadOnly { get => (bool)GetValue(IsReadOnlyProperty); set => SetValue(IsReadOnlyProperty, value); }

        #endregion
    }
}
