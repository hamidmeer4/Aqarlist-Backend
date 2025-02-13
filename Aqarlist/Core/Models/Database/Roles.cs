using System.ComponentModel.DataAnnotations.Schema;

namespace Aqarlist.Core.Models.Database
{
    [Table("Roles")]
    public class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
