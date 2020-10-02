using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSC.View.TagPanel
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
