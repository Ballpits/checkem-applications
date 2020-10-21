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

namespace ProjectSC.Views.Settings
{
    /// <summary>
    /// Interaction logic for GeneralSettings_View.xaml
    /// </summary>
    public partial class GeneralSettings_View : UserControl
    {
        public GeneralSettings_View()
        {
            InitializeComponent();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChangeLng.IsChecked == true)
            {
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("Assets/LanguageResourceDictionary/Language-en-us.xaml", UriKind.RelativeOrAbsolute)
                });

                Application.Current.Resources.MergedDictionaries.Remove(new ResourceDictionary()
                {
                    Source = new Uri("Assets/LanguageResourceDictionary/Language-ch-tr.xaml", UriKind.RelativeOrAbsolute)
                });
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("Assets/LanguageResourceDictionary/Language-ch-tr.xaml", UriKind.RelativeOrAbsolute)
                });

                Application.Current.Resources.MergedDictionaries.Remove(new ResourceDictionary()
                {
                    Source = new Uri("Assets/LanguageResourceDictionary/Language-en-us.xaml", UriKind.RelativeOrAbsolute)
                });
            }
        }
    }
}
