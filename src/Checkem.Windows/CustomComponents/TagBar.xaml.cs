using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using Checkem.Models;
using Checkem.Assets.ValueConverter;


namespace Checkem.Windows.CustomComponents
{
    public partial class TagBar : UserControl
    {
        public TagBar()
        {
            InitializeComponent();

            currentTagList = tagManager.Inventory;
        }

        public event EventHandler OpenPanel;
        public event EventHandler RemoveTag;
        public event EventHandler TagSort;
        public event EventHandler ItemTagRestId;

        List<TagItem> currentTagList;
        TagManager tagManager = new TagManager();

        DrawingColorToBrushConverter DrawingColorToBrushConverter = new DrawingColorToBrushConverter();

        public void Create(string text, Color color)
        {
            #region create new tag function goes here

            Tag tag = new Tag()
            {
                Text = text,
                Color = new SolidColorBrush(color),
                item = new TagItem()
                {
                    TagColor = new System.Drawing.Color(),
                    Content = text,
                    ID = currentTagList.Count
                }
            };


            tag.StateChanged += new EventHandler(this.Tag_StateChange);
            tag.Remove += new EventHandler(this.Tag_Remove);

            StpTagList.Children.Add(tag);
            tagManager.Add(tag.item);
            #endregion
        }

        private void ButtonCreateTag_Click(object sender, RoutedEventArgs e)
        {
            //tell parent control to open panel
            OpenPanel?.Invoke(this, EventArgs.Empty);
        }

        private void Tag_StateChange(object sender, EventArgs e)
        {
            TagSort?.Invoke(sender, EventArgs.Empty);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (currentTagList != null)
            {
                foreach (TagItem item in currentTagList)
                {
                    Tag tag = new Tag()
                    {
                        Text = item.Content,
                        Color = (SolidColorBrush)DrawingColorToBrushConverter.ConvertBack(item.TagColor),
                        item = new TagItem()
                        {
                            TagColor = item.TagColor,
                            ID = item.ID,
                            Content = item.Content
                        }
                    };
                    tag.StateChanged += new EventHandler(Tag_StateChange);
                    tag.Remove += new EventHandler(Tag_Remove);
                    StpTagList.Children.Add(tag);

                }
            }
        }

        private void Tag_Remove(object sender, EventArgs e)
        {

            RemoveTag?.Invoke(sender, EventArgs.Empty);

            Tag tagItem = sender as Tag;

            currentTagList.RemoveAt(tagItem.item.ID);
            tagManager.Remove(tagItem.item);
            tagManager.ResetId();
            ItemTagRestId?.Invoke(tagManager, EventArgs.Empty);
            StpTagList.Children.Clear();
            if (currentTagList != null)
            {
                foreach (TagItem item in currentTagList)
                {
                    Tag tag = new Tag()
                    {
                        Text = item.Content,
                        Color = (SolidColorBrush)DrawingColorToBrushConverter.ConvertBack(item.TagColor),
                        item = new TagItem()
                        {
                            TagColor = item.TagColor,
                            ID = item.ID,
                            Content = item.Content
                        }
                    };
                    tag.StateChanged += new EventHandler(Tag_StateChange);
                    tag.Remove += new EventHandler(Tag_Remove);
                    StpTagList.Children.Add(tag);

                }
            }
        }
    }
}
