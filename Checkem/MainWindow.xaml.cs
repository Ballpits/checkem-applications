using System;
using Checkem.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Checkem.Models;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Windows.Media.Animation;

namespace Checkem
{
    public partial class MainWindow : Window
    {
        public MainWindow(App app)
        {
            application = app;

            InitializeComponent();
        }


        #region Variables
        App application;

        //timer for notifications
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        //fix time offset
        private int TimerOffset = 60 - DateTime.Now.Second;

        TodoManager todoManager = new TodoManager();
        #endregion


        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
        }


        public void Reload()
        {
            todoList.Reload();
        }


        #region Notification Timer
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Notification && !Properties.Settings.Default.VacationMode)
            {
                TimerOffset = 60 - DateTime.Now.Second;

                //Fix timer delay time
                dispatcherTimer.Interval = TimeSpan.FromSeconds(TimerOffset);


                //Check if the begin or end time is matched with the current time
                foreach (var item in todoManager.Filter(FilterMethods.TaskToday))
                {
                    switch (item.ReminderState)
                    {
                        case ReminderState.Basic:
                            {
                                if (DateTime.Now.Year == item.EndDateTime.Value.Year && DateTime.Now.Month == item.EndDateTime.Value.Month && DateTime.Now.Hour == item.EndDateTime.Value.Hour && DateTime.Now.Minute == item.EndDateTime.Value.Minute)
                                {
                                    application.Notify(item.Title, item.Description);
                                }

                                break;
                            }
                        case ReminderState.Advance:
                            {
                                if (DateTime.Now.Hour == item.BeginDateTime.Value.Hour && DateTime.Now.Minute == item.BeginDateTime.Value.Minute)
                                {
                                    application.Notify(item.Title, item.Description);
                                }
                                else
                                {
                                    if (DateTime.Now.Year == item.EndDateTime.Value.Year && DateTime.Now.Month == item.EndDateTime.Value.Month && DateTime.Now.Hour == item.EndDateTime.Value.Hour && DateTime.Now.Minute == item.EndDateTime.Value.Minute)
                                    {
                                        application.Notify(item.Title, item.Description);
                                    }
                                }

                                break;
                            }
                        default:
                            break;
                    }
                }
            }
        }
        #endregion



        #region Window chrome buttons
        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow(this);
            settings.Owner = this;
            settings.Closed += Settings_Closed;

            settings.Show();

            BlockerGrid.Visibility = Visibility.Visible;
        }

        private void Settings_Closed(object sender, EventArgs e)
        {
            BlockerGrid.Visibility = Visibility.Hidden;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion



        #region Resize and state changed events
        private void AppWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                TrainsitioningContent.Margin = new Thickness(7);
            }
            else
            {
                TrainsitioningContent.Margin = new Thickness(0);
            }
        }
        #endregion


        public FilterMethods filterMethod { get; set; } = FilterMethods.None;


        #region Task bar button events
        private void NewTaskButton_Click(object sender, RoutedEventArgs e)
        {
            todoList.NewTaskButton_Click(sender, e);
        }

        private void SortByStarButton_Click(object sender, RoutedEventArgs e)
        {
            todoList.SortByStarButton_Click(sender, e);
        }

        private void SortByDueDateButton_Click(object sender, RoutedEventArgs e)
        {
            todoList.SortByDueDateButton_Click(sender, e);
        }

        private void SortByAlphabeticalAscendingButton_Click(object sender, RoutedEventArgs e)
        {
            todoList.SortByAlphabeticalAscendingButton_Click(sender, e);
        }

        private void SortByAlphabeticalDescendingButton_Click(object sender, RoutedEventArgs e)
        {
            todoList.SortByAlphabeticalDescendingButton_Click(sender, e);
        }

        private void SortByCreationDateButton_Click(object sender, RoutedEventArgs e)
        {
            todoList.SortByCreationDateButton_Click(sender, e);
        }
        #endregion



        #region Navigation bar
        #region Expander buttons

        private void ButtonExpand_Click(object sender, RoutedEventArgs e)
        {
            ButtonCollaps.Visibility = Visibility.Visible;
            ButtonExpand.Visibility = Visibility.Collapsed;
        }


        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCollaps.Visibility = Visibility.Collapsed;
            ButtonExpand.Visibility = Visibility.Visible;
        }

        #endregion

        #region Buttons

        private void ButtonMyDay_Click(object sender, RoutedEventArgs e)
        {
            filterMethod = FilterMethods.None;
            todoList.ListName = this.FindResource("Dict_MyDay") as string;

            this.Dispatcher.Invoke(Filter);
        }


        private void ButtonAllTasks_Click(object sender, RoutedEventArgs e)
        {
            filterMethod = FilterMethods.None;
            todoList.ListName = this.FindResource("Dict_AllItems") as string;

            this.Dispatcher.Invoke(Filter);
        }


        private void ButtonDueDateFilter_Click(object sender, RoutedEventArgs e)
        {
            filterMethod = FilterMethods.Planned;
            todoList.ListName = this.FindResource("Dict_Reminder") as string;

            this.Dispatcher.Invoke(Filter);
        }


        private void ButtonStarredFilter_Click(object sender, RoutedEventArgs e)
        {
            filterMethod = FilterMethods.Starred;
            todoList.ListName = this.FindResource("Dict_Starred") as string;

            this.Dispatcher.Invoke(Filter);
        }


        private void ButtonAddNewList_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void Filter()
        {
            Grid.SetRow(NavbarCursor, (int)filterMethod + 1);
            todoList.filterMethod = this.filterMethod;
        }

        #endregion



        #region Searchbox
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            todoList.Search(SearchBox.Text);
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Focus();
            SearchBox.SelectAll();
        }
        #endregion



        #region Hotkeys

        bool IsLCtrlPressed = false;
        bool IsFPressed = false;

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                IsLCtrlPressed = true;
            }

            if (e.Key == Key.F)
            {
                IsFPressed = true;
            }

            if (IsLCtrlPressed && IsFPressed)
            {
                Storyboard storyboard = this.FindResource("SearchBox_Open") as Storyboard;
                storyboard.Begin();

                SearchBox.Focus();
            }

            if (e.Key == Key.F11)
            {
                if (this.WindowStyle == WindowStyle.None)
                {
                    this.WindowStyle = WindowStyle.SingleBorderWindow;
                    this.WindowState = WindowState.Maximized;
                    TrainsitioningContent.Margin = new Thickness(7);
                }
                else
                {
                    this.WindowStyle = WindowStyle.None;
                    this.WindowState = WindowState.Minimized;
                    this.WindowState = WindowState.Maximized;
                    TrainsitioningContent.Margin = new Thickness(6);
                }
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                IsLCtrlPressed = false;
            }

            if (e.Key == Key.F)
            {
                IsFPressed = false;
            }
        }

        #endregion

        private void OpenCalendarButton_Click(object sender, RoutedEventArgs e)
        {
            calendar.Visibility = Visibility.Visible;

            OpenCalendarButton.Visibility = Visibility.Collapsed;
            CloseCalendarButton.Visibility = Visibility.Visible;

            AddButton.Visibility = Visibility.Collapsed;
            SearchButton.Visibility = Visibility.Collapsed;
            SortButton.Visibility = Visibility.Collapsed;
        }

        private void CloseCalendarButton_Click(object sender, RoutedEventArgs e)
        {
            calendar.Visibility = Visibility.Hidden;

            OpenCalendarButton.Visibility = Visibility.Visible;
            CloseCalendarButton.Visibility = Visibility.Collapsed;

            AddButton.Visibility = Visibility.Visible;
            SearchButton.Visibility = Visibility.Visible;
            SortButton.Visibility = Visibility.Visible;
        }
    }
}
