using System.Windows;
using System.Windows.Controls;

namespace UniversalModal.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для UniversalModalContainerNamedView.xaml
    /// </summary>
    public partial class UniversalModalContainerNamedView : UserControl
    {
        public UniversalModalContainerNamedView() => InitializeComponent();

        #region ShowAddButton : bool - Показывать кнопуку добавить 

        /// <summary>Показывать кнопку добавить </summary>
        public static readonly DependencyProperty ShowAddButtonProperty =
            DependencyProperty.Register(
                nameof(ShowAddButton),
                typeof(bool),
                typeof(UniversalModalContainerNamedView),
                new PropertyMetadata(true));

        /// <summary>Показывать кнопку добавить </summary>
        public bool ShowAddButton { get => (bool)GetValue(ShowAddButtonProperty); set => SetValue(ShowAddButtonProperty, value); }

        #endregion
    }
}
