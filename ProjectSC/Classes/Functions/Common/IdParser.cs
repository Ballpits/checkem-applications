using System.Windows.Controls;

namespace ProjectSC.Classes.Functions
{
    public static class IdParser
    {
        #region Id parser
        public static int ParseId(Button button)
        {
            string idtext = string.Empty;

            for (int i = 0; i < button.Name.Length; i++)
            {
                if (char.IsDigit(button.Name[i]))
                    idtext += button.Name[i];
            }

            return int.Parse(idtext);
        }

        public static int ParseId(CheckBox checkbox)
        {
            string idtext = string.Empty;

            for (int i = 0; i < checkbox.Name.Length; i++)
            {
                if (char.IsDigit(checkbox.Name[i]))
                    idtext += checkbox.Name[i];
            }

            return int.Parse(idtext);
        }

        public static int ParseId(Border border)
        {
            string idtext = string.Empty;

            for (int i = 0; i < border.Name.Length; i++)
            {
                if (char.IsDigit(border.Name[i]))
                    idtext += border.Name[i];
            }

            return int.Parse(idtext);
        }

        public static int ParseId(Grid grid)
        {
            string idtext = string.Empty;

            for (int i = 0; i < grid.Name.Length; i++)
            {
                if (char.IsDigit(grid.Name[i]))
                    idtext += grid.Name[i];
            }

            return int.Parse(idtext);
        }
        #endregion
    }
}
