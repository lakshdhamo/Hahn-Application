using FluentValidation.Results;
using Hahn.ApplicatonProcess.July2021.Domain.Entities;
using Hahn.ApplicatonProcess.July2021.Domain.ExtensionMethods;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces.ServiceInterface;
using Hahn.ApplicatonProcess.July2021.Domain.Validators;
using Hahn.ApplicatonProcess.July2021.Domain.VMs;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hahn.ApplicatonProcess.July2021.Domain.ServiceManager
{
    /// <summary>
    /// Manager class to handle the User related operations
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAssetManager _assetManager;
        private readonly ICacheManager _cacheManager;
        private const string getUsersKey = "getUsersKey";

        public UserManager(IUnitOfWork unitOfWork, IAssetManager assetManager, ICacheManager cacheManager)
        {
            _unitOfWork = unitOfWork;
            _assetManager = assetManager;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates user profile
        /// </summary>
        /// <param name="userVm"></param>
        /// <returns>Returns newly created User profile's id</returns>
        public UserVm CreateUser(UserVm userVm)
        {
            ValidationModel validateResult = ValidateUser(userVm);
            string errorMessage;

            /// User  model validation
            if (!validateResult.IsValid)
            {
                errorMessage = string.Join(',', validateResult.ValidationMessages);
            }

            /// Associated Asset validation
            errorMessage = _assetManager.AssetValidation(userVm);

            /// Throw exception in case of any validation fail
            if (errorMessage.Length > 0)
            {
                throw new Exception(errorMessage);
            }

            User user = new()
            {
                FirstName = userVm.FirstName,
                LastName = userVm.LastName,
                Address = userVm.Address,
                Age = userVm.Age,
                Email = userVm.Email,
                CreatedById = 0,
                CreatedDate = DateTime.UtcNow,
                ModifiedById = 0,
                ModifiedDate = DateTime.UtcNow,
                IsActive = true,
                Assets = userVm.ExtractAssets()
            };

            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();
            /// Remove the cached value due to Add
            _cacheManager.Remove(getUsersKey);
            return user.ExtractUserVm();
        }

        /// <summary>
        /// Gets all the users' details
        /// </summary>
        /// <returns>Retunrs the user profile infomation along with associated Aseet details</returns>
        public List<UserVm> GetUsers()
        {
            List<UserVm> lstUsers = null;
            // If found in cache, return cached data

            lstUsers = _cacheManager.Get<List<UserVm>>(getUsersKey, out lstUsers);

            if (lstUsers != null)
            { return lstUsers; }

            lstUsers = (from p in
                _unitOfWork.Users.GetAllUsers()
                        select new UserVm
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            Age = p.Age,
                            Email = p.Email,
                            Address = p.Address,
                            Assets = p.ExtractAssetVms()

                        }).ToList();

            /// Cache the output
            if (lstUsers.Count > 0)
            {
                // Set cache options
                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(24));

                // Set object in cache
                _cacheManager.Add(getUsersKey, lstUsers, cacheOptions);
            }


            return lstUsers;
        }

        /// <summary>
        /// Get the User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns specific User profile detail</returns>
        public UserVm GetUser(in int id)
        {
            User user = _unitOfWork.Users.GetUserById(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user.ExtractUserVm();
        }

        /// <summary>
        /// Update the user data
        /// </summary>
        /// <param name="id">Unique id</param>
        /// <param name="userVm">Entity to be modified</param>
        /// <returns>Returns the updated User profile id</returns>
        public UserVm UpdateUser(in int id, UserVm userVm)
        {
            ValidationModel validateResult = ValidateUser(userVm);

            if (!validateResult.IsValid)
            {
                string errorMessage = string.Join(',', validateResult.ValidationMessages);
                throw new Exception(errorMessage);
            }

            User user = _unitOfWork.Users.GetUserById(id);
            user.FirstName = userVm.FirstName;
            user.LastName = userVm.LastName;
            user.Address = userVm.Address;
            user.Age = userVm.Age;
            user.Email = userVm.Email;
            user.ModifiedById = userVm.Id;
            user.ModifiedDate = DateTime.UtcNow;
            user.Assets = userVm.ExtractAssets();

            _unitOfWork.Complete();
            /// Remove the cached value due to update
            _cacheManager.Remove(getUsersKey);
            return user.ExtractUserVm();
        }

        /// <summary>
        /// Removes user.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUser(in int id)
        {
            User user = _unitOfWork.Users.GetUserById(id);
            user.IsActive = false;
            _unitOfWork.Complete();
            /// Remove the cached value due to Delete
            _cacheManager.Remove(getUsersKey);
        }

        /// <summary>
        /// Check whether already system has same user. 
        /// Considered Email as the Candidate Key
        /// </summary>
        /// <param name="userVM"></param>
        /// <returns>
        /// True: If user already exists
        /// False: User doesn't exists
        /// </returns>
        public bool IsUserAlreadyExists(UserVm userVM)
        {
            return GetUsers().Any(x => x.Email == userVM.Email);
        }

        /// <summary>
        /// Validate the User entity
        /// </summary>
        /// <param name="userVm"></param>
        /// <returns>Returns ValidationModel to describe about validation error details</returns>
        private ValidationModel ValidateUser(UserVm userVm)
        {
            UserValidator validator = new();
            ValidationResult validationResult = validator.Validate(userVm);
            List<string> ValidationMessages = new();

            ValidationModel response = new();
            if (!validationResult.IsValid)
            {
                response.IsValid = false;
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ValidationMessages.Add(failure.ErrorMessage);
                }
                response.ValidationMessages = ValidationMessages;
            }
            return response;
        }

    }
}
