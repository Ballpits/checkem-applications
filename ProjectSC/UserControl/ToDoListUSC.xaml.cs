using ProjectSC.UserControls.Custom;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectSC
{
    public partial class ToDoListUSC : UserControl
    {
        public ToDoListUSC()
        {
            InitializeComponent();

            //JsonDataAccess.StoreTestData(Inventory);

            JsonDataAccess.RetrieveData(ref Inventory);
            Inventory = Inventory.OrderBy(x => x.Id).ToList();
            JsonDataAccess.ResetId(Inventory);

            LoadList();

            ListTesterTB.Text = ListViewer.ShowList(Inventory);
        }

        #region Variables
        public List<ToDoItem> Inventory = new List<ToDoItem>();
        private List<ItemBar> itemBarList = new List<ItemBar>();

        private bool DetailsPanelOpened = false;
        #endregion

        public void ListFilter(int filterMode)
        {
            stpMain.Children.Clear();
            itemBarList.Clear();

            Inventory = Inventory.OrderBy(x => x.Id).ToList();

            int counter = 0;

            if (filterMode == 0)//Filter:Starred
            {
                for (int index = 0; index < Inventory.Count; index++)
                {
                    if (Inventory[index].IsStarred)
                    {
                        AddItem(index, Inventory);
                        counter++;
                    }
                }

                ToDoListTitleTextBlock.Text = "Impotant";
            }
            else if (filterMode == 1)//Filter:Reminder
            {
                for (int index = 0; index < Inventory.Count; index++)
                {

                    if (Inventory[index].IsReminderOn)
                    {
                        AddItem(index, Inventory);
                        counter++;
                    }
                }

                ToDoListTitleTextBlock.Text = "Reminder";
            }
            else if (filterMode == 2)//Filter:None
            {
                for (int index = 0; index < Inventory.Count; index++)
                {
                    AddItem(index,Inventory);
                    counter++;
                }

                ToDoListTitleTextBlock.Text = "All Items";
            }

            ToDoListItemCounterTextBlock.Text = $"{counter} Items";
        }

        public void SearchItems(string searchString)
        {
            stpMain.Children.Clear();
            itemBarList.Clear();

            Inventory = Inventory.OrderBy(x => x.Id).ToList();

            List<ToDoItem> result = new List<ToDoItem>();

            result = Inventory.FindAll(x => x.Title.ToLower().Contains(searchString.ToLower()));

            for (int index = 0; index < result.Count; index++)
            {
                AddItem(index,result);
            }

            ToDoListItemCounterTextBlock.Text = string.Empty;
        }


        private void LoadList()
        {
            stpMain.Children.Clear();
            itemBarList.Clear();

            for (int i = 0; i < Inventory.Count; i++)
            {
                AddItem(i,Inventory);
            }
        }

        public void RemoveItemBar(int id)
        {
            stpMain.Children.RemoveAt(stpMain.Children.IndexOf(itemBarList[itemBarList.FindIndex(x => x.Id == id)]));
        }

        public void AddItemBar()
        {
            int id = Inventory.Count - 1;

            AddItem(id,Inventory);
        }

        #region Item
        private void AddItem(int index,List<ToDoItem> list)
        {
            ItemBar itemBar = new ItemBar(this)
            {
                Id = list[index].Id,
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

            DetailsPanelOpened = true;
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
                IsAdvanceReminderOn = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].IsAdvanceReminderOn,

                BeginDateTime = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].BeginDateTime,
                EndDateTime = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].EndDateTime,


                IsUsingTag = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].IsUsingTag,
                TagName = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].TagName
            };

            DataGrid.Children.Add(detailsPanel);


            DetailsPanelOpened = true;
        }

        public void CloseDetailsPanel()
        {
            if (DetailsPanelOpened)
            {
                DataGrid.Children.RemoveAt(DataGrid.Children.Count - 1);
                DetailsPanelOpened = false;
            }
        }
        #endregion

        private void SortButton_Importance_Click(object sender, RoutedEventArgs e)
        {
            Inventory = Inventory.OrderBy(x => x.IsStarred == false).ToList();

            ListTesterTB.Text = ListViewer.ShowList(Inventory);

            LoadList();
        }

        private void SortButton_DueDate_Click(object sender, RoutedEventArgs e)
        {
            Inventory = Inventory.OrderBy(x => x.EndDateTime).ToList();
            Inventory = Inventory.OrderBy(x => x.IsReminderOn == false).ToList();

            ListTesterTB.Text = ListViewer.ShowList(Inventory);

            LoadList();
        }

        private void SortButton_AlphabeticalAscending_Click(object sender, RoutedEventArgs e)
        {
            Inventory = Inventory.OrderBy(x => x.Title).ToList();

            ListTesterTB.Text = ListViewer.ShowList(Inventory);

            LoadList();
        }

        private void SortButton_AlphabeticalDescending_Click(object sender, RoutedEventArgs e)
        {
            Inventory = Inventory.OrderBy(x => x.Title).ToList();
            Inventory.Reverse();

            ListTesterTB.Text = ListViewer.ShowList(Inventory);

            LoadList();
        }

        private void SortButton_CreationDate_Click(object sender, RoutedEventArgs e)
        {
            Inventory = Inventory.OrderBy(x => x.CreationDateTime).ToList();

            ListTesterTB.Text = ListViewer.ShowList(Inventory);

            LoadList();
        }




        #region List tester button events
        private void ListViewerButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewerGrid.Visibility == Visibility.Collapsed)
            {
                ListViewerGrid.Visibility = Visibility.Visible;
                scrollBar.Margin = new Thickness(0, 40, 0, 200);
            }
            else
            {
                ListViewerGrid.Visibility = Visibility.Collapsed;
                scrollBar.Margin = new Thickness(0, 40, 0, 0);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListTesterTB.Background = Brushes.White;
            ListTesterTB.Foreground = Brushes.Black;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListTesterTB.Background = Brushes.Black;
            ListTesterTB.Foreground = Brushes.White;
        }
        #endregion

        private void SearchBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchItems(SearchBox.Text);

            if (SearchBox.Text == string.Empty)
            {
                ButtonClearSearchBox.Visibility = Visibility.Hidden;
            }
            else
            {
                ButtonClearSearchBox.Visibility = Visibility.Visible;
            }
        }

        private void ButtonClearSearchBox_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Clear();
        }
    }
}
