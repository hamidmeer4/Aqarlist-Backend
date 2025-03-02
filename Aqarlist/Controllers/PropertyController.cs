﻿using Aqarlist.Core.Models.Database;
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
        [HttpGet("by/category/{propertyTypeId:int}")]
        public ActionResult GetAllPropertiesByType(int propertyTypeId,PropertySearchFilter searchFilter)
        {
            var result = _propertyService.GetAllPropertiesByType(propertyTypeId, searchFilter);
            return Ok(result);
        }
        [HttpGet("category/all")]
        public ActionResult GetAllCategoryTpes()
        {
            var result = _propertyService.GetAllCategoryTypes();
            return Ok(result);
        }
        [HttpGet("numberByCity")]
        public ActionResult GetPropertyCountByCity(PropertySearchFilter searchFilter)
        {
            var result = _propertyService.GetPropertyCountByCity(searchFilter);
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
