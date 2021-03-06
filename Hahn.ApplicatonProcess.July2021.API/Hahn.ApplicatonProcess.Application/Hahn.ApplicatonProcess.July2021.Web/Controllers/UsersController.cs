using Hahn.ApplicatonProcess.July2021.Domain.Interfaces.ServiceInterface;
using Hahn.ApplicatonProcess.July2021.Domain.VMs;
using Hahn.ApplicatonProcess.July2021.Web.swaggerExample;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hahn.ApplicatonProcess.July2021.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ILogger _logger;
        public UsersController(IUserManager userManager, ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [SwaggerOperation("Gets all the Users' profile details along with Assets")]
        public IEnumerable<UserDto> Get()
        {
            return _userManager.GetUsers();
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Gets User profile for the supplied UserId")]
        [SwaggerResponse(200, "Successfully found the User", typeof(UserDto))]
        [SwaggerResponse(500, "Model validatation fails or unhandled error occured.", typeof(UserDto))]
        [SwaggerResponse(400, "Model data type mismatch might happen.", typeof(UserDto))]
        public IActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid user id");

                _logger.LogInformation("User/Post method fired on {date}", DateTime.Now);
                return Ok(_userManager.GetUser(id));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in User/Get method : {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error in User/Get method - " + e.Message);
            }

        }

        [HttpPost]
        [SwaggerOperation("Creates new User profile.")]
        [SwaggerRequestExample(typeof(UserDto), typeof(UserDtoExample))]
        [SwaggerResponse(200, "Successfully created User profile", typeof(UserDto))]
        [SwaggerResponse(500, "Model validatation fails or unhandled error occured.", typeof(UserDto))]
        [SwaggerResponse(400, "Model data type mismatch might happen.", typeof(UserDto))]
        public ActionResult<UserDto> Post([FromBody] UserDto user)
        {
            try
            {
                _logger.LogInformation("User/Post method fired on {date}", DateTime.Now);
                if (user == null || !ModelState.IsValid || _userManager.IsUserAlreadyExists(user))
                    return BadRequest("Invalid user");

                UserDto result = _userManager.CreateUser(user);
                return Created("/api/User/{id}", result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in User/Post method : {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data - " + e.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Updates the User profile.")]
        [SwaggerResponse(200, "Successfully updated User profile", typeof(UserDto))]
        [SwaggerResponse(500, "Model validatation fails or unhandled error occured.", typeof(UserDto))]
        [SwaggerResponse(400, "Model data type mismatch might happen.", typeof(UserDto))]
        [SwaggerRequestExample(typeof(UserDto), typeof(UserDtoUpdateExample))]
        public ActionResult<UserDto> Put(int id, [FromBody] UserDto user)
        {
            try
            {
                if (user == null || !ModelState.IsValid)
                    return BadRequest("Invalid user");

                _logger.LogInformation("User/Put method fired on {date}", DateTime.Now);
                UserDto result = _userManager.UpdateUser(id, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in User/Put method : {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data - " + e.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Deletes User profile (soft delete).")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid user id");

                _logger.LogInformation("User/Delete method fired on {date}", DateTime.Now);
                _userManager.DeleteUser(id);
                return Ok(StatusCode(200));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in User/Delete method : {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error in deleting data - " + e.Message);
            }

        }
    }
}
