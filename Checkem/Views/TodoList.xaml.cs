using Cyclops.Models.DataAccessComponents;
using System.Windows;
using System.Windows.Controls;

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
            foreach (var item in dataAccess.GetInventory())
            {
                TodoItemsStackPanel.Children.Add(new Itembar() { itemProperties = item});
            }
        }
    }
}
