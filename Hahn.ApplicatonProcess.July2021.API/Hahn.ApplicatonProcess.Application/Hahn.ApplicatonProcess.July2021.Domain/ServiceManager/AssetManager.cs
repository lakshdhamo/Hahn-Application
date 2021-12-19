using Hahn.ApplicatonProcess.July2021.Domain.Interfaces.ServiceInterface;
using Hahn.ApplicatonProcess.July2021.Domain.VMs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.ServiceManager
{
    /// <summary>
    /// Manager class to handle the Asset related operations
    /// </summary>
    public class AssetManager : IAssetManager
    {
        /// <summary>
        /// Gets all the assets from https://api.coincap.io/v2/assets
        /// </summary>
        /// <returns>Collection of assets</returns>
        public async Task<List<AssetDetailDto>> GetAssetDetailsAsync()
        {
            return await GetAssetDetailsFromApiAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the specified asset from https://api.coincap.io/v2/assets
        /// </summary>
        /// <param name="id">Asset id</param>
        /// <returns>Returns AssetDetailVm detail</returns>
        public async Task<AssetDetailDto> GetAssetDetailByIdAsync(string id)
        {
            List<AssetDetailDto> assetDeatils = await GetAssetDetailsFromApiAsync().ConfigureAwait(false);
            AssetDetailDto assetDetail = assetDeatils.FirstOrDefault(x => x.Id == id);
            return assetDetail;
        }

        /// <summary>
        /// Validate the associated asset details
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Collection of failed validation errors</returns>
        public string AssetValidation(UserDto userDto)
        {
            string error = string.Empty;
            List<AssetDetailDto> lstAssets = GetAssetDetailsFromApiAsync().Result;
            foreach (AssetDto ast in userDto.Assets)
            {
                if (!lstAssets.Any(x => x.Id == ast.AssetId && x.Name == ast.Name && x.Symbol == ast.Symbol))
                {
                    error = "Asset " + ast.AssetId + " - " + ast.Name + " - " + ast.Symbol + " is not valid. /n";
                }
            }

            return error;
        }

        /// <summary>
        /// Get assets from "https://api.coincap.io/v2/assets"
        /// </summary>
        /// <returns></returns>
        private async Task<List<AssetDetailDto>> GetAssetDetailsFromApiAsync()
        {
            List<AssetDetailDto> assetDeatils = new();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://api.coincap.io/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("v2/assets").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    dynamic dynJson = JsonConvert.DeserializeObject(result);

                    foreach (var item in dynJson)
                    {
                        assetDeatils = item.Value.ToObject<List<AssetDetailDto>>();
                        break;
                    }

                }

            }
            return assetDeatils;
        }
    }
}
