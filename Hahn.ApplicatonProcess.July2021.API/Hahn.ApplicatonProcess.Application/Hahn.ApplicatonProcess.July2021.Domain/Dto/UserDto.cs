using Hahn.ApplicatonProcess.July2021.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.VMs
{
    /// <summary>
    /// Holds User information
    /// </summary>
    public record UserDto
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User age
        /// </summary>
        public int Age { get; set; }
        
        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// User's Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's Asset's list
        /// </summary>
        public List<AssetDto> Assets { get; set; }
    }
}
