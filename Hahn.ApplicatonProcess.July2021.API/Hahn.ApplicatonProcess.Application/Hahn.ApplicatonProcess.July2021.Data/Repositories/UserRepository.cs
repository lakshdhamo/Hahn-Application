using Hahn.ApplicatonProcess.July2021.Domain.Entities;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the active user profile based on the supplied Userid along with Asset details
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>Returns user profile</returns>
        public User GetUserById(int id)
        {
            return _context.Users.Include(x => x.Assets).FirstOrDefault(x => x.Id == id && x.IsActive);
        }

        /// <summary>
        /// Gets all the active users
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            return _context.Users.Where(x => x.IsActive).Include(x => x.Assets).ToList<User>();
        }
    }
}
