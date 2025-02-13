using Aqarlist.Core.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace Aqarlist.Core.Models.Dto
{
    public class PropertyDto
    {
        public int Id { get; set; }  // Primary Key
      
        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; }  // Nullable Address
        public string? City { get; set; }  // Nullable Address
        public string? ZipCode { get; set; }
        public string? Country { get; set; }

        public int Price { get; set; }

        public int PropertyTypeId { get; set; }
        public int NumOfBedrooms { get; set; }

        public int NumOfBathrooms { get; set; }

        public double Size { get; set; }  // Float in SQL corresponds to double in C#

        public string? Description { get; set; }  // Nullable Description
    }
}
