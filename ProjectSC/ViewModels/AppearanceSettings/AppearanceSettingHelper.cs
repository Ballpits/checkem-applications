using System.Drawing;
using System.Windows.Media;

namespace ProjectSC.ViewModels.AppearanceSettings
{
    public static class AppearanceSettingHelper
    {
        //Apply color and save it to settings
        public static void ApplyColor(string primaryStr, string secondaryStr)
        {
            Properties.Settings.Default.Save();

            //GradientStop gradientStop = new GradientStop(ColorTranslator.FromHtml(primaryStr), 0);

            //Properties.Settings.Default.PrimaryColor = System.Drawing.;
            //Properties.Settings.Default.SecondaryColor = System.Drawing.ColorTranslator.FromHtml(secondaryStr);
        }
    }
}
