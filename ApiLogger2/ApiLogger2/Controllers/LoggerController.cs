using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLogger.Function.Contracts;
using ApiLogger.Function.Default;
using ApiLogger.Models.Default;
using ApiLogger.Models.JSON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ApiLogger.Controllers
{
    [Route("Api/[controller]")]
    public class LoggerController : Controller
    {
      
        
        // Register Api/Logger/Register
        [HttpPost("Register")]
        public string Register([FromBody]Member member)
        {

            return "maot ka";
        }

        [HttpPost("Log")]
        public string Log([FromBody]LogJSON log)
        {
            LoggerContract.Log(log.values);
            return null;
        }

        [HttpGet("GetLogs")]
        public string GetLogs()
        {
            var result = LoggerContract.GetLogs();

            return JsonConvert.SerializeObject(result);
        }

    }
}
