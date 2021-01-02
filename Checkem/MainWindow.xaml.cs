using System;
using Checkem.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Checkem.Models;
using System.Windows.Threading;
using System.Collections.Generic;

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



        #region Window chrome buttons
        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
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

            CheckWidth();
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CheckWidth();
        }

        private void CheckWidth()
        {
            if (this.WindowState == WindowState.Maximized || this.Width >= 1400)
            {
                SearchBoxBorder.HorizontalAlignment = HorizontalAlignment.Center;
            }
            else
            {
                SearchBoxBorder.HorizontalAlignment = HorizontalAlignment.Left;
            }
        }
        #endregion


        public FilterMethods filterMethod { get; set; } = FilterMethods.None;


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
            this.Dispatcher.Invoke(Filter);
        }


        private void ButtonAllTasks_Click(object sender, RoutedEventArgs e)
        {
            filterMethod = FilterMethods.None;
            this.Dispatcher.Invoke(Filter);
        }


        private void ButtonDueDateFilter_Click(object sender, RoutedEventArgs e)
        {
            filterMethod = FilterMethods.Planned;
            this.Dispatcher.Invoke(Filter);
        }


        private void ButtonStarredFilter_Click(object sender, RoutedEventArgs e)
        {
            filterMethod = FilterMethods.Starred;
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

        }

        private void ButtonClearSearchBox_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
