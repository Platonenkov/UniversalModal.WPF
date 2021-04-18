using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UniversalModal.WPF.Models;
using UniversalModal.WPF.TestProject.Classes;
using UniversalModal.WPF.TestProject.Views;
using UniversalModal.WPF.Views;

namespace UniversalModal.WPF.TestProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TestModal(object Sender, RoutedEventArgs E)
        {
            var model = new ModalContainer(){ModalBrush = new SolidColorBrush(Color.FromArgb(200, 169, 169, 169))};
            var win = new ModalTestWindow() {DataContext = model};
            win.Show();
        }

        private void TestNamed(object Sender, RoutedEventArgs E)
        {
            var rnd = new Random();
            var data = new List<NamedTestClass>();
            for (var i = 0; i < 10; i++)
                data.Add(new NamedTestClass(){Description = $"Test model {rnd.Next(1,100)}", Name = $"Element {rnd.Next(1,100)}"});

            var model = new UniversalModalViewModelNamed<NamedTestClass>(data, true, new SolidColorBrush(Color.FromArgb(200, 169, 169, 169)));
            var win = new NamedTestWindow(){DataContext = model};
            win.Show();
        }

        private void TestEnumerable(object Sender, RoutedEventArgs E)
        {
            var data = new List<int>();
            for (var i = 1; i < 10; i++)
                data.Add(i);

            var model = new EnumerableUniversalModalViewModel<int>(data,true, new SolidColorBrush(Color.FromArgb(200, 169, 169, 169)));
            
            var win = new EnumerableTestWindow(){DataContext = model};
            //var content = new EnumerableUniversalModalContainerView("This show how enumerate int collection") {DataContext = model};
            //win.Content = content;
            win.Show();

        }
    }
}
