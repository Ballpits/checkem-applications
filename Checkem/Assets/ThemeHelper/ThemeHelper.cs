using System;
using System.Windows;

namespace Checkem.Assets.ThemeHelper
{
    public static class ThemeHelper
    {
        public static void ThemeSetup()
        {
            ApplyTheme(Properties.Settings.Default.ThemeIndex);

            DarkModeSetup();
        }

        public static void DarkModeSetup()
        {
            if (Properties.Settings.Default.IsDarkModeOn)
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

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("Assets/ThemeResourceDictionary/Theme/Light.xaml", UriKind.RelativeOrAbsolute)
            });

            Application.Current.Resources.MergedDictionaries.Remove(new ResourceDictionary()
            {
                Source = new Uri("Assets/ThemeResourceDictionary/Theme/Dark.xaml", UriKind.RelativeOrAbsolute)
            });
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

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("Assets/ThemeResourceDictionary/Theme/Dark.xaml", UriKind.RelativeOrAbsolute)
            });

            Application.Current.Resources.MergedDictionaries.Remove(new ResourceDictionary()
            {
                Source = new Uri("Assets/ThemeResourceDictionary/Theme/Light.xaml", UriKind.RelativeOrAbsolute)
            });
        }

        public static void ApplyTheme(string theme)
        {
            string path = "Assets/ThemeResourceDictionary/Color/";//Path for the directory of the dictionary

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri($"{path}{theme}.xaml", UriKind.RelativeOrAbsolute)
            });
        }

        public static void ApplyTheme(int index)
        {
            switch (index)
            {
                case 0:
                    {
                        ApplyTheme("GalaxySlerpy");

                        break;
                    }
                case 1:
                    {
                        ApplyTheme("Ver");

                        break;
                    }
                case 2:
                    {
                        ApplyTheme("Shine");

                        break;
                    }
                case 3:
                    {
                        ApplyTheme("Peach");

                        break;
                    }
                case 4:
                    {
                        ApplyTheme("Evening");

                        break;
                    }
                case 5:
                    {
                        ApplyTheme("DarkOcean");

                        break;
                    }
                case 6:
                    {
                        ApplyTheme("Memory");

                        break;
                    }
                case 7:
                    {
                        ApplyTheme("Amin");

                        break;
                    }
                case 8:
                    {
                        ApplyTheme("Harvey");

                        break;
                    }
                case 9:
                    {
                        ApplyTheme("Neuromancer");

                        break;
                    }
                case 10:
                    {
                        ApplyTheme("Azur");

                        break;
                    }
                case 11:
                    {
                        ApplyTheme("TheWhitch");

                        break;
                    }
                case 12:
                    {
                        ApplyTheme("Flare");

                        break;
                    }
                case 13:
                    {
                        ApplyTheme("Violet");

                        break;
                    }
                case 14:
                    {
                        ApplyTheme("BurningOrange");

                        break;
                    }
                case 15:
                    {
                        ApplyTheme("SummerDays");

                        break;
                    }
                case 16:
                    {
                        ApplyTheme("Shifter");

                        break;
                    }
                case 17:
                    {
                        ApplyTheme("PunYeta");

                        break;
                    }
                default:
                    break;
            }

            Properties.Settings.Default.ThemeIndex = index;
            Properties.Settings.Default.Save();
        }
    }
}
