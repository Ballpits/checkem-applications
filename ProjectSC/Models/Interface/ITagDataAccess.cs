using ProjectSC.Models.Object.Tag;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace ProjectSC.Models.Interface
{
    interface ITagDataAccess
    {
        void AddNewTag(List<TagItem> tagInventory, string text, Brush color);

        void RemoveAt(int id, List<TagItem> taginventory);

        void RetrieveTag(ref List<TagItem> taginventory);

        void ResetId(List<TagItem> tagBackUpVal);
    }
}
