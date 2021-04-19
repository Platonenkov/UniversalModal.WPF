using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace UniversalModal.WPF.Helpers
{
    /// <summary>Класс методов расширений для обработчиков событий</summary>
    internal static class EventHandlerExtension
    {
        /// <summary>Асинхронный запуск обработчика события с созданием новой задачи</summary>
        /// <param name="handler">Запускаемый обработчик события</param>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        /// <returns>Задача асинхронного выполнения обработчика события</returns>
        [NotNull]
        public static Task InvokeAsync([CanBeNull] this EventHandler handler, object sender, EventArgs e) => handler != null ? Task.Run((Action)(() => handler(sender, e))) : Task.CompletedTask;

        /// <summary>Асинхронный запуск обработчика события с созданием новой задачи</summary>
        /// <param name="handler">Запускаемый обработчик события</param>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        /// <returns>Задача асинхронного выполнения обработчика события</returns>
        [NotNull]
        public static Task InvokeAsync<TEventArgs>(
          [CanBeNull] this EventHandler<TEventArgs> handler,
          object sender,
          TEventArgs e)
          where TEventArgs : EventArgs
        {
            return handler != null ? Task.Run((Action)(() => handler(sender, e))) : Task.CompletedTask;
        }

        /// <summary>Потоко-безопасная генерация события</summary>
        /// <param name="Handler">Обработчик события</param>
        /// <param name="Sender">Источник события</param>
        /// <param name="e">Аргумент события</param>
        [DebuggerStepThrough]
        public static void Start(
          [CanBeNull] this NotifyCollectionChangedEventHandler Handler,
          object Sender,
          NotifyCollectionChangedEventArgs e)
        {
            if (Handler == null)
                return;
            foreach (Delegate invocation in Handler.GetInvocationList())
            {
                if (invocation.Target is ISynchronizeInvoke target && target.InvokeRequired)
                    target.Invoke(invocation, new object[2]
                    {
            Sender,
            (object) e
                    });
                else
                    invocation.DynamicInvoke(Sender, (object)e);
            }
        }

        /// <summary>Потоко-безопасная генерация события</summary>
        /// <param name="Handler">Обработчик события</param>
        /// <param name="Sender">Источник события</param>
        /// <param name="PropertyName">Имя изменившегося свойства</param>
        public static void Start(
          this PropertyChangedEventHandler Handler,
          object Sender,
          [CallerMemberName, CanBeNull] string PropertyName = null)
        {
            Handler.Start(Sender, new PropertyChangedEventArgs(PropertyName));
        }

        /// <summary>Потоко-безопасная генерация события</summary>
        /// <param name="Handler">Обработчик события</param>
        /// <param name="Sender">Источник события</param>
        /// <param name="PropertyName">Имена изменившихся свойств</param>
        public static void Start(
          [CanBeNull] this PropertyChangedEventHandler Handler,
          object Sender,
          [NotNull] params string[] PropertyName)
        {
            if (PropertyName == null)
                throw new ArgumentNullException(nameof(PropertyName));
            if (Handler == null || PropertyName.Length == 0)
                return;
            PropertyChangedEventArgs[] array = ((IEnumerable<string>)PropertyName).ToArray<string, PropertyChangedEventArgs>((Func<string, PropertyChangedEventArgs>)(name => new PropertyChangedEventArgs(name)));
            foreach (Delegate invocation in Handler.GetInvocationList())
            {
                if (invocation.Target is ISynchronizeInvoke target && target.InvokeRequired)
                {
                    foreach (PropertyChangedEventArgs changedEventArgs in array)
                        target.Invoke(invocation, new object[2]
                        {
              Sender,
              (object) changedEventArgs
                        });
                }
                else
                {
                    foreach (PropertyChangedEventArgs changedEventArgs in array)
                        invocation.DynamicInvoke(Sender, (object)changedEventArgs);
                }
            }
        }

        /// <summary>Потоко-безопасная генерация события</summary>
        /// <param name="Handler">Обработчик события</param>
        /// <param name="Sender">Источник события</param>
        /// <param name="e">Аргумент события</param>
        [DebuggerStepThrough]
        public static void Start(
          [CanBeNull] this PropertyChangedEventHandler Handler,
          object Sender,
          PropertyChangedEventArgs e)
        {
            if (Handler == null)
                return;
            foreach (Delegate invocation in Handler.GetInvocationList())
            {
                if (invocation.Target is ISynchronizeInvoke target && target.InvokeRequired)
                    target.Invoke(invocation, new object[2]
                    {
            Sender,
            (object) e
                    });
                else
                    invocation.DynamicInvoke(Sender, (object)e);
            }
        }

        /// <summary>Потоко-безопасная генерация события</summary>
        /// <param name="Handler">Обработчик события</param>
        /// <param name="Sender">Источник события</param>
        /// <param name="e">Аргумент события</param>
        [DebuggerStepThrough]
        public static void Start([CanBeNull] this EventHandler Handler, object Sender, EventArgs e)
        {
            if (Handler == null)
                return;
            foreach (Delegate invocation in Handler.GetInvocationList())
            {
                if (invocation.Target is ISynchronizeInvoke target && target.InvokeRequired)
                    target.Invoke(invocation, new object[2]
                    {
            Sender,
            (object) e
                    });
                else
                    invocation.DynamicInvoke(Sender, (object)e);
            }
        }

        /// <summary>Потоко-безопасная асинхронная генерация события</summary>
        /// <param name="Handler">Обработчик события</param>
        /// <param name="Sender">Источник события</param>
        /// <param name="e">Аргумент события</param>
        /// <param name="CallBack">Метод завершения генерации события</param>
        /// <param name="State">Объект-состояние, передаваемый в метод завершения генерации события</param>
        [DebuggerStepThrough]
        [CanBeNull]
        public static IAsyncResult StartAsync(
          [CanBeNull] this EventHandler Handler,
          object Sender,
          EventArgs e,
          [CanBeNull] AsyncCallback CallBack = null,
          [CanBeNull] object State = null)
        {
            return Handler != null ? ((Action)(() => Handler(Sender, e))).BeginInvoke(CallBack, State) : (IAsyncResult)null;
        }

        /// <summary>Быстрая генерация события</summary>
        /// <param name="Handler">Обработчик события</param>
        /// <param name="Sender">Источник события</param>
        [DebuggerStepThrough]
        public static void FastStart(this EventHandler Handler, object Sender) => Handler.FastStart(Sender, EventArgs.Empty);

        /// <summary>Быстрая генерация события</summary>
        /// <param name="Handler">Обработчик события</param>
        /// <param name="Sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        [DebuggerStepThrough]
        public static void FastStart([CanBeNull] this EventHandler Handler, object Sender, EventArgs e)
        {
            if (Handler == null)
                return;
            Handler(Sender, e);
        }

        /// <summary>Быстрая генерация события</summary>
        /// <param name="Handler">Обработчик события</param>
        /// <param name="Sender">Источник события</param>
        /// <typeparam name="TEventArgs">Тип аргумента события</typeparam>
        /// <param name="e">Аргументы события</param>
        [DebuggerStepThrough]
        public static void FastStart<TEventArgs>(
          [CanBeNull] this EventHandler<TEventArgs> Handler,
          object Sender,
          TEventArgs e)
          where TEventArgs : EventArgs
        {
            if (Handler == null)
                return;
            Handler(Sender, e);
        }

        /// <summary>Потоко-безопасная генерация события</summary>
        /// <param name="Handler">Обработчик события</param>
        /// <param name="Sender">Источник события</param>
        /// <typeparam name="TEventArgs">Тип аргумента события</typeparam>
        /// <param name="e">Аргументы события</param>
        [DebuggerStepThrough]
        public static void Start<TEventArgs>(
          [CanBeNull] this EventHandler<TEventArgs> Handler,
          object Sender,
          TEventArgs e)
          where TEventArgs : EventArgs
        {
            if (Handler == null)
                return;
            foreach (Delegate invocation in Handler.GetInvocationList())
            {
                if (invocation.Target is ISynchronizeInvoke target && target.InvokeRequired)
                    target.Invoke(invocation, new object[2]
                    {
            Sender,
            (object) e
                    });
                else
                    invocation.DynamicInvoke(Sender, (object)e);
            }
        }

        /// <summary>Потоко-безопасная асинхронная генерация события</summary>
        /// <param name="Handler">Обработчик события</param>
        /// <param name="Sender">Источник события</param>
        /// <typeparam name="TEventArgs">Тип аргумента события</typeparam>
        /// <param name="e">Аргументы события</param>
        /// <param name="CallBack">Метод завершения генерации события</param>
        /// <param name="State">Объект-состояние, передаваемый в метод завершения генерации события</param>
        [DebuggerStepThrough]
        [CanBeNull]
        public static IAsyncResult StartAsync<TEventArgs>(
          [CanBeNull] this EventHandler<TEventArgs> Handler,
          object Sender,
          TEventArgs e,
          [CanBeNull] AsyncCallback CallBack = null,
          [CanBeNull] object State = null)
          where TEventArgs : EventArgs
        {
            return Handler != null ? ((Action)(() =>
            {
                foreach (Delegate invocation in Handler.GetInvocationList())
                {
                    object[] objArray = new object[2]
                    {
            Sender,
            (object) (TEventArgs) e
                    };
                    invocation.DynamicInvoke(objArray);
                }
            })).BeginInvoke(CallBack, State) : (IAsyncResult)null;
        }

        /// <summary>Потоко-безопасная генерация события</summary>
        /// <param name="Handler">Обработчик события</param>
        /// <param name="Sender">Источник события</param>
        /// <typeparam name="TArgs">Тип аргумента события</typeparam>
        /// <param name="Args">Аргументы события</param>
        /// <typeparam name="TResult">Тип результата обработки события</typeparam>
        /// <typeparam name="TSender">Тип источника события</typeparam>
        /// <returns>Массив результатов обработки события</returns>
        [DebuggerStepThrough]
        [NotNull]
        public static TResult[] Start<TResult, TSender, TArgs>(
          [CanBeNull] this EventHandler<TResult, TSender, TArgs> Handler,
          TSender Sender,
          TArgs Args)
        {
            return Handler != null ? ((IEnumerable<Delegate>)Handler.GetInvocationList()).Select<Delegate, TResult>((Func<Delegate, TResult>)(d =>
            {
                object obj;
                if (!(d.Target is ISynchronizeInvoke target) || !target.InvokeRequired)
                    obj = d.DynamicInvoke((object)(TSender)Sender, (object)(TArgs)Args);
                else
                    obj = target.Invoke(d, new object[2]
                    {
            (object) (TSender) Sender,
            (object) (TArgs) Args
                    });
                return (TResult)obj;
            })).ToArray<TResult>() : Array.Empty<TResult>();
        }
    }

    /// <summary>Аргумент события с 2 типизированными параметрами</summary>
    /// <typeparam name="TArgument1">Тип аргумента 1</typeparam>
    /// <typeparam name="TArgument2">Тип аргумента 2</typeparam>
    [DebuggerStepThrough]
    internal class EventArgs<TArgument1, TArgument2> : EventArgs
    {
        /// <summary>1 параметр аргумента</summary>
        public TArgument1 Argument1 { get; set; }

        /// <summary>2 параметр аргумента</summary>
        public TArgument2 Argument2 { get; set; }

        /// <summary>Инициализация нового экземпляра <see cref="T:System.EventArgs`2" /></summary>
        public EventArgs()
        {
        }

        /// <summary>Инициализация нового экземпляра <see cref="T:System.EventArgs`2" /></summary>
        /// <param name="Argument1">1 параметр аргумента</param>
        /// <param name="Argument2">2 параметр аргумента</param>
        public EventArgs(TArgument1 Argument1, TArgument2 Argument2)
        {
            this.Argument1 = Argument1;
            this.Argument2 = Argument2;
        }

        /// <summary>Деконструктор <see cref="T:System.EventArgs`2" /></summary>
        /// <param name="Arg1">1 параметр аргумента</param>
        /// <param name="Arg2">2 параметр аргумента</param>
        public void Deconstruct(out TArgument1 Arg1, out TArgument2 Arg2)
        {
            Arg1 = this.Argument1;
            Arg2 = this.Argument2;
        }

        /// <summary>Оператор неявного преобразования типа <see cref="T:System.EventArgs`2" /> к кортежу</summary>
        /// <param name="Args">Аргумент события <see cref="T:System.EventArgs`2" /></param>
        /// <returns>Кортеж из 2 параметров</returns>
        public static implicit operator (TArgument1 Arg1, TArgument2 Arg2)(
          [NotNull] EventArgs<TArgument1, TArgument2> Args)
        {
            return (Args.Argument1, Args.Argument2);
        }

        /// <summary>Оператор неявного преобразования кортежа из 2 параметров к типу <see cref="T:System.EventArgs`2" /></summary>
        /// <param name="Args">Кортеж из 2 параметров</param>
        /// <returns>Аргумент события <see cref="T:System.EventArgs`2" /></returns>
        [NotNull]
        public static implicit operator EventArgs<TArgument1, TArgument2>(
          (TArgument1 Arg1, TArgument2 Arg2) Args)
        {
            return new EventArgs<TArgument1, TArgument2>(Args.Arg1, Args.Arg2);
        }
    }

}
