using ProjectSC.UserControls.Custom;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


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

            ListTesterTB.Text = ListViewer.ShowList(Inventory);
        }

        #region Variables
        public List<ToDoItem> Inventory = new List<ToDoItem>();
        private List<ItemBar> itemBarList = new List<ItemBar>();

        private int filterMode = 0;
        #endregion

        public void LoadList(int filterMode)
        {
            stpMain.Children.Clear();
            itemBarList.Clear();

            DataAccess.RetrieveData(ref Inventory);
            DataAccess.ResetId(Inventory);

            for (int index = 0; index < Inventory.Count; index++)
            {
                if (filterMode == 0)//Filter:Improtance
                {
                    if (Inventory[index].IsImportant)
                    {
                        AddItem(index);
                    }
                }
                if (filterMode == 1)//Filter:Reminder
                {
                    if (Inventory[index].IsReminderOn)
                    {
                        AddItem(index);
                    }
                }
                if (filterMode == 2)//Filter:None
                {
                    AddItem(index);
                }
            }
        }

        public void RemoveItemBar(int id)
        {
            stpMain.Children.RemoveAt(stpMain.Children.IndexOf(itemBarList[itemBarList.FindIndex(x => x.Id == id)]));
        }

        public void AddItemBar()
        {
            int id = Inventory.Count - 1;

            AddItem(id);
        }

        #region Item
        private void AddItem(int index)
        {
            ItemBar itemBar = new ItemBar(this)
            {
                Id = Inventory[index].Id,
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

                Id = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].Id,


                Title = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].Title,
                //Title = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].Id.ToString(),

                Description = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].Description,


                IsReminderOn = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].IsReminderOn,
                IsAdvanceOn = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].IsAdvanceOn,

                BeginDateTime = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].BeginDateTime,
                EndDateTime = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].EndDateTime,


                IsUsingTag = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].IsUsingTag,
                TagName = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].TagName
            };

            DataGrid.Children.Add(detailsPanel);
        }

        public void CloseDetailsPanel()
        {
            if (DataGrid.Children.Count > 2)
            {
                DataGrid.Children.RemoveAt(DataGrid.Children.Count - 1);
            }
        }
        #endregion

        private void ButtonImp_Click(object sender, RoutedEventArgs e)
        {
            Inventory = Inventory.OrderBy(x => x.IsImportant == false).ToList();

            ListTesterTB.Text = ListViewer.ShowList(Inventory);


            stpMain.Children.Clear();
            itemBarList.Clear();

            for (int i = 0; i < Inventory.Count; i++)
            {
                ItemBar itemBar = new ItemBar(this)
                {
                    Id = Inventory[i].Id,
                };

                itemBarList.Add(itemBar);
                stpMain.Children.Add(itemBar);
            }
        }

        private void ButtonDueDate_Click(object sender, RoutedEventArgs e)
        {
            Inventory = Inventory.OrderBy(x => x.EndDateTime).ToList();

            ListTesterTB.Text = ListViewer.ShowList(Inventory);

            LoadList(2);
        }
    }
}
