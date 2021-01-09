using Checkem.CustomComponents;
using Checkem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Checkem.Views
{
    public partial class TodoList : UserControl, INotifyPropertyChanged
    {
        public TodoList()
        {
            //todoManager.StoreTestData();
            todoManager.ResetId();
            currentInventory = todoManager.Filter(FilterMethods.None);

            DataContext = this;

            InitializeComponent();
        }


        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        #region Variable
        TodoManager todoManager = new TodoManager();
        List<Todo> currentInventory;
        #endregion


        #region Property
        private string _ListName = string.Empty;
        public string ListName
        {
            get
            {
                return _ListName;
            }
            set
            {
                _ListName = value;

                OnPropertyChanged();
            }
        }

        public string ItemCount
        {
            get
            {
                return currentInventory.Count.ToString();
            }
            set
            {
                OnPropertyChanged();
            }
        }

        public string ItemCounterString
        {
            get
            {
                if (currentInventory.Count == 1)
                {
                    return "Task";
                }
                else if (currentInventory.Count > 1)
                {
                    return "Tasks";
                }
                else
                {
                    return "Nothing in the list";
                }
            }
        }

        private FilterMethods _filterMethod = FilterMethods.None;
        public FilterMethods filterMethod
        {
            get
            {
                return _filterMethod;
            }
            set
            {
                if (_filterMethod != value)
                {
                    _filterMethod = value;

                    SetFilter(_filterMethod);
                }
            }
        }
        #endregion



        private void TodoList_Loaded(object sender, RoutedEventArgs e)
        {
            SetFilter(FilterMethods.None);

            OnPropertyChanged();
        }


        public void Reload()
        {
            LoadTodoList(filterMethod, SortMethods.ID);
        }


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private void OnItemCountChanged()
        {
            ItemCount = currentInventory.Count.ToString();
        }


        private void SetFilter(FilterMethods filterMethod)
        {
            LoadTodoList(filterMethod, SortMethods.ID);

            OnItemCountChanged();
        }


        private void LoadTodoList(FilterMethods filterMethod, SortMethods sortMethods)
        {
            currentInventory = todoManager.Filter(filterMethod);
            currentInventory = todoManager.Sort(sortMethods, currentInventory);

            if (TodoItemsStackPanel.Children.Count != 0)
            {
                TodoItemsStackPanel.Children.Clear();
            }

            foreach (var item in currentInventory)
            {
                Itembar itembar = new Itembar(item);
                itembar.Click += new EventHandler(this.Itembar_Click);
                itembar.Remove += new EventHandler(this.Itembar_Remove);
                itembar.Update += new EventHandler(this.Itembar_Update);

                TodoItemsStackPanel.Children.Add(itembar);
            }
        }


        private void LoadTodoList(string searchString)
        {
            currentInventory = todoManager.Filter(filterMethod);
            currentInventory = todoManager.Sort(SortMethods.ID, currentInventory);
            currentInventory = todoManager.FindAll(searchString, currentInventory);

            if (TodoItemsStackPanel.Children.Count != 0)
            {
                TodoItemsStackPanel.Children.Clear();
            }

            foreach (var item in currentInventory)
            {
                Itembar itembar = new Itembar(item);
                itembar.Click += new EventHandler(this.Itembar_Click);
                itembar.Remove += new EventHandler(this.Itembar_Remove);
                itembar.Update += new EventHandler(this.Itembar_Update);

                TodoItemsStackPanel.Children.Add(itembar);
            }

            OnItemCountChanged();
        }

        private void Itembar_Click(object sender, EventArgs e)
        {
            //get triggered item bar;
            Itembar itembar = sender as Itembar;

            //create details creation panel and set close event handler
            DetailsPanel detailsPanel = new DetailsPanel(itembar);
            detailsPanel.Close += new EventHandler(this.Panel_Close);

            //show
            DataGrid.Children.Add(detailsPanel);
        }

        private void Itembar_SaveNewTask(object sender, EventArgs e)
        {
            //get triggered item bar;
            Itembar itembar = sender as Itembar;

            //create new task
            todoManager.Update(itembar.todo);
        }

        private void Itembar_Remove(object sender, EventArgs e)
        {
            //get triggered item bar;
            Itembar itembar = sender as Itembar;

            TodoItemsStackPanel.Children.Remove(itembar);
            currentInventory.Remove(itembar.todo);
            todoManager.Remove(itembar.todo);

            OnItemCountChanged();
        }

        private void Itembar_Update(object sender, EventArgs e)
        {
            Itembar itembar = sender as Itembar;

            todoManager.Update(itembar.todo);
        }

        private void Panel_Close(object sender, EventArgs e)
        {
            //remove the last child in DataGrid to remove panel
            DataGrid.Children.RemoveAt(DataGrid.Children.Count - 1);
        }

        private void TagBar_OpenPanel(object sender, EventArgs e)
        {
            //create tag creation panel and set close event handler
            TagCreationPanel tagCreationPanel = new TagCreationPanel();
            tagCreationPanel.CreateButtonClicked += new EventHandler(this.CreateButton_Click);
            tagCreationPanel.Close += new EventHandler(this.Panel_Close);

            //show
            DataGrid.Children.Add(tagCreationPanel);
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            TagCreationPanel tagCreationPanel = sender as TagCreationPanel;

            tagBar.Create(tagCreationPanel.TextboxTagName.Text, tagCreationPanel.colorPicker.Color);
        }

        public void NewTaskButton_Click(object sender, RoutedEventArgs e)
        {
            Itembar itembar = new Itembar();

            itembar.todo.CreationDateTime = DateTime.Now;

            itembar.Click += new EventHandler(this.Itembar_Click);
            itembar.SaveNewTask += new EventHandler(this.Itembar_SaveNewTask);
            itembar.Remove += new EventHandler(this.Itembar_Remove);
            itembar.Update += new EventHandler(this.Itembar_Update);

            TodoItemsStackPanel.Children.Insert(0, itembar);
            currentInventory.Add(itembar.todo);
            todoManager.Add(itembar.todo);

            OnItemCountChanged();
        }


        #region Sort button event handlers
        public void SortByStarButton_Click(object sender, RoutedEventArgs e)
        {
            SortIndicatorTextBlock.Text = this.FindResource("Dict_Sort_Starred") as string;
            ButtonClearSort.Visibility = Visibility.Visible;

            LoadTodoList(this.filterMethod, SortMethods.StarredFirst);
        }

        public void SortByDueDateButton_Click(object sender, RoutedEventArgs e)
        {
            SortIndicatorTextBlock.Text = this.FindResource("Dict_Sort_DueDate") as string;
            ButtonClearSort.Visibility = Visibility.Visible;

            LoadTodoList(this.filterMethod, SortMethods.EndTime);
        }

        public void SortByAlphabeticalAscendingButton_Click(object sender, RoutedEventArgs e)
        {
            SortIndicatorTextBlock.Text = this.FindResource("Dict_Sort_AlphabeticalAscending") as string;
            ButtonClearSort.Visibility = Visibility.Visible;

            LoadTodoList(this.filterMethod, SortMethods.AlphabeticalAscending);
        }

        public void SortByAlphabeticalDescendingButton_Click(object sender, RoutedEventArgs e)
        {
            SortIndicatorTextBlock.Text = this.FindResource("Dict_Sort_AlphabeticalDescending") as string;
            ButtonClearSort.Visibility = Visibility.Visible;

            LoadTodoList(this.filterMethod, SortMethods.AlphabeticalDescending);
        }

        public void SortByCreationDateButton_Click(object sender, RoutedEventArgs e)
        {
            SortIndicatorTextBlock.Text = this.FindResource("Dict_Sort_CreationDate") as string;
            ButtonClearSort.Visibility = Visibility.Visible;

            LoadTodoList(this.filterMethod, SortMethods.CreationDate);
        }


        //clear sort and hide clear sort button
        private void ClearSortButton_Click(object sender, RoutedEventArgs e)
        {
            LoadTodoList(this.filterMethod, SortMethods.ID);

            ButtonClearSort.Visibility = Visibility.Collapsed;
        }
        #endregion


        private void tagBar_RemoveTag(object sender, EventArgs e)
        {
            Tag tag = sender as Tag;
            //MessageBox.Show(tag.tagItem.Content + Environment.NewLine + tag.tagItem.TagColor + Environment.NewLine + tag.tagItem.ID);
            currentInventory = todoManager.Filter(FilterMethods.None);
            for (int u = 0; u < currentInventory.Count; u++)
            {
                for (int i = 0; i < currentInventory[u].TagItems.Count; i++)
                {
                    if (currentInventory[u].TagItems[i].ID == tag.tagItem.ID)
                    {
                        currentInventory[u].TagItems.Remove(currentInventory[u].TagItems.Find(x => x.ID == tag.tagItem.ID));
                        todoManager.Update(currentInventory[u]);

                        break;
                    }
                }
            }
            currentInventory = todoManager.Filter(filterMethod);

            if (TodoItemsStackPanel.Children.Count != 0)
            {
                TodoItemsStackPanel.Children.Clear();
            }
            foreach (var item in currentInventory)
            {
                Itembar itembar = new Itembar(item);
                itembar.Click += new EventHandler(this.Itembar_Click);
                itembar.Remove += new EventHandler(this.Itembar_Remove);
                itembar.Update += new EventHandler(this.Itembar_Update);

                TodoItemsStackPanel.Children.Add(itembar);
            }
        }


        public void Search(string searchString)
        {
            LoadTodoList(searchString);
        }

        private void tagBar_TagSort(object sender, EventArgs e)
        {
            Tag tag = sender as Tag;
            if (TodoItemsStackPanel.Children.Count != 0)
            {
                TodoItemsStackPanel.Children.Clear();
            }

            SortByTag(tag, tag.IsSelected);

        }

        private void SortByTag(Tag tag, bool Selected)
        {
            if (Selected)
            {
                foreach (Todo item in currentInventory)
                {
                    for (int o = 0; o < item.TagItems.Count; o++)
                    {
                        if (item.TagItems[o].ID == tag.tagItem.ID)
                        {
                            //MessageBox.Show("Succes");

                            Itembar itembar = new Itembar(item);
                            itembar.Click += new EventHandler(this.Itembar_Click);
                            itembar.Remove += new EventHandler(this.Itembar_Remove);
                            itembar.Update += new EventHandler(this.Itembar_Update);

                            TodoItemsStackPanel.Children.Add(itembar);

                        }
                    }
                }
            }
            else
            {
                foreach (var item in currentInventory)
                {
                    Itembar itembar = new Itembar(item);
                    itembar.Click += new EventHandler(this.Itembar_Click);
                    itembar.Remove += new EventHandler(this.Itembar_Remove);
                    itembar.Update += new EventHandler(this.Itembar_Update);

                    TodoItemsStackPanel.Children.Add(itembar);
                }
            }
        }

        private void tagBar_ItemTagRestId(object sender, EventArgs e)
        {
            TagManager tagManager = sender as TagManager;

            currentInventory = todoManager.Filter(FilterMethods.None);

            foreach (TagItem item in tagManager.Inventory)
            {
                for (int i = 0; i < currentInventory.Count; i++)
                {
                    if (currentInventory[i].TagItems.Exists(x => x.Content == item.Content))
                    {
                        currentInventory[i].TagItems[currentInventory[i].TagItems.FindIndex(x => x.Content == item.Content)].ID = item.ID;
                        todoManager.Update(currentInventory[i]);
                    }                    
                }
            }
            foreach (Todo item in currentInventory)
            {
                foreach (TagItem test in item.TagItems)
                    MessageBox.Show(test.ID.ToString());
            }

            currentInventory = todoManager.Filter(filterMethod);
        }
    }
}
