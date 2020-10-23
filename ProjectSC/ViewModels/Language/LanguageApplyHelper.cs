using System;
using System.Windows;
using System.Windows.Controls;

namespace ProjectSC.ViewModels.Language
{
    static class LanguageApplyHelper
    {
        public static void ApplyLanguage(object sender)
        {
            string path = "Assets/LanguageResourceDictionary/";//Path for the directory of the dictionary

            ListBoxItem ListBoxItem = (ListBoxItem)sender;
            string language = ListBoxItem.Name;//get the name from the button

            //set the current language to previous language and replace it with new selected language
            Properties.Settings.Default.PreviousLanguage = Properties.Settings.Default.CurrentLanguage;
            Properties.Settings.Default.CurrentLanguage = language;

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri($"{path}{language}.xaml", UriKind.RelativeOrAbsolute)
            });

            Application.Current.Resources.MergedDictionaries.Remove(new ResourceDictionary()
            {
                Source = new Uri($"{path}{Properties.Settings.Default.PreviousLanguage}.xaml", UriKind.RelativeOrAbsolute)
            });
        }
    }
}
