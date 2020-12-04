using Checkem.CustomComponents;
using Checkem.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Checkem.Views
{
    public partial class TodoList : UserControl
    {
        public TodoList()
        {
            InitializeComponent();
        }


        List<ToDoItem> currentInventory = TodoManager.Filter(FilterMethods.Starred);


        private void TodoList_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTodoList();
        }

        private void LoadTodoList()
        {
            foreach (var item in currentInventory)
            {
                Itembar itembar = new Itembar(item);
                itembar.Click += new EventHandler(this.Itembar_Click);

                TodoItemsStackPanel.Children.Add(itembar);
            }
        }

        private void Itembar_Click(object sender, EventArgs e)
        {
            Itembar itembar = sender as Itembar;

            DetailsPanel detailsPanel = new DetailsPanel(itembar);
            detailsPanel.CloceAnimationComplete += new EventHandler(this.DetailsPanel_Click);

            DataGrid.Children.Add(detailsPanel);
        }

        private void DetailsPanel_Click(object sender, EventArgs e)
        {
            DataGrid.Children.RemoveAt(DataGrid.Children.Count - 1);
        }
    }
}
