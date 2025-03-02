using Aqarlist.Core.Models.Dto;

namespace Aqarlist.Core.Services.Service_Interface
{
    public interface IPropertyService 
    {
        PropertyTypeDto[] GetAllCategoryTypes();
        PropertyDto[] GetAllPropertiesByType(int TypeId, PropertySearchFilter searchFilter);
        PropertyByCityDto[] GetPropertyCountByCity(PropertySearchFilter searchFilter);
        bool AddNewProperty(PropertyDto model);
    }
}
