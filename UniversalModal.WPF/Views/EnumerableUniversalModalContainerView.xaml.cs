using System.Windows;
using System.Windows.Controls;

namespace UniversalModal.WPF.Views
{
    public partial class EnumerableUniversalModalContainerView :UserControl
    {
        public EnumerableUniversalModalContainerView() => InitializeComponent();

        #region HeaderContent : object - Заголовок таблицы элементов

        /// <summary>Заголовок таблицы элементов</summary>
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register(
                nameof(HeaderContent),
                typeof(object),
                typeof(EnumerableUniversalModalContainerView),
                new PropertyMetadata(default(object)));

        /// <summary>Заголовок таблицы элементов</summary>
        public object HeaderContent { get => (object) GetValue(HeaderContentProperty); set => SetValue(HeaderContentProperty, value); }

        #endregion
    }
}
