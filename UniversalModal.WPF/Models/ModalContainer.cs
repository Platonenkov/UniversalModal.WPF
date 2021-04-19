using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using UniversalModal.WPF.Commands;
using UniversalModal.WPF.Helpers;
using UniversalModal.WPF.Interfaces;

namespace UniversalModal.WPF.Models
{
    public class ModalContainer : IModalContainer, INotifyPropertyChanged
    {
        public ModalContainer()
        {
            OpenModalCommand = new LambdaCommand(OnOpenModalCommandExecuted);
            CloseModalCommand = new LambdaCommand(OnCloseModalExecuted);
        } 

        public ModalContainer(SolidColorBrush ModalBrush):this()
        {
            this.ModalBrush = ModalBrush;
        }

        #region Implementation of IModalContainer

        private bool _isModalVisible;
        public bool IsModalVisible
        {
            get => _isModalVisible;
            set
            {
                _isModalVisible = value;
                OnPropertyChanged(nameof(IsModalVisible));
            }
        }

        private Brush _ModalBrush = new SolidColorBrush(Color.FromArgb(200, 169, 169, 169));
        public Brush ModalBrush
        {
            get=> _ModalBrush;
            set
            {
                _ModalBrush = value;
                OnPropertyChanged(nameof(ModalBrush));
            }
        }

        #region Command

        #region Command OpenModalCommand : Команда создать новый элемент

        /// <summary>Команда создать новый элемент</summary>
        public ICommand OpenModalCommand { get; }

        private void OnOpenModalCommandExecuted() => IsModalVisible = true;

        #endregion

        #region Command CloseModalCommand : Закрыть модальное окно

        /// <summary>Закрыть модальное окно</summary>
        public ICommand CloseModalCommand { get; }

        private void OnCloseModalExecuted() => IsModalVisible = false;

        #endregion


        #endregion

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}