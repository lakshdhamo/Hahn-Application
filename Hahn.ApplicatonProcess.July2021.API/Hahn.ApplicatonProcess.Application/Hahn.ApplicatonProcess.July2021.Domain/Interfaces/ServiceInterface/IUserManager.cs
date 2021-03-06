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
        UserDto CreateUser(UserDto user);

        /// <summary>
        /// Gets all the users' details
        /// </summary>
        /// <returns></returns>
        List<UserDto> GetUsers();

        /// <summary>
        /// Get the User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserDto GetUser(in int id);

        /// <summary>
        /// Update the user data
        /// </summary>
        /// <param name="id">Unique id</param>
        /// <param name="userDto">Entity to be modified</param>
        /// <returns>Returns the updated User profile id</returns>
        UserDto UpdateUser(in int id, UserDto user);

        /// <summary>
        /// Removes user.
        /// </summary>
        /// <param name="id"></param>
        void DeleteUser(in int id);

        /// <summary>
        /// Check whether already system has same user. 
        /// Considered Email as the Candidate Key
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>
        /// True: If user already exists
        /// False: User doesn't exists
        /// </returns>
        bool IsUserAlreadyExists(UserDto userDto);
    }
}
