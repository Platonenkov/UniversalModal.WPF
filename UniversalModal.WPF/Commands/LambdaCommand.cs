using System;
using System.ComponentModel;
using System.Windows.Markup;
using UniversalModal.WPF.Helpers;

namespace UniversalModal.WPF.Commands
{
    /// <summary>
    /// Лямда-команда
    /// Позволяет быстро указывать методы для выполнения основного тела команды и определения возможности выполнения
    /// </summary>
    [MarkupExtensionReturnType(typeof(LambdaCommand))]
    internal class LambdaCommand : Command
    {
        [NotNull] public static LambdaCommand<T> Create<T>([NotNull] Action<T> OnExecute, [CanBeNull] Func<T, bool> CanExecute = null) => new LambdaCommand<T>(OnExecute, CanExecute);

        #region События

        /// <summary>Возникает, когда команда отменена</summary>
        public event EventHandler Cancelled;

        protected virtual void OnCancelled([CanBeNull] EventArgs args = null) => Cancelled.Start(this, args);

        public event CancelEventHandler StartExecuting;

        protected virtual void OnStartExecuting([NotNull] CancelEventArgs args) => StartExecuting?.Invoke(this, args);

        public event EventHandler<EventArgs<object>> CompliteExecuting;

        protected virtual void OnCompliteExecuting([CanBeNull] EventArgs<object> args) => CompliteExecuting?.Invoke(this, args);

        #endregion

        #region Поля

        /// <summary>Делегат основного тела команды</summary>
        [CanBeNull] protected Action<object> _ExecuteAction;

        /// <summary>Функция определения возможности исполнения команды</summary>
        [CanBeNull] protected Func<object, bool> _CanExecute;

        #endregion

        #region Свойства

        /// <summary>Функция определения возможности исполнения команды</summary>
        public Func<object, bool> CanExecuteDelegate
        {
            get => _CanExecute;
            set
            {
                if (ReferenceEquals(_CanExecute, value)) return;
                _CanExecute = value;
                OnPropertyChanged(nameof(CanExecuteDelegate));
            }
        }

        #endregion

        #region Конструкторы

        protected LambdaCommand() { }

        public LambdaCommand([NotNull] Action<object> ExecuteAction, [CanBeNull] Func<object, bool> CanExecute = null)
        {
            _ExecuteAction = ExecuteAction ?? throw new ArgumentNullException(nameof(ExecuteAction));
            _CanExecute = CanExecute ?? (o => true);
        }

        public LambdaCommand([NotNull] Action ExecuteAction, [CanBeNull] Func<object, bool> CanExecute = null) : this(o => ExecuteAction(), CanExecute)
        {
            if (ExecuteAction is null) throw new ArgumentNullException(nameof(ExecuteAction));
        }

        public LambdaCommand([NotNull] Action ExecuteAction, [CanBeNull] Func<bool> CanExecute) : this(ExecuteAction, CanExecute is null ? null : new Func<object, bool>(o => CanExecute())) { }

        #endregion

        #region Методы

        /// <summary>Выполнение команды</summary>
        /// <param name="parameter">Параметр процессва выполнеия команды</param>
        /// <exception cref="InvalidOperationException">Метод выполнения коменды не определён</exception>
        public override void Execute(object parameter)
        {
            if (_ExecuteAction is null) throw new InvalidOperationException("Метод выполнения команды не определён");
            if (!CanExecute(parameter)) return;

            var cancel_args = new CancelEventArgs();
            OnStartExecuting(cancel_args);
            if (cancel_args.Cancel)
            {
                OnCancelled(cancel_args);
                if (cancel_args.Cancel) return;
            }
            _ExecuteAction?.Invoke(parameter);
            OnCompliteExecuting(new EventArgs<object>(parameter));
        }

        /// <summary>Проверка возможности выполенния команды</summary>
        /// <param name="parameter">Параметр процесса выполнеия команды</param>
        /// <returns>Истина, есл и команда может быть выполнена</returns>
        public override bool CanExecute(object parameter) => IsCanExecute && (_CanExecute?.Invoke(parameter) ?? false);

        /// <summary>Проверка возможности выполнения команды</summary>
        public void CanExecuteCheck() => OnCanExecuteChanged();

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;
            _ExecuteAction = null;
            _CanExecute = null;
        }

        #endregion

        #endregion

        [NotNull] public static implicit operator LambdaCommand([NotNull] Action action) => new LambdaCommand(action);
        [NotNull] public static implicit operator LambdaCommand([NotNull] Action<object> action) => new LambdaCommand(action);
    }

    /// <summary>
    /// Типизированная лямда-команда
    /// Позволяет быстро указывать методы для выполнения основного тела команды и определения возможности выполнения
    /// </summary>
    internal class LambdaCommand<T> : Command
    {
        #region События

        /// <summary>Возникает, когда команда отменена</summary>
        public event EventHandler Cancelled;

        protected virtual void OnCancelled([CanBeNull] EventArgs args = null) => Cancelled.Start(this, args);

        public event CancelEventHandler StartExecuting;

        protected virtual void OnStartExecuting([NotNull] CancelEventArgs args) => StartExecuting?.Invoke(this, args);

        public event EventHandler<EventArgs<object>> CompleteExecuting;

        protected virtual void OnCompleteExecuting([CanBeNull] EventArgs<object> args) => CompleteExecuting?.Invoke(this, args);

        #endregion

        //private readonly bool _IsStructType = typeof(T).

        #region Поля

        private static readonly bool __IsReferenceType = !typeof(T).IsValueType || typeof(T).Name.StartsWith("Nullable`");

        /// <summary>Делегат основного тела команды</summary>
        [CanBeNull] protected Action<T> _ExecuteAction;

        /// <summary>Функция определения возможности исполнения команды</summary>
        [CanBeNull] protected Func<T, bool> _CanExecute;

        #endregion

        #region Свойства

        /// <summary>Функция определения возможности исполнения команды</summary>
        public Func<T, bool> CanExecuteDelegate
        {
            get => _CanExecute;
            set
            {
                if (ReferenceEquals(_CanExecute, value)) return;
                _CanExecute = value;
                OnPropertyChanged(nameof(CanExecuteDelegate));
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Скрытый конструктор для потомков класса, желающих вручную установить значения действия выполнеия команды <see cref="_ExecuteAction"/> и функции проверки выполнимости команды <see cref="_CanExecute"/>
        /// Перед началом использования команды поля <see cref="_ExecuteAction"/> и <see cref="_CanExecute"/> должны быть != <see langword="null"/>
        /// </summary>
        protected LambdaCommand() { }

        public LambdaCommand([NotNull] Action<T> ExecuteAction, [CanBeNull] Func<T, bool> CanExecute = null)
        {
            _ExecuteAction = ExecuteAction ?? throw new ArgumentNullException(nameof(ExecuteAction));
            _CanExecute = CanExecute;
        }

        #endregion

        #region Методы

        [CanBeNull]
        protected object ConvertParameter([CanBeNull] object parameter)
        {
            if (parameter is null) return null;
            var command_parameter_type = typeof(T);
            var parameter_type = parameter.GetType();
            if (command_parameter_type.IsAssignableFrom(parameter_type))
                return parameter;
            var converter = TypeDescriptor.GetConverter(command_parameter_type);
            if (converter.CanConvertFrom(parameter_type))
                return converter.ConvertFrom(parameter);
            converter = TypeDescriptor.GetConverter(parameter_type);
            return converter.CanConvertFrom(command_parameter_type)
                ? converter.ConvertFrom(parameter)
                : null;
            //throw new ArgumentException("Некорректный тип значения параметра");
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            var execute_action = _ExecuteAction ?? throw new InvalidOperationException("Метод выполенния команды не определён");
            if (parameter != null && !(parameter is T))
                parameter = ConvertParameter(parameter);
            var cancel_args = new CancelEventArgs();
            OnStartExecuting(cancel_args);
            if (cancel_args.Cancel)
            {
                OnCancelled(cancel_args);
                if (cancel_args.Cancel) return;
            }
            execute_action?.Invoke((T)parameter);
            OnCompleteExecuting(new EventArgs<object>(parameter));
        }

        public override bool CanExecute(object obj)
        {
            if (!IsCanExecute) return false;
            var can_execute = _CanExecute;
            if (can_execute is null) return true;
            switch (obj)
            {
                case null when __IsReferenceType: return can_execute(default);
                case null: return false;
                case T parameter when can_execute(parameter): return true;
                default: return can_execute((T)ConvertParameter(obj));
            }
        }

        public void CanExecuteCheck() => OnCanExecuteChanged();

        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;
            _ExecuteAction = null;
            _CanExecute = null;
        }

        [NotNull] public static implicit operator LambdaCommand<T>([NotNull] Action<T> action) => new LambdaCommand<T>(action);

    }
}
