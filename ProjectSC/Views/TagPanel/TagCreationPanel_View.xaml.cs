using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectSC.Views.TagPanel
{
    public partial class TagCreationPanel_View : UserControl
    {
        public TagCreationPanel_View(ToDoList_View toDo)
        {
            InitializeComponent();

            ToDoList = toDo;
        }

        ToDoList_View ToDoList;

        private void DarkGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed || Mouse.XButton1 == MouseButtonState.Pressed)
            {
                ToDoList.CloseTagCreationPanel();
            }
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            //Add tag to StpTagList
        }
    }
}
