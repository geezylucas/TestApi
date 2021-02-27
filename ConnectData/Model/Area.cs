using System.Collections.Generic;

#nullable disable

namespace ConnectData.Model
{
    public partial class Area
    {
        public Area()
        {
            SubAreas = new HashSet<SubArea>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SubArea> SubAreas { get; set; }
    }
}
