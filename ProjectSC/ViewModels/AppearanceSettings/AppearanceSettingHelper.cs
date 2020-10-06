using ProjectSC.Models.Object.Appearance;

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
    }
}
