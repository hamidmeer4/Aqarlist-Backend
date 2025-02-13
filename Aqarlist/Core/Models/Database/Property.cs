using System.ComponentModel.DataAnnotations;

namespace Aqarlist.Core.Models.Database
{
    public class Property
    {
        public int Id { get; set; }  // Primary Key

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; }  // Nullable Address
        public string? City { get; set; }  // Nullable Address
        public string? ZipCode { get; set; } 
        public string? Country { get; set; }

        public int Price { get; set; }

        // Foreign Key to PropertyType
        public int PropertyTypeId { get; set; }
        public virtual PropertyType PropertyType { get; set; } = null!;  // Navigation Property

        public int NumOfBedrooms { get; set; }

        public int NumOfBathrooms { get; set; }

        public double Size { get; set; }  // Float in SQL corresponds to double in C#

        public string? Description { get; set; }  // Nullable Description
    }
}
