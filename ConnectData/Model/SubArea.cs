using System.Collections.Generic;

#nullable disable

namespace ConnectData.Model
{
    public partial class SubArea
    {
        public SubArea()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? AreaId { get; set; }

        public virtual Area Area { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
