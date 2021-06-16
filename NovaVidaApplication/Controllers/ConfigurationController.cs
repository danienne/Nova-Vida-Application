using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace NovaVidaApplication.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IConfiguration configuration;

        public ConfigurationController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public ActionResult GetConfigurationValue(string sectionName)
        {
            var parameterValue = configuration[$"{sectionName}"];
            return Json(new { parameter = parameterValue });
        }
    }
}
