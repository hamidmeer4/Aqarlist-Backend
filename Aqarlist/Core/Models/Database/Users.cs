using System.ComponentModel.DataAnnotations.Schema;

namespace Aqarlist.Core.Models.Database
{
    [Table("Users")]
    public class Users
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int RoleId { get; set; }
        public long? NationalId{ get; set; }
        public string Password { get; set; } = string.Empty;
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
       // public virtual Roles Role { get; set; } = new Roles();
    }
}
