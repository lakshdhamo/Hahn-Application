using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces.ServiceInterface;
using Hahn.ApplicatonProcess.July2021.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Hahn.ApplicatonProcess.July2021.Domain.VMs;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Hahn.ApplicatonProcess.July2021.Web.swaggerExample;
using Swashbuckle.AspNetCore.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hahn.ApplicatonProcess.July2021.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ILogger _logger;
        public UserController(IUserManager userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/<UserController>
        [HttpGet]
        [SwaggerOperation("Gets all the Users' profile details along with Assets")]
        public IEnumerable<UserVm> Get()
        {
            return _userManager.GetUsers();
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Gets User profile for the supplied UserId")]
        [SwaggerResponse(200, "Successfully found the User", typeof(UserVm))]
        [SwaggerResponse(500, "Model validatation fails or unhandled error occured.", typeof(UserVm))]
        [SwaggerResponse(400, "Model data type mismatch might happen.", typeof(UserVm))]
        public UserVm Get(int id)
        {
            return _userManager.GetUser(id);
        }

        // POST api/<UserController>
        [HttpPost]
        [SwaggerOperation("Creates new User profile.")]
        [SwaggerRequestExample(typeof(UserVm), typeof(UserVmExample))]
        [SwaggerResponse(200, "Successfully created User profile", typeof(UserVm))]
        [SwaggerResponse(500, "Model validatation fails or unhandled error occured.", typeof(UserVm))]
        [SwaggerResponse(400, "Model data type mismatch might happen.", typeof(UserVm))]
        public IActionResult Post([FromBody] UserVm user)
        {
            try
            {
                _logger.LogInformation("User/Post method fired on {date}", DateTime.Now);
                if (user == null || !ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, "Invalid user");

                int result = _userManager.CreateUser(user);
                return Created("/api/User/{id}", result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in User/Post method : {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data - " + e.Message);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut]
        [SwaggerOperation("Updates the User profile.")]
        [SwaggerResponse(200, "Successfully updated User profile", typeof(UserVm))]
        [SwaggerResponse(500, "Model validatation fails or unhandled error occured.", typeof(UserVm))]
        [SwaggerResponse(400, "Model data type mismatch might happen.", typeof(UserVm))]
        [SwaggerRequestExample(typeof(UserVm), typeof(UserVmUpdateExample))]
        public IActionResult Put([FromBody] UserVm user)
        {
            try
            {
                _logger.LogInformation("User/Put method fired on {date}", DateTime.Now);
                int result = _userManager.UpdateUser(user);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in User/Put method : {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data - " + e.Message);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [SwaggerOperation("Deletes User profile (soft delete).")]
        public IActionResult Delete(int id)
        {
            try
            {
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
