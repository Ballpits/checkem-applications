using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjectSC.Models.Object.Tag
{
    public class TagItem
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public Brush TagColor { get; set; }
    }
}
