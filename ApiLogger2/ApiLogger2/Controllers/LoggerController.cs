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
using Microsoft.AspNetCore.Cors;

namespace ApiLogger.Controllers
{
    [Route("Api/[controller]")]
    public class LoggerController : Controller
    {    
        
        // Register Api/Logger/[METHOD]

        [HttpPost("Log")]
        public string Log([FromBody]LogJSON log)
        {
            LoggerContract.Log(log);
            return null;
        }

        [HttpGet("GetLogs")]
        public string GetLogs([FromBody]DeviceJSON _deviceID)
        {
            var result = LoggerContract.GetLogs(_deviceID.deviceID);

            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET");
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");


            return JsonConvert.SerializeObject(result);


        }

    }
}
