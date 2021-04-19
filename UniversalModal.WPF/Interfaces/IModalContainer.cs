using System.Windows.Input;
using System.Windows.Media;

namespace UniversalModal.WPF.Interfaces
{
    public interface IModalContainer
    {
        /// <summary> показано или нет модальное окно </summary>
        public bool IsModalVisible { get; set; }
        public Brush ModalBrush { get; set; }
        #region Commands

        /// <summary> Команда показать модальное окно </summary>
        /// <remarks>
        /// <code>
        ///  OpenModalCommand = new LambdaCommand(OnOpenModalCommandExecute);
        ///  private void OnOpenModalCommandExecute() => IsModalVisible = true;
        /// </code>
        /// </remarks>
        public ICommand OpenModalCommand { get; }

        /// <summary> Команда скрыть модальное окно </summary>
        /// <remarks>
        /// <code>
        ///  CloseModalCommand = new LambdaCommand(OnCloseModalCommandExecute);
        ///  private void OnCloseModalCommandExecute() => IsModalVisible = false;
        /// </code>
        /// </remarks>
        public ICommand CloseModalCommand { get; }

        #endregion

    }
}
