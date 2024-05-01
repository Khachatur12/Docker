using System;
using System.Runtime.InteropServices;
using DockerTest.DB;
using Microsoft.AspNetCore.Mvc;

namespace DockerTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(DBService dBService) : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<List<string>> Get()
        {
            var cmd = dBService.Connection.CreateCommand();
            cmd.CommandText = "select username from Users";
            var rdr = cmd.ExecuteReader();

            var result = new List<string>();
            while(await rdr.ReadAsync())
            {
                result.Add(rdr["username"]?.ToString());
            }
            return result;
        }
    }
}