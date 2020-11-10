using Cyclops.Models.DataAccessComponents;
using System.Windows;
using System.Windows.Controls;
using Checkem.CheckemUserControls;

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
                TodoItemsStackPanel.Children.Add(new Itembar(item));
            }
        }
    }
}
