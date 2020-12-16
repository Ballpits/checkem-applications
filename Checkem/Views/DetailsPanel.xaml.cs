using Checkem.CustomComponents;
using Checkem.Models;
using System;
using Sphere.Readable;
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
                //this will prevent user from saving task without title
                if (value != string.Empty)
                {
                    if (todo.Title != value)
                    {
                        todo.Title = value;

                        //update item bar's title text block's text
                        itembar.Update_Title();

                        OnPropertyChanged();
                    }
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

                    itembar.Update_Description();
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

                    //update completion check box's check state in item bar
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

                    //update star toggle's check state in item bar
                    itembar.Update_IsStarred();
                    OnPropertyChanged();
                }
            }
        }

        public string CreationDateTime
        {
            get
            {
                return $"Created on {DateTimeManipulator.SimplifiedDate(todo.CreationDateTime)}";
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
