using Checkem.Assets.ColorConverters;
using Cyclops.Models.Objects;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Visual;

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

        Binding ItemCompletedTextColorBindings = new Binding()
        {
            Source = Properties.Settings.Default,
            Path = new PropertyPath("ItemCompletedTextColor", Properties.Settings.Default),
            Converter = new ColorToBrushConverter()
        };

        Binding NormalTextColorBindings = new Binding()
        {
            Source = Properties.Settings.Default,
            Path = new PropertyPath("NormalTextColor", Properties.Settings.Default),
            Converter = new ColorToBrushConverter()
        };

        Binding OverDueTextColorBindings = new Binding()
        {
            Source = Properties.Settings.Default,
            Path = new PropertyPath("OverDueTextColor", Properties.Settings.Default),
            Converter = new ColorToBrushConverter()
        };

        Binding ItembarHighlightColorBindings = new Binding()
        {
            Source = Properties.Settings.Default,
            Path = new PropertyPath("ItembarHighlightColor", Properties.Settings.Default),
            Converter = new ColorToBrushConverter()
        };

        Binding ControlColorBindings = new Binding()
        {
            Source = Properties.Settings.Default,
            Path = new PropertyPath("ControlColor", Properties.Settings.Default),
            Converter = new ColorToBrushConverter()
        };

        PackIcon icon = new PackIcon();
        #endregion

        private void Itembar_Loaded(object sender, RoutedEventArgs e)
        {
            TitleTextBlock.Text = itemProperties.Title;

            VisualUpdate();
        }

        private void VisualUpdate()
        {
            if (itemProperties.IsReminderOn)
            {
                this.Height = 70;

                ReminderIcon.Visibility = Visibility.Visible;
                ReminderDetailTextBlock.Visibility = Visibility.Visible;

                if (this.itemProperties.IsAdvanceReminderOn)
                {
                    ReminderDetailTextBlock.Text = "Start on: " + DateTimeManipulator.SimplifiedDate(this.itemProperties.BeginDateTime.Value) + "\tEnd on: " + DateTimeManipulator.SimplifiedDate(this.itemProperties.EndDateTime.Value);
                }
                else
                {
                    ReminderDetailTextBlock.Text = DateTimeManipulator.SimplifiedDate(this.itemProperties.EndDateTime.Value);
                }

                if (DateTimeManipulator.IsPassed(this.itemProperties.EndDateTime.Value))
                {
                    ReminderDetailTextBlock.SetBinding(TextBlock.ForegroundProperty, OverDueTextColorBindings);
                }
                else
                {
                    ReminderDetailTextBlock.SetBinding(TextBlock.ForegroundProperty, NormalTextColorBindings);
                }
            }
            else
            {
                this.Height = 50;

                ReminderIcon.Visibility = Visibility.Hidden;
                ReminderDetailTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void ItembarBorder_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ItembarBorder.SetBinding(Border.BackgroundProperty, ItembarHighlightColorBindings);
        }

        private void ItembarBorder_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ItembarBorder.SetBinding(Border.BackgroundProperty, ControlColorBindings);
        }
    }
}
