using Hahn.ApplicatonProcess.July2021.Domain.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces.ServiceInterface
{
    public interface IAssetManager
    {
        /// <summary>
        /// Gets all the assets from https://api.coincap.io/v2/assets
        /// </summary>
        /// <returns>Collection of assets</returns>
        Task<List<AssetDetailVm>> GetAssetDetailsAsync();

        /// <summary>
        /// Gets the specified asset from https://api.coincap.io/v2/assets
        /// </summary>
        /// <param name="id">Asset id</param>
        /// <returns>Returns AssetDetailVm detail</returns>
        Task<AssetDetailVm> GetAssetDetailByIdAsync(string id);

        /// <summary>
        /// Validate the associated asset details
        /// </summary>
        /// <param name="userVm"></param>
        /// <returns>RCollection of failed validation errors</returns>
        string AssetValidation(UserVm userVm);
    }
}
