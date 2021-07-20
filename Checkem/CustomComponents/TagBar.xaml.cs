using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using Checkem.Models;


namespace Checkem.Windows.CustomComponents
{
    public partial class TagBar : UserControl
    {
        public TagBar()
        {
            InitializeComponent();

            TagItems = tagManager.Inventory;
        }


        #region Events
        public event EventHandler OpenPanel;
        public event EventHandler RemoveTag;
        public event EventHandler TagSort;
        public event EventHandler ItemTagRestId;
        #endregion


        List<TagItem> TagItems;
        TagManager tagManager = new TagManager();

<<<<<<< Updated upstream:Checkem/CustomComponents/TagBar.xaml.cs
=======
        DrawingColorToBrushConverter DrawingColorToBrushConverter = new DrawingColorToBrushConverter();


>>>>>>> Stashed changes:src/Checkem.Windows/CustomComponents/TagBar.xaml.cs
        public void Create(string text, Color color)
        {
            // Create a new tag control
            Tag tag = new Tag()
            {
                Text = text,
                Color = new SolidColorBrush(color),
                tagItem = new TagItem()
                {
<<<<<<< Updated upstream:Checkem/CustomComponents/TagBar.xaml.cs
                    TagColor = new SolidColorBrush(color),
=======
                    Color = (System.Drawing.Color)DrawingColorToBrushConverter.ConvertBack(color),
>>>>>>> Stashed changes:src/Checkem.Windows/CustomComponents/TagBar.xaml.cs
                    Content = text,
                    ID = TagItems.Count
                }
            };


            tag.StateChanged += new EventHandler(this.Tag_StateChange);
            tag.Remove += new EventHandler(this.Tag_Remove);

            StpTagList.Children.Add(tag);
<<<<<<< Updated upstream:Checkem/CustomComponents/TagBar.xaml.cs
            tagManager.Add(tag.tagItem);
            #endregion
=======
            tagManager.Add(tag.item);
>>>>>>> Stashed changes:src/Checkem.Windows/CustomComponents/TagBar.xaml.cs
        }

        private void ButtonCreateTag_Click(object sender, RoutedEventArgs e)
        {
            // Tell the parent control to open tag creation panel
            OpenPanel?.Invoke(this, EventArgs.Empty);
        }

        private void Tag_StateChange(object sender, EventArgs e)
        {
            TagSort?.Invoke(sender, EventArgs.Empty);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (TagItems != null)
            {
                foreach (TagItem item in TagItems)
                {
                    Tag tag = new Tag(item)
                    {
                        Text = item.Content,
<<<<<<< Updated upstream:Checkem/CustomComponents/TagBar.xaml.cs
                        Color = item.TagColor,
                        tagItem = new TagItem()
                        {
                            TagColor = item.TagColor,
                            ID = item.ID,
                            Content = item.Content
                        }
=======
                        Color = (SolidColorBrush)DrawingColorToBrushConverter.Convert(item.Color)
>>>>>>> Stashed changes:src/Checkem.Windows/CustomComponents/TagBar.xaml.cs
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

<<<<<<< Updated upstream:Checkem/CustomComponents/TagBar.xaml.cs
            currentTagList.RemoveAt(tagItem.tagItem.ID);
            tagManager.Remove(tagItem.tagItem);
=======
            TagItems.RemoveAt(tagItem.item.ID);
            tagManager.Remove(tagItem.item);
>>>>>>> Stashed changes:src/Checkem.Windows/CustomComponents/TagBar.xaml.cs
            tagManager.ResetId();
            ItemTagRestId?.Invoke(tagManager, EventArgs.Empty);
            StpTagList.Children.Clear();

            if (TagItems != null)
            {
                foreach (TagItem item in TagItems)
                {
                    Tag tag = new Tag(item)
                    {
                        Text = item.Content,
<<<<<<< Updated upstream:Checkem/CustomComponents/TagBar.xaml.cs
                        Color = item.TagColor,
                        tagItem = new TagItem()
                        {
                            TagColor = item.TagColor,
                            ID = item.ID,
                            Content = item.Content
                        }
=======
                        Color = (SolidColorBrush)DrawingColorToBrushConverter.Convert(item.Color)
>>>>>>> Stashed changes:src/Checkem.Windows/CustomComponents/TagBar.xaml.cs
                    };

                    tag.StateChanged += new EventHandler(Tag_StateChange);
                    tag.Remove += new EventHandler(Tag_Remove);

                    StpTagList.Children.Add(tag);
                }
            }
        }
    }
}
