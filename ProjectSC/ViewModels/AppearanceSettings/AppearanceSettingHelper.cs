using ProjectSC.Models.Object.Appearance;
using System;
using System.Drawing;
using System.Windows;

namespace ProjectSC.ViewModels.AppearanceSettings
{
    public static class AppearanceSettingHelper
    {
        //Apply color and save it to settings
        public static void ApplyColor(Themes themes)
        {
            Properties.Settings.Default.ThemeName = themes.ThemeName;
            Properties.Settings.Default.PrimaryColor = System.Drawing.ColorTranslator.FromHtml(themes.PrimaryColor);
            Properties.Settings.Default.SecondaryColor = System.Drawing.ColorTranslator.FromHtml(themes.SecondaryColor);

            Properties.Settings.Default.Save();
        }

        public static void DarkModeSetup()
        {
            if (Properties.Settings.Default.IsDarkModeApplied)
            {
                ApplyDarkMode();
            }
            else
            {
                ApplyLightMode();
            }
        }

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
            Properties.Settings.Default.GrayMainColor = Color.FromArgb(30, 30, 30);
            Properties.Settings.Default.ItembarColor = Color.Black;

            Properties.Settings.Default.Save();
        }
    }
}
