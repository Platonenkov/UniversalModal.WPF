using System.Windows.Input;

namespace UniversalModal.WPF.Interfaces
{
    public interface IModalContainer
    {
        public bool IsModalVisible { get; set; }
        #region Commands

        public ICommand OpenModalCommand { get; }
        public ICommand CloseModalCommand { get; }

        #endregion

    }
}
