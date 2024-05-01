using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace DockerTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return $"Version: {Environment.OSVersion}; Description: {RuntimeInformation.OSDescription}";
        }
    }
}
