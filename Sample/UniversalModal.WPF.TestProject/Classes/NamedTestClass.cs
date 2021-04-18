using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UniversalModal.WPF.Interfaces;
using UniversalModal.WPF.TestProject.Annotations;

namespace UniversalModal.WPF.TestProject.Classes
{
    public class NamedTestClass : INamedElement
    {

        #region Implementation of INamedElement

        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public bool Rename(string NewName)
        {
            Name = NewName; //тут проверка возможно ли переименование или нет
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        #endregion

        public string Description { get; set; }
    }
}
