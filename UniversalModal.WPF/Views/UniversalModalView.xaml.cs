using System.Windows;
using System.Windows.Controls;
using UniversalModal.WPF.Interfaces;
using UniversalModal.WPF.Models;

namespace UniversalModal.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для UniversalModalView.xaml
    /// </summary>
    public partial class UniversalModalView : UserControl
    {
        public UniversalModalView()
        {
            primaryContentPresenter = new ContentPresenter();
            modalContentPresenter = new ContentPresenter();
            logicalChildren = new object[2];
            InitializeComponent();
            Controller = new ModalContainer();
        }

        #region ShowOpenButton : bool - Показать базовую кнопку открытия модального окна

        /// <summary>Показать базовую кнопку открытия модального окна</summary>
        public static readonly DependencyProperty ShowOpenButtonProperty =
            DependencyProperty.Register(
                nameof(ShowOpenButton),
                typeof(bool),
                typeof(UniversalModalView),
                new PropertyMetadata(true));

        /// <summary>Показать базовую кнопку открытия модального окна</summary>
        public bool ShowOpenButton { get => (bool)GetValue(ShowOpenButtonProperty); set => SetValue(ShowOpenButtonProperty, value); }

        #endregion

        #region ShowCloseButton : bool - Показать базовую кнопку закрытия модального окна

        /// <summary>Показать базовую кнопку закрытия модального окна</summary>
        public static readonly DependencyProperty ShowCloseButtonProperty =
            DependencyProperty.Register(
                nameof(ShowCloseButton),
                typeof(bool),
                typeof(UniversalModalView),
                new PropertyMetadata(true));

        /// <summary>Показать базовую кнопку закрытия модального окна</summary>
        public bool ShowCloseButton { get => (bool)GetValue(ShowCloseButtonProperty); set => SetValue(ShowCloseButtonProperty, value); }

        #endregion

        private ContentPresenter primaryContentPresenter;       // Hosts the primary content.
        private ContentPresenter modalContentPresenter;         // Hosts the modal content.
        private object[] logicalChildren;

        #region MainContent : object - Контент главной области

        /// <summary>Контент главной области</summary>
        public static readonly DependencyProperty MainContentProperty =
            DependencyProperty.Register(
                nameof(MainContent),
                typeof(object),
                typeof(UniversalModalView),
                new UIPropertyMetadata(null));

        /// <summary>Контент главной области</summary>
        public object MainContent { get => (object)GetValue(MainContentProperty); set => SetValue(MainContentProperty, value); }
        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (UniversalModalView)d;

            /*
             * If the ModalContentPresenter already contains primary content then
             * the existing content will need to be removed from the logical tree.
             */
            if (e.OldValue != null)
            {
                control.RemoveLogicalChild(e.OldValue);
            }

            /*
             * Add the new content to the logical tree of the ModalContentPresenter
             * and update the logicalChildren array so that the correct element is returned
             * when it is requested by WPF.
             */
            control.primaryContentPresenter.Content = e.NewValue;
            control.AddLogicalChild(e.NewValue);
            control.logicalChildren[0] = e.NewValue;
        }

        #endregion

        #region ModalContent : object - Контент модального окна

        /// <summary>Контент модального окна</summary>
        public static readonly DependencyProperty ModalContentProperty =
            DependencyProperty.Register(
                nameof(ModalContent),
                typeof(object),
                typeof(UniversalModalView),
                new UIPropertyMetadata(null/*, OnModalContentChanged*/));

        /// <summary>Контент модального окна</summary>
        public object ModalContent { get => (object)GetValue(ModalContentProperty); set => SetValue(ModalContentProperty, value); }
        private static void OnModalContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (UniversalModalView)d;

            /*
             * If the ModalContentPresenter already contains modal content then
             * the existing content will need to be removed from the logical tree.
             */
            if (e.OldValue != null)
            {
                control.RemoveLogicalChild(e.OldValue);
            }

            /*
             * Add the new content to the logical tree of the ModalContentPresenter
             * and update the logicalChildren array so that the correct element is returned
             * when it is requested by WPF.
             */
            control.modalContentPresenter.Content = e.NewValue;
            control.AddLogicalChild(e.NewValue);
            control.logicalChildren[1] = e.NewValue;
        }

        #endregion


        #region Modal Implementation

        #region Controller : IModalContainer - Сервис управления модальной частью

        /// <summary>Сервис управления модальной частью</summary>
        public static readonly DependencyProperty ControllerProperty =
                DependencyProperty.Register(
                    nameof(Controller),
                    typeof(IModalContainer),
                    typeof(UniversalModalView),
                    new PropertyMetadata(null));

            /// <summary>Сервис управления модальной частью</summary>
            public IModalContainer Controller { get => (IModalContainer)GetValue(ControllerProperty); set => SetValue(ControllerProperty, value); }

            #endregion

        #endregion
    }
}
