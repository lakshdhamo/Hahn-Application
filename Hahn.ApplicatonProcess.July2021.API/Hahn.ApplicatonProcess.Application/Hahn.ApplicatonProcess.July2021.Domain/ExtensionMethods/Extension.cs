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
        /// Extracs Assets from UserDto
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>List of associated Assets</returns>
        public static List<Asset> ExtractAssets(this UserDto userDto)
        {
            if (userDto.Assets.Any())
            {
                return userDto.Assets.Select(x =>
                                            new Asset()
                                            {
                                                AssetId = x.AssetId,
                                                Name = x.Name,
                                                Symbol = x.Symbol
                                            }).ToList<Asset>();
            }
            else
            {
                return new List<Asset>();
            }
        }

        /// <summary>
        /// Extracs AssetVm from User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>List of associated AssetVms</returns>
        public static List<AssetDto> ExtractAssetDtos(this User user)
        {
            if (user.Assets.Any())
            {
                return user.Assets.Select(x =>
                                        new AssetDto()
                                        {
                                            AssetId = x.AssetId,
                                            Name = x.Name,
                                            Symbol = x.Symbol
                                        }).ToList<AssetDto>();
            }
            else
            {
                return new List<AssetDto>();
            }

        }

        /// <summary>
        /// Extracs UserDto from User
        /// </summary>
        /// <param name="user">User entity</param>
        /// <returns>UserDto</returns>
        public static UserDto ExtractUserDto(this User user) => new UserDto()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Address = user.Address,
            Age = user.Age,
            Email = user.Email,
            Id = user.Id,
            Assets = user.ExtractAssetDtos()
        };

    }
}
