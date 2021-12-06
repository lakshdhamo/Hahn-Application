using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.VMs
{
    public class AssetDetailVm : AssetVm
    {
        [JsonProperty("id")]
        public string Id { get; init; }

        [JsonProperty("rank")]
        public string Rank { get; init; }
     
        [JsonProperty("supply")]
        public string Supply { get; init; }

        [JsonProperty("maxSupply")]
        public string MaxSupply { get; init; }

        [JsonProperty("marketCapUsd")]
        public string MarketCapUsd { get; init; }

        [JsonProperty("volumeUsd24Hr")]
        public string VolumeUsd24Hr { get; init; }

        [JsonProperty("priceUsd")]
        public string PriceUsd { get; init; }

        [JsonProperty("changePercent24Hr")]
        public string ChangePercent24Hr { get; init; }

        [JsonProperty("vwap24Hr")]
        public string Vwap24Hr { get; init; }

        [JsonProperty("explorer")]
        public string Explorer { get; init; }
    }
}
