using Newtonsoft.Json;
using ProjectSC.Models.Interface;
using ProjectSC.Models.Object.Tag;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace ProjectSC.Models.DataAccess
{
    public class TagDataAccess_Json : ITagDataAccess
    {
        private string path = "TagRecord.json";
        private void SaveToJson(List<TagItem> tagInventory)
        {
            File.WriteAllText(@"TagRecord.json", JsonConvert.SerializeObject(tagInventory));
        }

        public void AddNewTag(List<TagItem> tagInventory, string text, Brush color)
        {
            int id;
            if (tagInventory != null)
                id = tagInventory.Count;
            else
                id = 0;

            tagInventory.Add(new TagItem
            {
                Id = id,
                Text = text,
                TagColor = color
            });
            SaveToJson(tagInventory);
        }
        public void RemoveAt(int id, List<TagItem> taginventory)
        {
            taginventory.RemoveAt(taginventory.FindIndex(x => x.Id == id));
            SaveToJson(taginventory);
        }
        public void RetrieveTag(ref List<TagItem> taginventory)
        {
            string json = File.ReadAllText(path);

            taginventory = JsonConvert.DeserializeObject<List<TagItem>>(json);
        }
        public void ResetId(List<TagItem> tagBackUpVal)
        {
            foreach (var item in tagBackUpVal)
            {
                item.Id = tagBackUpVal.IndexOf(item);
            }

            SaveToJson(tagBackUpVal);
        }
    }
}
