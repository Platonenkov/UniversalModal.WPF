using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xaml;
using UniversalModal.WPF.Helpers;

namespace UniversalModal.WPF.Commands
{
    internal abstract class Command : MarkupExtension, ICommand, INotifyPropertyChanged, IDisposable
    {
        #region События

        #region INotifyPropertyChanged

        private event PropertyChangedEventHandler PropertyChangedHandlers;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged { add => PropertyChangedHandlers += value; remove => PropertyChangedHandlers -= value; }

        [NotifyPropertyChangedInvocator]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        protected virtual void OnPropertyChanged([Helpers.NotNull][CallerMemberName] string PropertyName = null) => PropertyChangedHandlers?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

        [NotifyPropertyChangedInvocator]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        protected virtual bool Set<T>([CanBeNull] ref T field, [CanBeNull] T value, [Helpers.NotNull][CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        #endregion

        #region ICommand 

        private event EventHandler CanExecuteChangedHandlers;

        protected virtual void OnCanExecuteChanged([CanBeNull] EventArgs e = null) => CanExecuteChangedHandlers?.Invoke(this, e ?? EventArgs.Empty);

        /// <summary>Событие возникает при изменении возможности исполнения команды</summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedHandlers += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedHandlers -= value;
            }
        }

        #endregion

        #endregion

        #region Поля


        #endregion

        #region Свойства

        [CanBeNull] private WeakReference _TargetObject;

        [CanBeNull]
        protected object TargetObject
        {
            get => _TargetObject?.Target;
            private set => _TargetObject = new WeakReference(value);
        }

        [CanBeNull] private WeakReference _RootObject;

        [CanBeNull]
        protected object RootObject
        {
            get => _RootObject?.Target;
            private set => _RootObject = new WeakReference(value);
        }

        [CanBeNull] private WeakReference _TargetProperty;

        [CanBeNull]
        protected object TargetProperty
        {
            get => _TargetProperty?.Target;
            private set => _TargetProperty = new WeakReference(value);
        }

        private bool _IsCanExecute = true;

        /// <summary>Признак возможности исполнения</summary>
        public bool IsCanExecute
        {
            get => _IsCanExecute;
            set
            {
                if (_IsCanExecute == value) return;
                _IsCanExecute = value;
                OnPropertyChanged(nameof(IsCanExecute));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        #region Name : string - Имя команды

        /// <summary>Имя команды</summary>
        private string _Name;

        /// <summary>Имя команды</summary>
        public string Name { get => _Name; set => Set(ref _Name, value); }

        #endregion

        #region Description : string - Описание

        /// <summary>Описание</summary>
        private string _Description;

        /// <summary>Описание</summary>
        public string Description { get => _Description; set => Set(ref _Description, value); }

        #endregion

        #endregion

        protected Command() { }

        protected Command(string Name) => _Name = Name;

        protected Command(string Name, string Description) : this(Name) => _Description = Description;

        #region MarkupExtension

        /// <inheritdoc />
        [Helpers.NotNull]
        public override object ProvideValue(IServiceProvider sp)
        {
            var target_value_provider = (IProvideValueTarget)sp.GetService(typeof(IProvideValueTarget));
            TargetObject = target_value_provider?.TargetObject;
            TargetProperty = target_value_provider?.TargetProperty;
            var root_object_provider = (IRootObjectProvider)sp.GetService(typeof(IRootObjectProvider));
            RootObject = root_object_provider?.RootObject ?? (TargetObject as FrameworkElement)?.FindVisualRoot();
            return this;
        }

        #endregion

        #region ICommand

        public virtual bool CanExecute([CanBeNull] object parameter) => /*ViewModel.IsDesignMode ||*/ _IsCanExecute;
        public abstract void Execute([CanBeNull] object parameter);

        #endregion

        /// <inheritdoc />
        public override string ToString() => Name ?? base.ToString();

        #region IDisposable

        private bool f_Disposed;
        public void Dispose()
        {
            if (f_Disposed) return;
            Dispose(true);
            GC.SuppressFinalize(this);
            f_Disposed = true;
        }

        protected virtual void Dispose(bool disposing) { }

        #endregion
    }
}