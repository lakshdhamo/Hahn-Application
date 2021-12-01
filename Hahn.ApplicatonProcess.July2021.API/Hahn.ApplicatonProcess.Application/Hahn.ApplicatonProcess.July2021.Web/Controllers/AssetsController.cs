using Hahn.ApplicatonProcess.July2021.Domain.Interfaces.ServiceInterface;
using Hahn.ApplicatonProcess.July2021.Domain.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hahn.ApplicatonProcess.July2021.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetManager _assetManager;
        private readonly ILogger _logger;

        public AssetsController(IAssetManager assetManager, ILogger<UsersController> logger)
        {
            _assetManager = assetManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/<AssetDetailController>
        [HttpGet]
        [SwaggerOperation("Gets all the assets from https://api.coincap.io/v2/assets ")]
        public async Task<List<AssetDetailVm>> GetAsync()
        {
            return await _assetManager.GetAssetDetailsAsync();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [SwaggerOperation("Gets the specified asset from https://api.coincap.io/v2/assets ")]
        public async Task<AssetDetailVm> GetAsync(string id)
        {
            return await _assetManager.GetAssetDetailByIdAsync(id);
        }

    }
}
