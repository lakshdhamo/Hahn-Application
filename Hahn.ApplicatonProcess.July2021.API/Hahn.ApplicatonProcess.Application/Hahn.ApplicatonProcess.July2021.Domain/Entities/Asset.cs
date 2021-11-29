using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Entities
{
    /// <summary>
    /// Holds User associates Asset information
    /// </summary>
    public class Asset : BaseEntity
    {
        /// <summary>
        /// Asset Id
        /// </summary>
        public string AssetId { get; set; }

        /// <summary>
        /// Asset Symbol
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Asset Name
        /// </summary>
        public string Name { get; set; }
    }
}
