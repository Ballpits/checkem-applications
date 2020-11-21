using System;

namespace Visual
{
    public static class DateTimeManipulator
    {
        //Return shortened date time
        public static string SimplifiedDate(DateTime dateTime)
        {
            if (dateTime.Year == DateTime.Now.Year && dateTime.Month == DateTime.Now.Month && dateTime.Day == DateTime.Now.Day)
            {
                return "Today, " + dateTime.ToString("hh:mm tt");
            }
            else if (dateTime.Year == DateTime.Now.Year && dateTime.Month == DateTime.Now.Month && dateTime.Day == DateTime.Now.Day + 1)
            {
                return "Tomorrow, " + dateTime.ToString("hh:mm tt");
            }
            else if (dateTime.Year == DateTime.Now.Year && dateTime.Month == DateTime.Now.Month && dateTime.Day == DateTime.Now.Day - 1)
            {
                return "Yesterday, " + dateTime.ToString("hh:mm tt");
            }
            else
            {
                return dateTime.ToString("yyyy/MM/dd, hh:mm tt");
            }
        }


        //Check if the date time is passed
        public static bool IsPassed(DateTime dateTime)
        {
            if (dateTime < DateTime.Now)
            {
                //return true if it's passed
                return true;
            }
            else
            {
                //return false if it's not passed
                return false;
            }
        }
    }
}
