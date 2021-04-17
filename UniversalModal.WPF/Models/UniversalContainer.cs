using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MathCore.WPF.Commands;
using UniversalModal.WPF.Interfaces;

namespace UniversalModal.WPF.Models
{
    public class UniversalContainer<T> : IUniversalContainerBase
    {
        public UniversalContainer()
        {
            
        }
        public UniversalContainer(IEnumerable<T> elements, bool UseUniqueElements)
        {
            UseUnique = UseUniqueElements;
            Elements = new ObservableCollection<T>(elements);
            RemoveElementCommand = new LambdaCommand<T>(OnRemoveElementCommandExecuted, NotNullCanCommandExecuted);
            CreateNewCommand = new LambdaCommand<T>(OnCreateNewCommandExecuted, CanCreateNewCommandExecut);
        }

        public virtual bool UseUnique { get; }

        #region Elements : ObservableCollection<T> - перечисление элементов


        /// <summary>перечисление фильтров</summary>
        public virtual IList Elements { get; }

        #endregion

        #region Commands
        private static bool NotNullCanCommandExecuted(T element) => element is not null;

        #region Command CreateNewCommand : Команда создать новый Элемент

        /// <summary>Команда создать новый элемент</summary>
        public virtual ICommand CreateNewCommand { get; }

        private bool CanCreateNewCommandExecut(T element)
        {
            if (element is null)
            {
                if (Elements.Contains(default))
                    return false;
            }

            if (Elements.Contains(element))
                return false;

            return true;
        }

        private void OnCreateNewCommandExecuted(T element) => Elements.Add(element is null ? default : element);

        #endregion

        #region Command RemoveElementCommand : Команда удалить элемент

        /// <summary>Команда удалить элемент</summary>
        public virtual ICommand RemoveElementCommand { get; }

        private void OnRemoveElementCommandExecuted(T element) => Elements.Remove(element);

        #endregion


        #endregion

    }
}