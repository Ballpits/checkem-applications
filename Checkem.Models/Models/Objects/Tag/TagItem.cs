using System.Windows.Media;

namespace Checkem.Models
{
    public class TagItem
    {
        public int ID { get; set; }

        public string Content { get; set; }

<<<<<<< Updated upstream:Checkem.Models/Models/Objects/Tag/TagItem.cs
        public SolidColorBrush TagColor { get; set; }
=======
        public Color Color { get; set; }
>>>>>>> Stashed changes:src/Checkem.Models/Models/Objects/Tag/TagItem.cs
    }
}
