namespace BusinessLogic.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string SecondLastname { get; set; }
        public int? SubAreaId { get; set; }
        public string SubAreaName { get; set; }
    }
}
