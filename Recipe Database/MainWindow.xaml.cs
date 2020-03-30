using Recipe_Database.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Recipe_Database
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel vm;

        public MainWindow()
        {
            InitializeComponent();

            vm = DataContext as MainViewModel;
            vm.PropertyChanged += OnModelChangedHandler;
        }

        private void OnModelChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentCardVisualState")
            {
                var state = sender as MainViewModel;
                if (state != null)
                {
                    VisualStateManager.GoToElementState(MainGrid, state.CurrentCardVisualState, false); // WPF
                    //VisualStateManager.GoToState(this, state.CurrentCardVisualState, false);  // UWP
                }
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.SetButtonStates();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            vm.RecipeDoubleClicked();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
            {
                return;
            }

            vm.TypeSelectionChanged();
        }
    }
}
