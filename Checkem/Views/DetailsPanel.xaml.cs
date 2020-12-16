using Checkem.CustomComponents;
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
            this.DataContext = this;

            InitializeComponent();
        }

        public DetailsPanel(Itembar itembar)
        {
            this.DataContext = this;

            //copy item bar
            this.itembar = itembar;

            //get item bar's todo properties
            this.todo = itembar.todo;

            InitializeComponent();
        }


        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Close;
        #endregion


        #region Variable
        public Itembar itembar;
        #endregion


        #region Property
        public Todo todo = new Todo();

        public string Title
        {
            get
            {
                return todo.Title;
            }
            set
            {
                if (todo.Title != value)
                {
                    todo.Title = value;

                    itembar.Update_Title();
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get
            {
                return todo.Description;
            }
            set
            {
                if (todo.Description != value)
                {
                    todo.Description = value;

                    OnPropertyChanged();
                }
            }
        }

        public bool IsCompleted
        {
            get
            {
                return todo.IsCompleted;
            }
            set
            {
                if (todo.IsCompleted != value)
                {
                    todo.IsCompleted = value;

                    itembar.Update_IsCompleted();
                    OnPropertyChanged();
                }
            }
        }

        public bool IsStarred
        {
            get
            {
                return todo.IsStarred;
            }
            set
            {
                if (todo.IsStarred != value)
                {
                    todo.IsStarred = value;

                    itembar.Update_IsStarred();
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
