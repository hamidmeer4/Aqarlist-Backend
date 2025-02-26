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
        public string? Category { get; set; }
        public string? ListedIn { get; set; }
        public string? Status { get; set; }
        public double? YearlyTaxRate { get; set; }
        public string? AfterPriceLabel { get; set; }
        public string? VideoFrom { get; set; }
        public string? EmbedVideoId { get; set; }
        public string? Neighbourhood { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? LotSize { get; set; }
        public int NumOfRooms { get; set; }
        public string? CustomId { get; set; }
        public bool HasGarage { get; set; } = false;
        public double? GarageSize { get; set; }
        public int? YearBuilt { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public bool HasBasement { get; set; } = false;
        public int? NumOfFloors { get; set; }
        public string? Roofing { get; set; }
        public string? ExteriorMaterial { get; set; }
        public string? StructureType { get; set; }
        public int? FloorNumber { get; set; }
        public string? OwnerOrAgentNotes { get; set; }
        public string Amenities { get; set; } = string.Empty;
        public IFormFile[]? Attachments { get; set; }
        public int? MainAttachmentId { get; set; }
    }
}
