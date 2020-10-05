using Newtonsoft.Json;
using ProjectSC.Models.Interface;
using ProjectSC.Models.Object.Appearance;
using System.Collections.Generic;
using System.IO;

namespace ProjectSC.Models.AppearanceDataAccess
{
    public class AppearanceDataAccess_Json : IAppearanceDataAccess
    {
        private string path = "Themes.json";

        public List<Themes> Retrieve()
        {
            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<Themes>>(json);
        }
    }
}
