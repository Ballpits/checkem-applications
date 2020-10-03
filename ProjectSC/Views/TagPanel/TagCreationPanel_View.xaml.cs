using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectSC.Views.TagPanel
{
    public partial class TagCreationPanel_View : UserControl
    {
        public TagCreationPanel_View(ToDoList_View toDo,TagList_View tagList_View)
        {
            InitializeComponent();

            ToDoList = toDo;
            TagList = tagList_View;
        }

        ToDoList_View ToDoList;
        TagList_View TagList;


        private void DarkGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed || Mouse.XButton1 == MouseButtonState.Pressed)
            {
                ToDoList.CloseTagCreationPanel();
            }
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            Style style = this.FindResource("TagButton") as Style;
            TagList.StpTagList.Children.Add(new Button() { Style=style,Content = TextboxTagName.Text});
            ToDoList.CloseTagCreationPanel();
        }
    }
}
