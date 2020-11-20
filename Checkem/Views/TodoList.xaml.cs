using Cyclops.Models.DataAccessComponents;
using System.Windows;
using System.Windows.Controls;
using Checkem.CustomComponents;
using System;

namespace Checkem.Views
{
    public partial class TodoList : UserControl
    {
        DataAccess dataAccess = new DataAccess();

        public TodoList()
        {
            InitializeComponent();
        }


        private void TodoListUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTodoList();
        }

        private void LoadTodoList()
        {
            foreach (var item in dataAccess.Inventory())
            {
                Itembar itembar = new Itembar(item);
                itembar.Click += new EventHandler(this.Itembar_Click);
                TodoItemsStackPanel.Children.Add(itembar);
            }
        }

        private void Itembar_Click(object sender, EventArgs e)
        {
            Itembar itembar = sender as Itembar;

            DetailsPanel detailsPanel = new DetailsPanel(itembar.ItemProperties);
            detailsPanel.CloceAnimationComplete += new EventHandler(this.DetailsPanel_Click);

            DataGrid.Children.Add(detailsPanel);
        }

        private void DetailsPanel_Click(object sender, EventArgs e)
        {
            DataGrid.Children.RemoveAt(DataGrid.Children.Count - 1);
        }
    }
}
