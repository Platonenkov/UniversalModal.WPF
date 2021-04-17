using System.Collections;
using System.Windows.Input;

namespace UniversalModal.WPF.Interfaces
{
    public interface IUniversalContainerBase
    {
        /// <summary> Коллекция элементов </summary>
        public IList Elements { get; }
        /// <summary> Уникальность элементов </summary>
        public bool UseUnique { get; }

        #region Commands

        /// <summary> команда удалить элемент из коллекции</summary>
        public ICommand RemoveElementCommand { get; }
        /// <summary> Команда создать и добавить элемент в коллекцию </summary>
        public ICommand CreateNewCommand { get; }

        #endregion
    }
}