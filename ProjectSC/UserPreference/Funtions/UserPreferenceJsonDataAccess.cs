using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSC.UserPreference.Funtions
{
    class UserPreferenceJsonDataAccess
    {
        public static void SaveSettings(UPref uPref)
        {
            File.WriteAllText("UPref.json", JsonConvert.SerializeObject(uPref));
        }

        public void RetrieveSettings(ref UPref uPref)
        {
            string json = File.ReadAllText("UPref.json");

            uPref = JsonConvert.DeserializeObject<UPref>(json);
        }//Get all the data from the json file and save it to the list
    }
}
