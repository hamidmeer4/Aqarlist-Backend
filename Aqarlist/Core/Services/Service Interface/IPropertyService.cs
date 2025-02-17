using Aqarlist.Core.Models.Dto;

namespace Aqarlist.Core.Services.Service_Interface
{
    public interface IPropertyService 
    {
        PropertyTypeDto[] GetAllPropertyTypes();
        PropertyDto[] GetAllPropertiesByType(int TypeId);
        PropertyByCityDto[] GetPropertyCountByCity();
        bool AddNewProperty(PropertyDto model);
    }
}
