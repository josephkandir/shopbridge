using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopbridge.api.Models;
using shopbridge.api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopbridge.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly IUser _userRepo;

        public UsersController(IUser userRepo)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// Get User Detail List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                var users = await _userRepo.GetAsync();

                if (users.Count() == 0)
                    return NotFound("User hasn't added any data");

                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Some error has occured!");
            }
        }

        /// <summary>
        /// Get User Detail by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<User> GetUsers(int id)
        {
            try
            {
                var user = _userRepo.GetAsync(id);

                if (user == null)
                    return NotFound($"Data doesn't exist for the given Id {id}");

                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
