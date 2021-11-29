using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Entities
{
    /// <summary>
    /// Holds User profile information
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// User's age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// User's First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's associated Asset list
        /// </summary>
        public List<Asset> Assets { get; set; }

    }
}
