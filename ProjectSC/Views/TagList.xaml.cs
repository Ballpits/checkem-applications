using ProjectSC.Models.DataAccess;
using ProjectSC.Models.Object.Tag;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ProjectSC.Views
{
    public partial class TagList : UserControl
    {
        public TagList()
        {
            InitializeComponent();
        }

        public TagList(ToDoList toDo)
        {
            InitializeComponent();

            ToDoList = toDo;
        }

        #region Variables
        private ToDoList ToDoList;
        private TagDataAccess_Json tagdataAccess = new TagDataAccess_Json();
        #endregion

        public List<TagItem> tagInventory = new List<TagItem>();


        private void ButtonCreateTag_Click(object sender, RoutedEventArgs e)
        {
            ToDoList.OpenTagCreationPanel();
        }


        //Load All The Tag Created At Last Time
        private void TagList_Loaded(object sender, RoutedEventArgs e)
        {
            tagdataAccess.RetrieveTag(ref tagInventory);

            if (tagInventory != null)
            {
                Style style = this.FindResource("TagButton") as Style;

                foreach (var tagItem in tagInventory)
                {
                    Button TagButton = new Button() { Style = style, Content = tagItem.Text, Name = "Tag_" + tagItem.Text };
                    TagButton.Click += new RoutedEventHandler(this.Tag_Click);

                    StpTagList.Children.Add(TagButton);
                }

            }
        }
        private void Tag_Click(object sender, RoutedEventArgs e)
        {
            Button tagbtn = sender as Button;
            ToDoList.TagFilter(tagbtn.Content.ToString());
        }
    }
}
