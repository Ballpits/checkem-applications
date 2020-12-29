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
            todoManager.StoreTestData();

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
        public string ItemCount
        {
            get
            {
                if (currentInventory.Count > 0)
                {
                    if (ToDoListItemCounterTextBlock.Margin == new Thickness(0))
                    {
                        ToDoListItemCounterTextBlock.Margin = new Thickness(0, 0, 5, 0);
                    }

                    return currentInventory.Count.ToString();
                }
                else
                {
                    ToDoListItemCounterTextBlock.Margin = new Thickness(0);

                    return string.Empty;
                }
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
            tagCreationPanel.Close += new EventHandler(this.Panel_Close);
            tagCreationPanel.Create += new EventHandler(this.CreateNewTag);

            //show
            DataGrid.Children.Add(tagCreationPanel);
        }

        private void CreateNewTag(object sender, EventArgs e)
        {
            TagCreationPanel Test = sender as TagCreationPanel;

            tagBar.Create(Test.TextboxTagName.Text, Test.colorPicker.Color);
        }

        private void NewTaskButton_Click(object sender, RoutedEventArgs e)
        {
            Itembar itembar = new Itembar();

            itembar.todo.CreationDateTime = DateTime.Now;

            itembar.Click += new EventHandler(this.Itembar_Click);
            itembar.Remove += new EventHandler(this.Itembar_Remove);
            itembar.Update += new EventHandler(this.Itembar_Update);

            TodoItemsStackPanel.Children.Insert(0, itembar);
            currentInventory.Add(itembar.todo);
            todoManager.Add(itembar.todo);

            OnItemCountChanged();
        }


        #region Sort button event handlers
        private void SortByStarButton_Click(object sender, RoutedEventArgs e)
        {
            SortIndicatorTextBlock.Text = this.FindResource("Dict_Sort_Starred") as string;
            ButtonClearSort.Visibility = Visibility.Visible;

            LoadTodoList(this.filterMethod, SortMethods.StarredFirst);
        }

        private void SortByDueDateButton_Click(object sender, RoutedEventArgs e)
        {
            SortIndicatorTextBlock.Text = this.FindResource("Dict_Sort_DueDate") as string;
            ButtonClearSort.Visibility = Visibility.Visible;

            LoadTodoList(this.filterMethod, SortMethods.EndTime);
        }

        private void SortByAlphabeticalAscendingButton_Click(object sender, RoutedEventArgs e)
        {
            SortIndicatorTextBlock.Text = this.FindResource("Dict_Sort_AlphabeticalAscending") as string;
            ButtonClearSort.Visibility = Visibility.Visible;

            LoadTodoList(this.filterMethod, SortMethods.AlphabeticalAscending);
        }

        private void SortByAlphabeticalDescendingButton_Click(object sender, RoutedEventArgs e)
        {
            SortIndicatorTextBlock.Text = this.FindResource("Dict_Sort_AlphabeticalDescending") as string;
            ButtonClearSort.Visibility = Visibility.Visible;

            LoadTodoList(this.filterMethod, SortMethods.AlphabeticalDescending);
        }

        private void SortByCreationDateButton_Click(object sender, RoutedEventArgs e)
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
    }
}
