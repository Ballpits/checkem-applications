using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;

namespace ProjectSC.ViewModels.AppearanceSettings
{
    static class DarkModeHelper
    {
        public static void ApplyLightMode()
        {
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml", UriKind.RelativeOrAbsolute)
            });

            Application.Current.Resources.MergedDictionaries.Remove(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml", UriKind.RelativeOrAbsolute)
            });

            Properties.Settings.Default.IsDarkModeApplied = false;
            Properties.Settings.Default.LightMainColor = Color.White;
            Properties.Settings.Default.DarkMainColor = Color.Black;
            Properties.Settings.Default.GrayMainColor = Color.WhiteSmoke;
            Properties.Settings.Default.ItembarColor = Color.White;

            Properties.Settings.Default.Save();
        }
        public static void ApplyDarkMode()
        {
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml", UriKind.RelativeOrAbsolute)
            });

            Application.Current.Resources.MergedDictionaries.Remove(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml", UriKind.RelativeOrAbsolute)
            });

            Properties.Settings.Default.IsDarkModeApplied = true;
            Properties.Settings.Default.LightMainColor = Color.Black;
            Properties.Settings.Default.DarkMainColor = Color.White;
            Properties.Settings.Default.GrayMainColor = Color.Black;
            Properties.Settings.Default.ItembarColor = Color.DimGray;
         
            Properties.Settings.Default.Save();
        }
    }
}
