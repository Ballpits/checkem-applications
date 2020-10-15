using ProjectSC.Models.DataAccess;
using ProjectSC.Models.ToDo;
using ProjectSC.ViewModels.SnackBar;
using ProjectSC.Views.TagPanel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectSC.Views
{
    public partial class ToDoList_View : UserControl
    {
        public ToDoList_View()
        {
            InitializeComponent();

            //test data
            //dataAccess.StoreTestData(Inventory);

            //retrieve data from database
            RetrieveData();

            //refresh id
            Inventory = Inventory.OrderBy(x => x.Id).ToList();
            dataAccess.ResetId(Inventory);

            //load in itembars into stackpanel
            LoadList(Inventory);

            TagList = new TagList_View(this);
            GridTagList.Children.Add(TagList);

            //ListTesterTB.Text = ListViewer.ShowList(Inventory);
        }

        #region Variables
        private DataAccess_Json dataAccess = new DataAccess_Json();

        public List<ToDoItem> Inventory = new List<ToDoItem>();
        private List<ToDoItem> FilteredInventory = new List<ToDoItem>();

        private TagList_View TagList;

        private bool IsDetailsPanelOpened = false;
        private bool IsTagCreationPanelOpened = false;

        private int filterMode = 0;
        private int counter = 0;
        #endregion

        private void RetrieveData()
        {
            dataAccess.RetrieveData(ref Inventory);
        }

        private void LoadList(List<ToDoItem> list)
        {
            stpMain.Children.Clear();

            foreach (var item in list)
            {
                AddItem(item);
            }
        }

        public void AddItemBar()
        {
            int id = Inventory.Count - 1;
            //ListTesterTB.Text = ListViewer.ShowList(Inventory);

            AddItem(Inventory[Inventory.FindIndex(x => x.Id == id)]);
        }

        public void RemoveItemBar(ItemBar_View itembar)
        {
            stpMain.Children.RemoveAt(stpMain.Children.IndexOf(itembar));

            counter--;
            ToDoListItemCounterTextBlock.Text = $"{counter} Items";

            //ListTesterTB.Text = ListViewer.ShowList(Inventory);
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            OpenDetailsPanel();

            counter++;
            ToDoListItemCounterTextBlock.Text = $"{counter} Items";
        }


        #region Item
        private void AddItem(ToDoItem todoItem)
        {
            ItemBar_View itemBar = new ItemBar_View(this)
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
        private void ListFilter(int mode)
        {
            filterMode = mode;

            stpMain.Children.Clear();

            Inventory = Inventory.OrderBy(x => x.Id).ToList();

            counter = 0;

            ButtonClearSort.Visibility = Visibility.Hidden;

            switch (mode)
            {
                case 0://Filter:All items
                    FilteredInventory.Clear();

                    LoadList(Inventory);

                    ToDoListTitleTextBlock.Text = "All Items";

                    counter = Inventory.Count;

                    break;

                case 1://Filter:Reminder
                    FilteredInventory.Clear();

                    for (int index = 0; index < Inventory.Count; index++)
                    {

                        if (Inventory[index].IsReminderOn)
                        {
                            FilteredInventory.Add(Inventory[index]);
                            AddItem(Inventory[index]);

                            counter++;
                        }
                    }
                    ToDoListTitleTextBlock.Text = "Reminder";

                    break;

                case 2://Filter:None
                    FilteredInventory.Clear();

                    for (int index = 0; index < Inventory.Count; index++)
                    {
                        if (Inventory[index].IsStarred)
                        {
                            FilteredInventory.Add(Inventory[index]);
                            AddItem(Inventory[index]);

                            counter++;
                        }
                    }

                    ToDoListTitleTextBlock.Text = "Starred";

                    break;

                default:
                    break;
            }

            switch (counter)
            {
                case 0:
                    ToDoListItemCounterTextBlock.Text = "";
                    break;
                case 1:
                    ToDoListItemCounterTextBlock.Text = "1 Task";
                    break;
                default:
                    ToDoListItemCounterTextBlock.Text = $"{counter} Tasks";
                    break;
            }
        }

        public void Filter(int mode)
        {
            ListFilter(mode);

            CloseDetailsPanel();
        }
        #endregion


        #region Search
        public void SearchItems(string searchString)
        {
            stpMain.Children.Clear();

            counter = 0;

            Inventory = Inventory.OrderBy(x => x.Id).ToList();

            List<ToDoItem> result = new List<ToDoItem>();

            result = Inventory.FindAll(x => x.Title.ToLower().Contains(searchString.ToLower()));

            foreach (var item in result)
            {
                AddItem(item);

                counter++;
            }

            switch (counter)
            {
                case 0:
                    ToDoListItemCounterTextBlock.Text = "Nope... couldn't find anything";
                    break;
                case 1:
                    ToDoListItemCounterTextBlock.Text = "1 Item found";
                    break;
                default:
                    ToDoListItemCounterTextBlock.Text = counter + "Items found";
                    break;
            }
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

            ListFilter(filterMode);
        }
        #endregion


        #region Sort
        private void SortButton_Importance_Click(object sender, RoutedEventArgs e)
        {
            if (filterMode == 0)
            {
                Inventory = Inventory.OrderBy(x => x.IsStarred == false).ToList();

                LoadList(Inventory);
            }
            else
            {
                FilteredInventory = FilteredInventory.OrderBy(x => x.IsStarred == false).ToList();

                LoadList(FilteredInventory);
            }

            ChengeSortingIndicatorText("Importance");

            //ListTesterTB.Text = ListViewer.ShowList(Inventory);
        }

        private void SortButton_DueDate_Click(object sender, RoutedEventArgs e)
        {
            if (filterMode == 0)
            {
                Inventory = Inventory.OrderBy(x => x.EndDateTime).ToList();
                Inventory = Inventory.OrderBy(x => x.IsReminderOn == false).ToList();

                LoadList(Inventory);
            }
            else
            {
                FilteredInventory = FilteredInventory.OrderBy(x => x.EndDateTime).ToList();
                FilteredInventory = FilteredInventory.OrderBy(x => x.IsReminderOn == false).ToList();

                LoadList(FilteredInventory);
            }

            ChengeSortingIndicatorText("Due Date");

            //ListTesterTB.Text = ListViewer.ShowList(Inventory);
        }

        private void SortButton_AlphabeticalAscending_Click(object sender, RoutedEventArgs e)
        {
            if (filterMode == 0)
            {
                Inventory = Inventory.OrderBy(x => x.Title).ToList();

                LoadList(Inventory);
            }
            else
            {
                FilteredInventory = FilteredInventory.OrderBy(x => x.Title).ToList();

                LoadList(FilteredInventory);
            }

            ChengeSortingIndicatorText("Alphabetical Ascending");

            //ListTesterTB.Text = ListViewer.ShowList(Inventory);
        }

        private void SortButton_AlphabeticalDescending_Click(object sender, RoutedEventArgs e)
        {
            if (filterMode == 0)
            {
                Inventory = Inventory.OrderBy(x => x.Title).ToList();
                Inventory.Reverse();

                LoadList(Inventory);
            }
            else
            {
                FilteredInventory = FilteredInventory.OrderBy(x => x.Title).ToList();
                FilteredInventory.Reverse();

                LoadList(FilteredInventory);
            }

            ChengeSortingIndicatorText("Alphabetical Descending");

            //ListTesterTB.Text = ListViewer.ShowList(Inventory);
        }

        private void SortButton_CreationDate_Click(object sender, RoutedEventArgs e)
        {
            if (filterMode == 0)
            {
                Inventory = Inventory.OrderBy(x => x.CreationDateTime).ToList();

                LoadList(Inventory);
            }
            else
            {
                FilteredInventory = FilteredInventory.OrderBy(x => x.CreationDateTime).ToList();

                LoadList(FilteredInventory);
            }

            ChengeSortingIndicatorText("Creation Date");

            //ListTesterTB.Text = ListViewer.ShowList(Inventory);
        }
        #endregion


        #region Details panel
        public void OpenDetailsPanel()
        {
            DetailsPanel_View detailsPanel = new DetailsPanel_View(this)
            {
                IsNew = true
            };

            DataGrid.Children.Add(detailsPanel);

            IsDetailsPanelOpened = true;
        }

        public void OpenDetailsPanel(ItemBar_View itemBar)
        {
            DetailsPanel_View detailsPanel = new DetailsPanel_View(this, itemBar)
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


            IsDetailsPanelOpened = true;
        }

        public void CloseDetailsPanel()
        {
            if (IsDetailsPanelOpened)
            {
                DataGrid.Children.RemoveAt(DataGrid.Children.Count - 1);
                IsDetailsPanelOpened = false;
            }
        }

        public void CloseDetailsPanel(string msg)
        {
            CloseDetailsPanel();
            DataGrid.Children.Add(SnackbarController.OpenSnackBar(msg));
            IsDetailsPanelOpened = false;
        }
        #endregion


        #region Tag creation panel
        public void OpenTagCreationPanel()
        {
            TagCreationPanel_View tagCreationPanel = new TagCreationPanel_View(this, TagList);

            DataGrid.Children.Add(tagCreationPanel);

            IsTagCreationPanelOpened = true;
        }

        public void CloseTagCreationPanel()
        {
            if (IsTagCreationPanelOpened)
            {
                DataGrid.Children.RemoveAt(DataGrid.Children.Count - 1);
                IsTagCreationPanelOpened = false;
            }
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

        private void ButtonClearSort_Click(object sender, RoutedEventArgs e)
        {
            if (filterMode == 0)
            {
                Inventory = Inventory.OrderBy(x => x.Id).ToList();

                LoadList(Inventory);
            }
            else
            {
                FilteredInventory = FilteredInventory.OrderBy(x => x.Id).ToList();

                LoadList(FilteredInventory);
            }

            ButtonClearSort.Visibility = Visibility.Hidden;
        }

        private void ChengeSortingIndicatorText(string text)
        {
            if (!ButtonClearSort.IsVisible)
            {
                ButtonClearSort.Visibility = Visibility.Visible;
            }

            SortIndicatorTextBlock.Text = text;
        }
    }
}
