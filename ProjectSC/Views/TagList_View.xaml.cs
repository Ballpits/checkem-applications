using System.Windows;
using System.Windows.Controls;

namespace ProjectSC.Views
{
    public partial class TagList_View : UserControl
    {
        public TagList_View(ToDoList_View toDo)
        {
            InitializeComponent();

            ToDoList = toDo;
        }

        #region Variables
        private ToDoList_View ToDoList;
        #endregion

        private void ButtonCreateTag_Click(object sender, RoutedEventArgs e)
        {
            ToDoList.OpenTagCreationPanel();
        }
    }
}
