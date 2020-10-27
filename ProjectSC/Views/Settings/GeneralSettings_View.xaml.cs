using ProjectSC.ViewModels.Language;
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

        private void FilterChanger(object sender, TextChangedEventArgs e)
        {
            if (int.Parse(TagFilterMethod.Text) > 1)
                TagFilterMethod.Text = "1";
            if (int.Parse(TagFilterMethod.Text) < 0)
                TagFilterMethod.Text = "0";
        }

        private void Language_Selected(object sender, RoutedEventArgs e)
        {
            LanguageApplyHelper.ApplyLanguage(sender);
        }
    }
}
