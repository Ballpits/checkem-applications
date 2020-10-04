namespace ProjectSC.ViewModels.AppearanceSettings
{
    public static class AppearanceSettingHelper
    {
        //Apply color and save it to settings
        public static void ApplyColor(string primaryStr,string secondaryStr)
        {
            Properties.Settings.Default.PrimaryColor = System.Drawing.ColorTranslator.FromHtml(primaryStr);
            Properties.Settings.Default.SecondaryColor = System.Drawing.ColorTranslator.FromHtml(secondaryStr);

            Properties.Settings.Default.Save();
        }
    }
}
