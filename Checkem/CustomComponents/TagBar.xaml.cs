using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using Checkem.Models;


namespace Checkem.CustomComponents
{
    public partial class TagBar : UserControl
    {
        public TagBar()
        {
            InitializeComponent();

            currentTagList =tagManager.Inventory;
        }

        public event EventHandler OpenPanel;

        List<TagItem> currentTagList;
        TagManager tagManager = new TagManager();

        public void Create(string text, Color color)
        {
            #region create new tag function goes here
            

            Tag tag = new Tag()
            {
                Text = text,
                Color = new SolidColorBrush(color)
            };
            TagItem tagItem = new TagItem()
            {
                TagColor = new SolidColorBrush(color),
                Content = text,
                ID = currentTagList.Count
            };

            tag.StateChanged += new EventHandler(this.Tag_StateChange);

            currentTagList.Add(tagItem);
            tagManager.Add(tagItem);
            StpTagList.Children.Add(tag);
            #endregion
        }

        private void ButtonCreateTag_Click(object sender, RoutedEventArgs e)
        {
            //tell parent control to open panel
            OpenPanel?.Invoke(this, EventArgs.Empty);
        }

        private void Tag_StateChange(object sender, EventArgs e)
        {
            Tag tag = sender as Tag;
            System.Windows.Forms.MessageBox.Show($"Tag.IsSelected = {tag.IsSelected}");
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (currentTagList != null)
            {
                foreach (TagItem item in currentTagList)
                {                   
                    StpTagList.Children.Add(new Tag() { Text = item.Content, Color = item.TagColor });
                }
            }
        }
    }
}
