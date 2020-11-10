using Checkem.Assets.ColorConverters;
using Cyclops.Models.Objects;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Checkem.CheckemUserControls
{
    public partial class Itembar : UserControl
    {
        public Itembar(ToDoItem item)
        {
            InitializeComponent();

            itemProperties = item;

            //SetupColor();
        }

        #region Properties
        public ToDoItem itemProperties { get; set; }
        #endregion

        #region Variables
        //private DataAccess_Json dataAccess = new DataAccess_Json();

        //private ToDoList ToDoList;


        const int ChaeckBoxIconSize = 35;

        private bool CheckboxLoaded = false;

        private bool BorderEventCanActivate = true;

        System.Drawing.Color ItemCompletedTextColor_D = Properties.Settings.Default.StrikedOutTextColor;
        System.Drawing.Color ItemPassedTextColor_D = Properties.Settings.Default.OverDueTextColor;

        SolidColorBrush ItemCompletedTextColor, ItemPassedTextColor;

        Binding ItemCompletedTextColorBindings = new Binding()
        {
            Source = Properties.Settings.Default,
            Path = new PropertyPath("ItemCompletedTextColor", Properties.Settings.Default),
            Converter = new ColorToBrushConverter()
        };

        Binding DarkMainColorBindings = new Binding()
        {
            Source = Properties.Settings.Default,
            Path = new PropertyPath("DarkMainColor", Properties.Settings.Default),
            Converter = new ColorToBrushConverter()
        };

        //Binding ItembarHighlightColorBindings = new Binding()
        //{
        //    Source = Properties.Settings.Default,
        //    Path = new PropertyPath("DarkMainColor", Properties.Settings.Default),
        //    Converter = new ColorToBrushConverter()
        //};

        PackIcon icon = new PackIcon();
        #endregion

        private void Itembar_Loaded(object sender, RoutedEventArgs e)
        {
            TitleTextBlock.Text = itemProperties.Title;
        }
    }
}
