using Hahn.ApplicatonProcess.July2021.Domain.Interfaces.ServiceInterface;
using Hahn.ApplicatonProcess.July2021.Domain.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Hahn.ApplicatonProcess.July2021.Domain.Entities;

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
        public async Task<List<AssetDetailVm>> GetAssetDetailsAsync()
        {
            return await GetAssetDetailsFromApiAsync();
        }

        /// <summary>
        /// Gets the specified asset from https://api.coincap.io/v2/assets
        /// </summary>
        /// <param name="id">Asset id</param>
        /// <returns>Returns AssetDetailVm detail</returns>
        public async Task<AssetDetailVm> GetAssetDetailByIdAsync(string id)
        {
            List<AssetDetailVm> assetDeatils = await GetAssetDetailsFromApiAsync();
            AssetDetailVm assetDetail = assetDeatils.FirstOrDefault(x => x.Id == id);
            return assetDetail;
        }

        /// <summary>
        /// Validate the associated asset details
        /// </summary>
        /// <param name="userVm"></param>
        /// <returns>Collection of failed validation errors</returns>
        public string AssetValidation(UserVm userVm)
        {
            string error = string.Empty;
            List<AssetDetailVm> lstAssets = GetAssetDetailsFromApiAsync().Result;
            foreach (AssetVm ast in userVm.Assets)
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
        private async Task<List<AssetDetailVm>> GetAssetDetailsFromApiAsync()
        {
            List<AssetDetailVm> assetDeatils = new List<AssetDetailVm>();
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
                        assetDeatils = item.Value.ToObject<List<AssetDetailVm>>();
                        break;
                    }

                }

            }
            return assetDeatils;
        }
    }
}
