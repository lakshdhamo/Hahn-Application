using Hahn.ApplicatonProcess.July2021.Domain.Entities;
using Hahn.ApplicatonProcess.July2021.Domain.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces.ServiceInterface
{
    public interface IUserManager
    {
        /// <summary>
        /// Creates user profile
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        int CreateUser(UserVm user);

        /// <summary>
        /// Gets all the users' details
        /// </summary>
        /// <returns></returns>
        List<UserVm> GetUsers();

        /// <summary>
        /// Get the User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserVm GetUser(int id);

        /// <summary>
        /// Update the user data
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        int UpdateUser(int id, UserVm user);

        /// <summary>
        /// Removes user.
        /// </summary>
        /// <param name="id"></param>
        void DeleteUser(int id);
    }
}
