using System.Windows.Input;

namespace UniversalModal.WPF.Interfaces
{
    public interface IUniversalContainerNamed : IUniversalContainerBase, IModalContainer
    {
        /// <summary> Новое имя для элемента </summary>
        public string NewName { get; set; }

        #region Commands
        /// <summary> Команда переименовать элемент </summary>
        public ICommand RenameElementCommand { get; }

        #endregion
    }
    
}
