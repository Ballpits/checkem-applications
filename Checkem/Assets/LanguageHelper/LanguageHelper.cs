using System;
using System.Windows;

namespace Checkem.Assets.LanguageHelper
{
    public static class LanguageHelper
    {
        public static void LanguageSetup()
        {
            ApplyLanguage(Properties.Settings.Default.LanguageIndex);
        }

        private static void ApplyLanguage(string language)
        {
            string path = "Assets/LanguageResourceDictionary/";//Path for the directory of the dictionary

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri($"{path}{language}.xaml", UriKind.RelativeOrAbsolute)
            });
        }

        public static void ApplyLanguage(int index)
        {
            switch (index)
            {
                case 0:
                    {
                        LanguageHelper.ApplyLanguage("Lng_English_US");

                        break;
                    }
                case 1:
                    {
                        LanguageHelper.ApplyLanguage("Lng_Chinese_Traditional");

                        break;
                    }
                default:
                    break;
            }

            Properties.Settings.Default.LanguageIndex = index;
            Properties.Settings.Default.Save();
        }
    }
}
