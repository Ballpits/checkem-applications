﻿using Checkem.CustomComponents;
using Checkem.Models;
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
            DataContext = this;

            InitializeComponent();
        }

        public DetailsPanel(Itembar itemar)
        {
            DataContext = this;

            ItemProperties = itemar.todo;

            InitializeComponent();
        }


        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Close;
        #endregion


        #region Variable
        public Todo ItemProperties;
        #endregion


        #region Property
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
        #endregion


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private void StoryBoard_Completed(object sender, EventArgs e)
        {
            Close?.Invoke(this, EventArgs.Empty);
        }
    }
}
