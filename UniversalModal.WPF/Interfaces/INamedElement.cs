using System.ComponentModel;

namespace UniversalModal.WPF.Interfaces
{
    public interface INamedElement : INotifyPropertyChanged
    {
        /// <summary> Имя элемента </summary>
        /// <remarks>
        /// <code>
        ///[XmlIgnore]
        ///public string Name
        ///{
        ///    get => DefaultName;
        ///    set
        ///    {
        ///        DefaultName = value;
        ///        OnPropertyChanged(nameof(DefaultName));
        ///    }
        ///}
        /// </code>
        /// </remarks>
        public string Name { get; set; }
        //[XmlIgnore]
        //public string Name
        //{
        //    get => DefaultName;
        //    set
        //    {
        //        DefaultName = value;
        //        OnPropertyChanged(nameof(DefaultName));
        //    }
        //}


        /// <summary> Переименовать элемент </summary>
        /// <param name="NewName">Новое имя</param>
        /// <returns>bool Результат операции переименования</returns>
        /// <remarks>
        /// <code>
        ///public bool Rename(string NewName);
        ///{
        ///    Name = NewName;
        ///    return true;
        ///}
        /// </code>
        /// </remarks>
        public bool Rename(string NewName);
        //{
        //    Name = NewName;
        //    return true;
        //}
    }
}
