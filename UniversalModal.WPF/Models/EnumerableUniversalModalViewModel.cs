using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using MathCore.ViewModels;
using MathCore.WPF.Commands;
using UniversalModal.WPF.Interfaces;

namespace UniversalModal.WPF.Models
{
    public class EnumerableUniversalModalViewModel<T> : ViewModel, IEnumerableUniversalModalContainer
    {

        public EnumerableUniversalModalViewModel()
        {
            OpenModalCommand = new LambdaCommand(OnOpenModalCommandExecuted);
            CloseModalCommand = new LambdaCommand(OnCloseModalExecuted);
            CreateNewCommand = new LambdaCommand<object>(OnCreateNewCommandExecuted, CanCreateNewCommandExecuted);
            RemoveElementCommand = new LambdaCommand<int>(OnRemoveElementCommandExecuted);
            EditElementCommand = new LambdaCommand<int>(OnEditElementCommandExecuted);
        }

        /// <summary>
        /// Конструктор модели контейнера
        /// </summary>
        /// <param name="elements">Список элементов</param>
        /// <param name="UseUniqueNames">Использовать уникальные имена элементов</param>
        /// <param name="ModalBrush">цвет модального фона</param>
        public EnumerableUniversalModalViewModel(IEnumerable<T> elements, bool UseUniqueNames, SolidColorBrush ModalBrush = null) : this()
        {
            ModalBrush ??= new SolidColorBrush(Color.FromArgb(200, 169, 169, 169));
            this.ModalBrush = ModalBrush;
            Elements = new ObservableCollection<T>(elements);
            UseUnique = UseUniqueNames;
        }

        #region Свойства
        public bool UseUnique { get; set; } = true;
        private Brush _ModalBrush;
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

        #region NewElement : object - Новый элемент

        /// <summary>Новый элемент</summary>
        private object _NewElement;

        /// <summary>Новый элемент</summary>
        public object NewElement
        {
            get => _NewElement; 
            set => Set(ref _NewElement, value);
        }

        #endregion

        #region IsModalVisible : bool - Статус видимости модального окна

        /// <summary>Статус видимости модального окна</summary>
        private bool _IsModalVisible;

        /// <summary>Статус видимости модального окна</summary>
        public bool IsModalVisible { get => _IsModalVisible; set => Set(ref _IsModalVisible, value); }

        #endregion

        #region IsNameEdit : (bool status, T element)? - Режим редактирования имени

        /// <summary>Режим редактирования имени</summary>
        private (bool status, int index)? _IsEditElement;

        /// <summary>Режим редактирования</summary>
        public (bool status, int index)? IsEditElement { get => _IsEditElement; set => Set(ref _IsEditElement, value); }

        #endregion

        #endregion

        #region Command
        private static bool NotNullCanCommandExecuted(object element) => element is not null;

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
            NewElement = default(T);
            IsEditElement = null;
        }

        #endregion

        #region Command CreateNewCommand : Команда создать новый Элемент

        /// <summary>Команда создать новый элемент</summary>
        public ICommand CreateNewCommand { get; }

        private void OnCreateNewCommandExecuted(object elem)
        {
            if (typeof(T) == typeof(string))
            {
                elem = ((string)elem).Trim(' ');
            }

            if (IsEditElement != null && IsEditElement.Value.status)
            {
                Elements[IsEditElement.Value.index] = Convert.ChangeType(elem, typeof(T));
                IsModalVisible = false;
                IsEditElement = null;
                NewElement = default(T);
                return;
            }
            IsModalVisible = false;

            NewElement = default(T);
            
            Elements.Add(Convert.ChangeType(elem, typeof(T)));
        }

        private bool CanCreateNewCommandExecuted(object elem)
        {
            if (elem is null) return false;

            if (!UseUnique) return true;
            if (typeof(T) == typeof(string) && ((string)elem).IsNullOrWhiteSpace())
                return false;
            if (Elements.Contains(Convert.ChangeType(elem, typeof(T))))
                return false;
            return true;
        }

        #endregion

        #region Command RemoveElementCommand : Команда удалить элемент

        /// <summary>Команда удалить элемент</summary>
        public ICommand RemoveElementCommand { get; }

        private void OnRemoveElementCommandExecuted(int index)
        {
            if(index<0) return;
            Elements.RemoveAt(index);
        }

        #endregion

        #region Command EditElementCommand : Команда изменить элемент

        /// <summary>Команда изменить элемент</summary>
        public ICommand EditElementCommand { get; }

        private void OnEditElementCommandExecuted(int index)
        {
            if(index<0)return;
            
            IsEditElement = (true, index);
            NewElement = _Elements[index];
            IsModalVisible = true;
        }

        #endregion

        #endregion
    }

}
