using MaterialDesignThemes.Wpf;
using ProjectSC.UserControls.Custom;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;


namespace ProjectSC
{
    public partial class MyDayUSC : UserControl
    {
        public MyDayUSC()
        {
            InitializeComponent();

            DataAccess.StoreTestData(Inventory);

            DataAccess.RetrieveData(ref Inventory);
            DataAccess.ResetId(Inventory);

            LoadList();
        }

        #region Variables
        public List<ToDoItem> Inventory = new List<ToDoItem>();

        private List<ItemBar> itemBarList = new List<ItemBar>();
        #endregion

        private void LoadList()
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

        public void UpdateItemBar()
        {
            
        }

        public void RemoveItemBar(int id)
        {
            stpMain.Children.RemoveAt(itemBarList.IndexOf(itemBarList[id]));
            itemBarList.RemoveAt(itemBarList.IndexOf(itemBarList[id]));
            DataAccess.ResetId(Inventory);
        }

        #region Item
        private void AddItem(int id)
        {
            ItemBar itemBar = new ItemBar(this)
            {
                Id = Inventory[id].Id,
                Title = Inventory[id].Title
            };

            itemBarList.Add(itemBar);
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
            DetailsPanel detailsPanel = new DetailsPanel(this)
            {
                IsNew = true
            };

            DataGrid.Children.Add(detailsPanel);
        }

        public void OpenDetailsPanel(int id)
        {
            DetailsPanel detailsPanel = new DetailsPanel(this)
            {
                IsNew = false,

                Id = Inventory[id].Id,
                Title = Inventory[id].Title,
                Description = Inventory[id].Description,
                CanNotify = Inventory[id].CanNotify,
                BeginDateTime = Inventory[id].BeginDateTime,
                EndDateTime = Inventory[id].EndDateTime
            };

            DataGrid.Children.Add(detailsPanel);
        }

        public void CloseDetailsPanel()
        {
            DataGrid.Children.RemoveAt(DataGrid.Children.Count - 1);
        }
        #endregion
    }
}
