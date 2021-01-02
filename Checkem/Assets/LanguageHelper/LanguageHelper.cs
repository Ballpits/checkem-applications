using System;
using System.Windows;

namespace Checkem.Assets.LanguageHelper
{
    static class LanguageHelper
    {
        public static void LanguageSetup()
        {
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri($"Assets/LanguageResourceDictionary/{Properties.Settings.Default.Language}.xaml", UriKind.RelativeOrAbsolute)
            });
        }

        public static void ApplyLanguage(string language)
        {
            string path = "Assets/LanguageResourceDictionary/";//Path for the directory of the dictionary


            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri($"{path}{language}.xaml", UriKind.RelativeOrAbsolute)
            });

            Application.Current.Resources.MergedDictionaries.Remove(new ResourceDictionary()
            {
                Source = new Uri($"{path}{Properties.Settings.Default.Language}.xaml", UriKind.RelativeOrAbsolute)
            });

            Properties.Settings.Default.Language = language;

            Properties.Settings.Default.Save();
        }
    }
}
