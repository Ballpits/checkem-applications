namespace Sphere.DateTime
{
    public static class DateTimeCalculator
    {
        public static int PreviousMonth
        {
            get
            {
                return GetPreviousMonth(System.DateTime.Now.Month);
            }
        }

        public static int GetPreviousMonth(int month)
        {
            if (month == 1)
            {
                return 12;
            }
            else
            {
                return month - 1;
            }
        }
    }
}
