using System;

namespace backend.Dtos.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
    }
}