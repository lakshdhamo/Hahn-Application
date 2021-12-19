using Hahn.ApplicatonProcess.July2021.Domain.Interfaces.ServiceInterface;
using Hahn.ApplicatonProcess.July2021.Domain.VMs;
using Hahn.ApplicatonProcess.July2021.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Hahn.ApplicatonProcess.July2021.Test.ControllerTest
{
    public class UserControllerTest
    {
        private readonly Mock<ILogger<UsersController>> _mockLogger;
        private readonly Mock<IUserManager> _userManager;
        private readonly UsersController _userController;
        private UserDto singleUser;
        private readonly List<AssetDto> lstAssets = new();

        public UserControllerTest()
        {
            _mockLogger = new Mock<ILogger<UsersController>>();
            _userManager = new Mock<IUserManager>();
            _userController = new UsersController(_userManager.Object, _mockLogger.Object);
        }

        [SetUp]
        public void Setup()
        {
            /// Prepare Assets list
            for (int i = 0; i < 5; i++)
            {
                lstAssets.Add(CreateAssetVm("bitcoin", "Bitcoin", "BTC"));
                lstAssets.Add(CreateAssetVm("ethereum", "Ethereum", "ETH"));
                lstAssets.Add(CreateAssetVm("binance-coin", "Binance Coin", "BNB"));
                lstAssets.Add(CreateAssetVm("tether", "Tether", "USDT"));
                lstAssets.Add(CreateAssetVm("solana", "Solana", "SOL"));
            }

            /// Prepare User profile
            singleUser = new UserDto()
            {
                Id = 1,
                Address = "15/3, North Street",
                Age = 25,
                Email = "1@1.com",
                FirstName = "User1",
                LastName = "LUser1",
                Assets = lstAssets
            };
        }

        [Test]
        public void GetUser_ReturnsAUser_When_IdPassed()
        {
            // Arrange
            _userManager.Setup(x => x.GetUser(1)).Returns(singleUser);

            // Act
            var result = _userController.Get(1);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [Test]
        public void CreateUser_ReturnsStatus201Created_When_NewObjectPassed()
        {
            // Arrange
            //Input Value
            UserDto newsUser = new()
            {
                Id = 0,
                Address = "15/3, North Street",
                Age = 25,
                Email = "1@1.com",
                FirstName = "User1",
                LastName = "LUser1",
                Assets = lstAssets.Take(3).ToList<AssetDto>()
            };

            //Output Value
            UserDto outputUser = new()
            {
                Id = 1,
                Address = "15/3, North Street",
                Age = 25,
                Email = "1@1.com",
                FirstName = "User1",
                LastName = "LUser1",
                Assets = lstAssets.Take(3).ToList<AssetDto>()
            };
            _userManager.Setup(x => x.CreateUser(newsUser)).Returns(outputUser);

            // Act
            ActionResult<UserDto> result = _userController.Post(newsUser);

            // Assert
            CreatedResult createdResult = (CreatedResult)result.Result;
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual(newsUser.Email, ((UserDto)createdResult.Value).Email);
        }

        [Test]
        public void CreateUser_ReturnsStatus500Created_When_ValidationFailed()
        {
            // Arrange
            UserDto newsUser = new()
            {
                Id = 0,
                Address = "15/3, North Street",
                Age = 25,
                Email = "1@1.com",
                FirstName = "U",
                LastName = "LUser1",
                Assets = lstAssets.Take(3).ToList<AssetDto>()
            };
            _userManager.Setup(x => x.CreateUser(newsUser)).Throws(new Exception("First Name should contains at least 3 Characters"));

            // Act
            ActionResult<UserDto> result = _userController.Post(newsUser);

            // Assert
            ObjectResult createdResult = (ObjectResult)result.Result;
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [Test]
        public void UpdateUser_ReturnsStatus200_When_CorrectObjectPassed()
        {
            // Arrange
            UserDto newsUser = new()
            {
                Id = 1,
                Address = "15/3, North Street",
                Age = 25,
                Email = "1@1.com",
                FirstName = "User1",
                LastName = "LUser1",
                Assets = lstAssets.Take(3).ToList<AssetDto>()
            };

            //Output Value => Modified the age
            UserDto outputUser = new()
            {
                Id = 1,
                Address = "15/3, North Street",
                Age = 27,
                Email = "1@1.com",
                FirstName = "User1",
                LastName = "LUser1",
                Assets = lstAssets.Take(3).ToList<AssetDto>()
            };
            _userManager.Setup(x => x.UpdateUser(1, newsUser)).Returns(outputUser);

            // Act
            ActionResult<UserDto> result = _userController.Put(1, newsUser);

            // Assert
            OkObjectResult status = (OkObjectResult)result.Result;
            Assert.AreEqual(200, status.StatusCode);
            Assert.AreEqual(((UserDto)status.Value).Age, 27);
        }

        [Test]
        public void UpdateUser_ReturnsStatus500_When_ValidationFailed()
        {
            // Arrange
            UserDto newsUser = new()
            {
                Id = 1,
                Address = "15/3, North Street",
                Age = 25,
                Email = "1@1.com",
                FirstName = "U",
                LastName = "LUser1",
                Assets = lstAssets.Take(3).ToList<AssetDto>()
            };
            _userManager.Setup(x => x.UpdateUser(1, newsUser)).Throws(new Exception("First Name should contains at least 3 Characters"));

            // Act
            ActionResult<UserDto> result = _userController.Put(1, newsUser);

            // Assert
            var status = (ObjectResult)result.Result;
            Assert.AreEqual(500, status.StatusCode);
        }

        [Test]
        public void DeleteUser_ReturnsStatus200_When_CorrectIdPassed()
        {
            // Arrange
            _userManager.Setup(x => x.DeleteUser(1));

            // Act
            IActionResult result = _userController.Delete(1);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [Test]
        public void DeleteUser_ReturnsStatus500_When_SomethingWentWrong()
        {
            // Arrange
            _userManager.Setup(x => x.DeleteUser(1)).Throws(new Exception("Error in deleting data"));

            // Act
            IActionResult result = _userController.Delete(1);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        #region Private Methods

        /// <summary>
        /// Create AssetVm objects
        /// </summary>
        /// <returns>AssetVm</returns>
        private AssetDto CreateAssetVm(string assetId, string name, string symbol)
        {
            return new AssetDto()
            {
                AssetId = assetId,
                Name = name,
                Symbol = symbol
            };
        }
        #endregion

    }
}
