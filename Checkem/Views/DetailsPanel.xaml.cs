using Cyclops.Models.Objects;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Checkem.Views
{
    public partial class DetailsPanel : UserControl, INotifyPropertyChanged
    {
        public DetailsPanel()
        {
            InitializeComponent();
        }

        public DetailsPanel(ToDoItem toDoItem)
        {
            ItemProperties = toDoItem;

            InitializeComponent();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CloceAnimationComplete;


        public ToDoItem ItemProperties;

        public string Title
        {
            get
            {
                return ItemProperties.Title;
            }
            set
            {
                if (ItemProperties.Title != value)
                {
                    ItemProperties.Title = value;

                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get
            {
                return ItemProperties.Description;
            }
            set
            {
                if (ItemProperties.Description != value)
                {
                    ItemProperties.Description = value;

                    OnPropertyChanged();
                }
            }
        }

        private void userControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void StoryBoard_Completed(object sender, EventArgs e)
        {
            CloceAnimationComplete?.Invoke(this, EventArgs.Empty);
        }
    }
}
