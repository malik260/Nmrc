using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class LoginDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? IsActive { get; set; }
        public string? Password { get; set; }
        public Guid RoleId { get; set; }
        public string? Username { get; set; }
        public int UserTypeId { get; set; }
        public bool Logged { get; set; }

        public UserEntity ToEntity()
        {
            return new()
            {
                UserName = Username,
                Password = Password
            };
        }
    }
}