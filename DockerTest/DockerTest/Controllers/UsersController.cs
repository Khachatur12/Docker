using System;
using System.Runtime.InteropServices;
using DockerTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace DockerTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(DBService dBService, RedisCacheService redisCacheService) : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<List<User>> Get()
        {
            var cmd = dBService.Connection.CreateCommand();
            cmd.CommandText = "select * from Users";
            var rdr = await cmd.ExecuteReaderAsync();

            var result = new List<User>();
            while(await rdr.ReadAsync())
            {
                result.Add(new User(int.Parse(rdr["id"]?.ToString()), rdr["username"]?.ToString()));
            }
            return result;
        }

        // GET api/values
        [HttpPost]
        public async Task<bool> Add([FromBody] AddUser user)
        {
            var cmd = dBService.Connection.CreateCommand();
            cmd.CommandText = "insert into Users values (@username)";
            cmd.Parameters.AddWithValue("@username", user.Name);
            return (await cmd.ExecuteNonQueryAsync()) > 0;
        }

        [HttpGet("cache/{id}")]
        public async Task<User> GetFromCache([FromRoute] int id)
        {
            return await redisCacheService.GetAsync<User>(id.ToString());
        }

        // GET api/values
        [HttpPost("cache")]
        public async Task<bool> AddCache([FromBody] User user)
        {
            await redisCacheService.SetAsync(user.Id.ToString(), user);
            return true;
        }
    }

    public record AddUser(string Name);
    public record User(int Id, string Name);
}