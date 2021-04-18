using System.Windows.Input;

namespace UniversalModal.WPF.Interfaces
{
    public interface IEnumerableUniversalModalContainer : IUniversalContainerBase, IModalContainer
    {
        /// <summary>Новый элемент</summary>
        public object NewElement { get ; set; }
        /// <summary>Режим редактирования</summary>
        public (bool status, int index)? IsEditElement { get; set; }
        /// <summary>Команда изменить элемент</summary>
        public ICommand EditElementCommand { get; }

    }
}