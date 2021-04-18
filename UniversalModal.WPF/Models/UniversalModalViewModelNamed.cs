using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using MathCore.WPF.Commands;
using UniversalModal.WPF.Interfaces;

namespace UniversalModal.WPF.Models
{
    public class UniversalModalViewModelNamed<T> : MathCore.ViewModels.ViewModel, IUniversalContainerNamed where T : INamedElement, new()
    {

        public UniversalModalViewModelNamed()
        {
            OpenModalCommand = new LambdaCommand(OnOpenModalCommandExecuted);
            CloseModalCommand = new LambdaCommand(OnCloseModalExecuted);
            CreateNewCommand = new LambdaCommand<string>(OnCreateNewCommandExecuted, CanCreateNewCommandExecuted);
            RemoveElementCommand = new LambdaCommand<T>(OnRemoveElementCommandExecuted, NotNullCanCommandExecuted);
            RenameElementCommand = new LambdaCommand<T>(OnRenameElementCommandExecuted, NotNullCanCommandExecuted);
        }

        /// <summary>
        /// Конструктор модели контейнера
        /// </summary>
        /// <param name="elements">Список элементов</param>
        /// <param name="UseUniqueNames">Использовать уникальные имена элементов</param>
        /// <param name="ModalBrush">цвет модального фона</param>
        public UniversalModalViewModelNamed(IEnumerable<T> elements, bool UseUniqueNames, SolidColorBrush ModalBrush = null) : this()
        {
            ModalBrush ??= new SolidColorBrush(Color.FromArgb(200, 169, 169, 169));
            this.ModalBrush = ModalBrush;
            Elements = new ObservableCollection<T>(elements);
            UseUnique = UseUniqueNames;
        }

        #region Свойства
        public bool UseUnique { get; set; } = true;

        private Brush _ModalBrush = new SolidColorBrush(Color.FromArgb(200, 169, 169, 169));
        public Brush ModalBrush
        {
            get => _ModalBrush;
            set
            {
                _ModalBrush = value;
                OnPropertyChanged(nameof(ModalBrush));
            }
        }

        #region Elements : ObservableCollection<T> - перечисление элементов

        /// <summary>перечисление фильтров</summary>
        private ObservableCollection<T> _Elements;

        /// <summary>перечисление фильтров</summary>
        public IList Elements { get => _Elements; set => Set(ref _Elements, (ObservableCollection<T>)value); }

        #endregion

        #region NewName : string - Имя нового элемета

        /// <summary>Имя нового элемента</summary>
        private string _NewName;

        /// <summary>Имя нового элемента</summary>
        public string NewName { get => _NewName; set => Set(ref _NewName, value); }

        #endregion

        #region IsModalVisible : bool - Статус видимости модального окна

        /// <summary>Статус видимости модального окна</summary>
        private bool _IsModalVisible;

        /// <summary>Статус видимости модального окна</summary>
        public bool IsModalVisible { get => _IsModalVisible; set => Set(ref _IsModalVisible, value); }

        #endregion

        #region IsNameEdit : (bool status, T element)? - Режим редактирования имени

        /// <summary>Режим редактирования имени</summary>
        private (bool status, T element)? _IsNameEdit;

        /// <summary>Режим редактирования имени</summary>
        public (bool status, T element)? IsNameEdit { get => _IsNameEdit; set => Set(ref _IsNameEdit, value); }

        #endregion

        #endregion

        #region Command
        private static bool NotNullCanCommandExecuted(T element) => element is not null;

        #region Command OpenModalCommand : Команда создать новый элемент

        /// <summary>Команда создать новый элемент</summary>
        public ICommand OpenModalCommand { get; }

        private void OnOpenModalCommandExecuted() => IsModalVisible = true;

        #endregion

        #region Command CloseModalCommand : Закрыть модальное окно

        /// <summary>Закрыть модальное окно</summary>
        public ICommand CloseModalCommand { get; }

        private void OnCloseModalExecuted()
        {
            IsModalVisible = false;
            NewName = string.Empty;
            IsNameEdit = null;
        }

        #endregion

        #region Command CreateNewCommand : Команда создать новый Элемент

        /// <summary>Команда создать новый элемент</summary>
        public ICommand CreateNewCommand { get; }

        private void OnCreateNewCommandExecuted(string name)
        {
            name = name.Trim(' ');

            if (IsNameEdit != null && IsNameEdit.Value.status)
            {
                if (!IsNameEdit.Value.element.Rename(name))
                    return;
                IsModalVisible = false;
                IsNameEdit = null;
                NewName = string.Empty;
                return;
            }
            IsModalVisible = false;

            NewName = string.Empty;

            var new_element = new T { Name = name };

            Elements.Add(new_element);
        }

        private bool CanCreateNewCommandExecuted(string name) =>
            !name.IsNullOrWhiteSpace() && (!UseUnique || !Elements.OfType<INamedElement>().Any(e => e.Name == name));

        #endregion

        #region Command RemoveElementCommand : Команда удалить элемент

        /// <summary>Команда удалить элемент</summary>
        public ICommand RemoveElementCommand { get; }

        private void OnRemoveElementCommandExecuted(T element) => Elements.Remove(element);

        #endregion

        #region Command RenameElementCommand : Команда переименовать элемент

        /// <summary>Команда переименовать элемент</summary>
        public ICommand RenameElementCommand { get; }

        private void OnRenameElementCommandExecuted(T element)
        {
            IsNameEdit = (true, element);
            NewName = element.Name;
            IsModalVisible = true;
        }

        #endregion

        #endregion
    }
}
