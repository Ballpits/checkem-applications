using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSC.UserPreference
{
    class UPref
    {
        public bool IsNotificationOn { get; set; }


        public int StyleCode { get; set; }

        public string PrimaryColor { get; set; }

        public string SecondaryColor{ get; set; }

        public bool IsDarkMode { get; set; }
    }
}
