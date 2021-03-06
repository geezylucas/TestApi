﻿#nullable disable

namespace ConnectData.Model
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string SecondLastname { get; set; }
        public int? SubAreaId { get; set; }

        public virtual SubArea SubArea { get; set; }
    }
}
