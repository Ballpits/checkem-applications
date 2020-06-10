using MaterialDesignThemes.Wpf;
using ProjectSC.UserControls.Custom;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace ProjectSC
{
    public partial class MyDayUSC : UserControl
    {
        public MyDayUSC()
        {
            InitializeComponent();

            item.RetrieveData(ref Inventory);
            item.ResetId(Inventory);
            LoadInList();
        }

        public int TransitionMode { get; set; }

        #region Variables
        ToDoItem item = new ToDoItem();

        public List<ToDoItem> Inventory = new List<ToDoItem>();

        private List<Border> borderlist = new List<Border>();
        private List<CheckBox> checkBoxList = new List<CheckBox>();
        private List<TextBlock> textBlockList = new List<TextBlock>();
        private List<Grid> cBoxList = new List<Grid>();
        #endregion

        private void LoadInList()
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                AddItem(i);
            }

            AddNewButton();
        }

        private void AddNewButton()
        {
            Button ButtonAddNew = new Button
            {
                Height = 50,
                Width = 50,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 10, 10, 0),
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3")),
                BorderThickness = new Thickness(0),
            };
            CornerRadius cr = new CornerRadius(100);
            ButtonAssist.SetCornerRadius(ButtonAddNew, cr);
            ShadowAssist.SetShadowDepth(ButtonAddNew, 0);
            ButtonAddNew.Click += new RoutedEventHandler(this.AddNewButton_Click);

            var icon = new PackIcon { Kind = PackIconKind.Plus };
            icon.Height = 30;
            icon.Width = 30;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Foreground = Brushes.White;
            ButtonAddNew.Content = icon;

            stpMain.Children.Add(ButtonAddNew);
        }

        public void Refresh()
        {
            item.ResetId(Inventory);

            borderlist.Clear();
            checkBoxList.Clear();
            textBlockList.Clear();
            cBoxList.Clear();
            stpMain.Children.Clear();

            LoadInList();
        }

        #region Item
        private void AddItem(int id)
        {
            ItemBar itemBar = new ItemBar(this)
            {
                Id = id,
                Title = Inventory[id].Title
            };

            stpMain.Children.Add(itemBar);
        }
        #endregion

        #region Button evnts
        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            OpenDetailsPanel();
        }
        #endregion

        #region Details panel
        public void OpenDetailsPanel()
        {
            DetailsPanel detailsPanel = new DetailsPanel(this);

            detailsPanel.IsNew = true;

            DataGrid.Children.Add(detailsPanel);
        }

        public void OpenDetailsPanel(int id)
        {
            DetailsPanel detailsPanel = new DetailsPanel(this);

            detailsPanel.IsNew = false;

            detailsPanel.Id = Inventory[id].Id;

            detailsPanel.Title = Inventory[id].Title;
            detailsPanel.Description = Inventory[id].Description;

            detailsPanel.CanNotify = Inventory[id].CanNotify;

            detailsPanel.BeginDateTime = Inventory[id].BeginDateTime;
            detailsPanel.EndDateTime = Inventory[id].EndDateTime;

            DataGrid.Children.Add(detailsPanel);
        }

        public void CloseDetailsPanel()
        {
            DataGrid.Children.RemoveAt(DataGrid.Children.Count - 1);
        }
        #endregion
    }
}
