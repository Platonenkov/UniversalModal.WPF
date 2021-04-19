using System;

namespace UniversalModal.WPF.Helpers
{
    /// <summary>Делегат обработчика события</summary>
    /// <typeparam name="TSender">Тип источника события</typeparam>
    /// <typeparam name="TParameter1">Тип параметра 1 аргумента события</typeparam>
    /// <typeparam name="TParameter2">Тип параметра 2 аргумента события</typeparam>
    /// <param name="Sender">Источник события</param>
    /// <param name="Args">Аргумент события</param>
    [Serializable]
    internal delegate void EventHandler<in TSender, TParameter1, TParameter2>(
        TSender Sender,
        EventArgs<TParameter1, TParameter2> Args);
}