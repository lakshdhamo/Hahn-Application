using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.VMs
{
    public class AssetVm
    {
        /// <summary>
        /// Asset Id
        /// </summary>
        public string AssetId { get; set; }

        /// <summary>
        /// Asset Symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Asset Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
