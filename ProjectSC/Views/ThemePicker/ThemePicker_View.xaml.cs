using ProjectSC.Models.AppearanceDataAccess;
using ProjectSC.Models.Object.Appearance;
using ProjectSC.ViewModels.AppearanceSettings;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProjectSC.Views.ThemePicker
{
    public partial class ThemePicker_View : UserControl
    {
        public ThemePicker_View()
        {
            InitializeComponent();
        }

        #region Variables
        AppearanceDataAccess_Json AppearanceDataAccess = new AppearanceDataAccess_Json();

        List<Themes> ThemesList = new List<Themes>();

        Style style = new Style();
        #endregion

        private void ThemePicker_Loaded(object sender, RoutedEventArgs e)
        {
            ThemesList = AppearanceDataAccess.Retrieve();

            LoadThemePickerButtons(ThemesList);
        }

        private static Color HexToColor(string hexString)
        {
            if (hexString.IndexOf('#') != -1)
                hexString = hexString.Replace("#", "");

            byte r, g, b = 0;

            r = byte.Parse(hexString.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            g = byte.Parse(hexString.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            b = byte.Parse(hexString.Substring(4, 2), NumberStyles.AllowHexSpecifier);

            return Color.FromRgb(r, g, b);
        }

        private void LoadThemePickerButtons(List<Themes> list)
        {
            style = (Style)FindResource("ColorPickerButton");


            foreach (var item in list)
            {
                LinearGradientBrush gradientBrush = new LinearGradientBrush(HexToColor(item.PrimaryColor), HexToColor(item.SecondaryColor), new Point(0.5, 0), new Point(0.5, 1));

                gradientBrush.RelativeTransform = new RotateTransform(-90, 0.5, 0.5);

                Button button = new Button();

                button.Name = item.ThemeName;
                button.Style = style;

                button.Background = gradientBrush;

                button.Click += new RoutedEventHandler(this.ThemePickerButton_Click);

                StpColorPicker.Children.Add(button);
            }
        }

        private void ThemePickerButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Themes themes = ThemesList.Find(x => x.ThemeName == button.Name);

            AppearanceSettingHelper.ApplyColor(themes.PrimaryColor, themes.SecondaryColor);
        }
    }
}
