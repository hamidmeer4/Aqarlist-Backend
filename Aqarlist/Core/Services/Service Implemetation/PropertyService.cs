using Aqarlist.Core.Models.Database;
using Aqarlist.Core.Models.Dto;
using Aqarlist.Core.Services.Service_Interface;
using AutoMapper;

namespace Aqarlist.Core.Services.Service_Implemetation
{
    public class PropertyService : IPropertyService
    {
        private readonly ApiDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public PropertyService(ApiDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public PropertyDto[] GetAllPropertiesByType(int TypeId, PropertySearchFilter searchFilter)
        {
            var properties = _db.Property.Where(x => x.PropertyTypeId == TypeId);
            if (!string.IsNullOrWhiteSpace(searchFilter.SearchTerm))
            {
                properties = properties.Where(x => x.Name.Contains(searchFilter.SearchTerm) || (!string.IsNullOrWhiteSpace(x.Description) && x.Description.Contains(searchFilter.SearchTerm)) ||
                                                   (!string.IsNullOrWhiteSpace(x.Country) && x.Country.Contains(searchFilter.SearchTerm)) ||
                                                   (!string.IsNullOrWhiteSpace(x.City) && x.City.Contains(searchFilter.SearchTerm)) ||
                                                   (!string.IsNullOrWhiteSpace(x.Address) && x.Address.Contains(searchFilter.SearchTerm)));
                                                   
            }
            if (searchFilter.MinPriceFilter.HasValue)
            {
                properties.Where(x => x.Price <= searchFilter.MinPriceFilter);
            }
            if(searchFilter.MaxPriceFilter.HasValue)
            {
                properties.Where(x => x.Price >= searchFilter.MaxPriceFilter);
            }
            if(searchFilter.MinSizeFilter.HasValue)
            {
                properties.Where(x => x.Size <= searchFilter.MinSizeFilter);
            }
            if(searchFilter.MaxPriceFilter.HasValue)
            {
                properties.Where(x => x.Size >= searchFilter.MaxPriceFilter);
            }
            foreach (var item in properties)
            {
                item.OwnerOrAgentNotes = string.Empty;
            }
            return _mapper.Map<PropertyDto[]>(properties);
        }

        public PropertyTypeDto[] GetAllCategoryTypes()
        {
            var types = _db.PropertyType.ToArray();
            return _mapper.Map<PropertyTypeDto[]>(types);
        }

        public PropertyByCityDto[] GetPropertyCountByCity(PropertySearchFilter searchFilter)
        {
            var query = _db.Property.Where(x => !string.IsNullOrEmpty(x.City));
            if (!string.IsNullOrWhiteSpace(searchFilter.SearchTerm))
            {
                query = query.Where(x =>!string.IsNullOrWhiteSpace(x.City) && x.City.Contains(searchFilter.SearchTerm));
            }
            if (query is null) return new PropertyByCityDto[] { };  
             var toReturn = query.GroupBy(x => x.City)
                  .Select(x => new PropertyByCityDto
                  {
                      City = x.Key,
                      NumOfProperties = x.Count()
                  });
            return toReturn.ToArray();
        }
        public bool AddNewProperty(PropertyDto model)
        {
            List<int> attachmentIds = new List<int>();
            if (model.Attachments != null && model.Attachments.Length != 0)
            {
                foreach (var item in model.Attachments)
                {
                    var attachment = _fileService.UploadFileAsync(item, model.Id);
                    attachmentIds.Add(attachment.Id);
                }
            }
            model.MainAttachmentId = attachmentIds[0];
            var data = _mapper.Map<Property>(model);
            _db.Property.Add(data);
            _db.SaveChanges();
            return true;
        }
    }
}
