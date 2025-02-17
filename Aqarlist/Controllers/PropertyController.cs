using Aqarlist.Core.Models.Database;
using Aqarlist.Core.Models.Dto;
using Aqarlist.Core.Services.Service_Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aqarlist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }
        [HttpGet("by/type/{propertyTypeId:int}")]
        public ActionResult GetAllPropertiesByType(int propertyTypeId)
        {
            var result = _propertyService.GetAllPropertiesByType(propertyTypeId);
            return Ok(result);
        }
        [HttpGet("types/all")]
        public ActionResult GetAllPropertyTpes()
        {
            var result = _propertyService.GetAllPropertyTypes();
            return Ok(result);
        }
        [HttpGet("numberByCity")]
        public ActionResult GetPropertyCountByCity()
        {
            var result = _propertyService.GetPropertyCountByCity();
            return Ok(result);
        }

        [HttpPost("")]
        public ActionResult AddNewProperty(PropertyDto model) 
        {
            _propertyService.AddNewProperty(model);
            return Ok(true);
        }
    }
}
