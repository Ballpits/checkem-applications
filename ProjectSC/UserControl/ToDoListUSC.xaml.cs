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

            //test data
            //JsonDataAccess.StoreTestData(Inventory);

            //retrieve data from database
            RetrieveData();

            //refresh id
            Inventory = Inventory.OrderBy(x => x.Id).ToList();
            dataAccess.ResetId(Inventory);

            //load in itembars into stackpanel
            LoadList();

            ListTesterTB.Text = ListViewer.ShowList(Inventory);
        }

        #region Variables
        private DataAccess_Json dataAccess = new DataAccess_Json();

        public List<ToDoItem> Inventory = new List<ToDoItem>();

        private string path = "Inventory.json";
        private bool DetailsPanelOpened = false;
        #endregion

        private void RetrieveData()
        {
            dataAccess.RetrieveData(ref Inventory, path);
        }

        private void LoadList()
        {
            stpMain.Children.Clear();

            foreach (var item in Inventory)
            {
                AddItem(item);
            }
        }

        public void AddItemBar()
        {
            int id = Inventory.Count - 1;
            ListTesterTB.Text = ListViewer.ShowList(Inventory);

            AddItem(Inventory[Inventory.FindIndex(x => x.Id == id)]);
        }

        public void RemoveItemBar(ItemBar itembar)
        {
            stpMain.Children.RemoveAt(stpMain.Children.IndexOf(itembar));
            ListTesterTB.Text = ListViewer.ShowList(Inventory);
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            OpenDetailsPanel();
        }

        #region Item
        private void AddItem(ToDoItem todoItem)
        {
            ItemBar itemBar = new ItemBar(this)
            {
                Id = todoItem.Id,
                Title = todoItem.Title,
                Description = todoItem.Description,
                IsCompleted = todoItem.IsCompleted,
                IsStarred = todoItem.IsStarred,
                IsReminderOn = todoItem.IsReminderOn,
                IsAdvanceReminderOn = todoItem.IsAdvanceReminderOn,
                BeginDateTime = todoItem.BeginDateTime,
                EndDateTime = todoItem.EndDateTime
            };

            stpMain.Children.Add(itemBar);
        }
        #endregion


        #region Filter
        public void ListFilter(int filterMode)
        {
            stpMain.Children.Clear();

            Inventory = Inventory.OrderBy(x => x.Id).ToList();

            int counter = 0;

            if (filterMode == 0)//Filter:Starred
            {
                for (int index = 0; index < Inventory.Count; index++)
                {
                    if (Inventory[index].IsStarred)
                    {
                        AddItem(Inventory[index]);
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
                        AddItem(Inventory[index]);
                        counter++;
                    }
                }

                ToDoListTitleTextBlock.Text = "Reminder";
            }
            else if (filterMode == 2)//Filter:None
            {
                LoadList();

                ToDoListTitleTextBlock.Text = "All Items";

                counter = Inventory.Count;
            }

            ToDoListItemCounterTextBlock.Text = $"{counter} Items";
        }
        #endregion


        #region Search
        public void SearchItems(string searchString)
        {
            stpMain.Children.Clear();

            Inventory = Inventory.OrderBy(x => x.Id).ToList();

            List<ToDoItem> result = new List<ToDoItem>();

            result = Inventory.FindAll(x => x.Title.ToLower().Contains(searchString.ToLower()));

            foreach (var item in result)
            {
                AddItem(item);
            }


            ToDoListItemCounterTextBlock.Text = string.Empty;
        }

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
        #endregion


        #region Sort
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

        public void OpenDetailsPanel(ItemBar itemBar)
        {
            DetailsPanel detailsPanel = new DetailsPanel(this, itemBar)
            {
                IsNew = false,

                Id = itemBar.Id,


                //Title = Inventory[Inventory.IndexOf(Inventory.Find(x => x.Id == id))].Title,
                Title = itemBar.Title,

                Description = itemBar.Description,


                IsReminderOn = itemBar.IsReminderOn,
                IsAdvanceReminderOn = itemBar.IsAdvanceReminderOn,

                BeginDateTime = itemBar.BeginDateTime,
                EndDateTime = itemBar.EndDateTime,


                IsUsingTag = itemBar.IsUsingTag,
                TagName = itemBar.TagName
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

        public void CloseDetailsPanel(string msg)
        {
            CloseDetailsPanel();
            DataGrid.Children.Add(SnackbarControl.OpenSnackBar(msg));
        }
        #endregion



        #region List tester button events
        private void ListViewerButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewerGrid.Visibility == Visibility.Collapsed)
            {
                ListViewerGrid.Visibility = Visibility.Visible;
                scrollBar.Margin = new Thickness(0, 75, 0, 200);
            }
            else
            {
                ListViewerGrid.Visibility = Visibility.Collapsed;
                scrollBar.Margin = new Thickness(0, 75, 0, 0);
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
    }
}
