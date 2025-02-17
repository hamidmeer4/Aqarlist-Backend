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
        public PropertyService(ApiDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public PropertyDto[] GetAllPropertiesByType(int TypeId)
        {
            var properties = _db.Property.Where(x => x.PropertyTypeId == TypeId);
            foreach (var item in properties)
            {
                item.OwnerOrAgentNotes = string.Empty;
            }
            return _mapper.Map<PropertyDto[]>(properties);
        }

        public PropertyTypeDto[] GetAllPropertyTypes()
        {
            var types = _db.PropertyType.ToArray();
            return _mapper.Map<PropertyTypeDto[]>(types);
        }

        public PropertyByCityDto[] GetPropertyCountByCity()
        {
            var query = _db.Property.Where(x=> !string.IsNullOrEmpty(x.City)).GroupBy(x => x.City)
                                    .Select(x => new PropertyByCityDto
                                            {
                                                City = x.Key,
                                                NumOfProperties = x.Count()
                                            });
            return query.ToArray();
        }
        public bool AddNewProperty(PropertyDto model)
        {
            var data = _mapper.Map<Property>(model);
            _db.Property.Add(data);
            _db.SaveChanges();
            return true;
        }
    }
}
