using MaterialDesignThemes.Wpf;
using ProjectSC.UserControls.Custom;
using System.Collections.Generic;
using System.Linq;
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

            //DataAccess.StoreTestData(Inventory);

            DataAccess.RetrieveData(ref Inventory);
            DataAccess.ResetId(Inventory);

            LoadList(filterMode);
        }

        #region Variables
        public List<ToDoItem> Inventory = new List<ToDoItem>();
        private List<ItemBar> itemBarList = new List<ItemBar>();

        private int filterMode = 0;
        #endregion

        public void LoadList(int filterMode)
        {
            stpMain.Children.Clear();

            for (int i = 0; i < Inventory.Count; i++)
            {
                if (filterMode == 0)
                {
                    if (Inventory[i].IsImportant)
                    {
                        AddItem(i);
                    }
                }
                if (filterMode == 1)
                {
                    if (Inventory[i].CanNotify)
                    {
                        AddItem(i);
                    }
                }
                if (filterMode == 2)
                {
                    AddItem(i);
                }
            }

            AddNewButton();
        }

        private void AddNewButton()
        {
            Button ButtonAddNew = new Button
            {
                Height = 50,
                Width = 50,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 50, 0, 20),
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

        public void RemoveItemBar(int id)
        {
            stpMain.Children.RemoveAt(stpMain.Children.IndexOf(itemBarList[id]));
        }

        #region Item
        private void AddItem(int id)
        {
            ItemBar itemBar = new ItemBar(this)
            {
                Id = id,
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

        public void OpenDetailsPanel(int id, ItemBar itemBar)
        {
            DetailsPanel detailsPanel = new DetailsPanel(this, itemBar)
            {
                IsNew = false,

                Id = id,
                Title = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].Title,
                Description = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].Description,
                CanNotify = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].CanNotify,
                BeginDateTime = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].BeginDateTime,
                EndDateTime = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].EndDateTime
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
