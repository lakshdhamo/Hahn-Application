using Hahn.ApplicatonProcess.July2021.Domain.Entities;
using Hahn.ApplicatonProcess.July2021.Domain.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.ExtensionMethods
{
    /// <summary>
    /// Contains custom extentions methods.
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// Extracs Assets from UserVm
        /// </summary>
        /// <param name="userVm"></param>
        /// <returns>List of associated Assets</returns>
        public static List<Asset> ExtractAssets(this UserVm userVm)
        {
            return userVm.Assets.Select(x =>
                                        new Asset()
                                        {
                                            AssetId = x.AssetId,
                                            Name = x.Name,
                                            Symbol = x.Symbol
                                        }).ToList<Asset>();
        }

        /// <summary>
        /// Extracs AssetVm from User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>List of associated AssetVms</returns>
        public static List<AssetVm> ExtractAssetVms(this User user)
        {
            return user.Assets.Select(x =>
                                        new AssetVm()
                                        {
                                            AssetId = x.AssetId,
                                            Name = x.Name,
                                            Symbol = x.Symbol
                                        }).ToList<AssetVm>();
        }

    }
}
