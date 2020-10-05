﻿using MaterialDesignThemes.Wpf;
using ProjectSC.Models.AppearanceDataAccess;
using ProjectSC.Models.Object.Appearance;
using ProjectSC.ViewModels.AppearanceSettings;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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

        private void LoadThemePickerButtons(List<Themes> list)
        {
            style = (Style)FindResource("ColorPickerButton");

            foreach (var item in list)
            {
                Button button = new Button();

                button.Name = item.ThemeName;
                button.Style = style;

                button.Click += new RoutedEventHandler(this.ThemePickerButton_Click);

                StpColorPicker.Children.Add(button);
            }
        }

        private void ThemePickerButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Themes themes = ThemesList.Find(x => x.ThemeName == button.Name);

            //AppearanceSettingHelper.ApplyColor(themes.PrimaryColor, themes.SecondaryColor);
        }
    }
}
