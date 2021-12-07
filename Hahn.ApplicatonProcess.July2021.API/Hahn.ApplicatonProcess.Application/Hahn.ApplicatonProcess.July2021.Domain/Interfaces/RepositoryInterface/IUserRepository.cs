using Hahn.ApplicatonProcess.July2021.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        /// <summary>
        /// Gets the active user profile based on the supplied Userid along with Asset details
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns user profile</returns>
        User GetUserById(int id);

        /// <summary>
        /// Gets all the active users
        /// </summary>
        /// <returns></returns>
        List<User> GetAllUsers();
    }
}
