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

namespace ProjectSC.View
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
