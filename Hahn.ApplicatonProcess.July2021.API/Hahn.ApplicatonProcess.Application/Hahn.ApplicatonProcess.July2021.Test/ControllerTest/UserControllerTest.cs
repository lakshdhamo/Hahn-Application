﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.July2021.Domain.Entities;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces.ServiceInterface;
using Hahn.ApplicatonProcess.July2021.Domain.VMs;
using Hahn.ApplicatonProcess.July2021.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Hahn.ApplicatonProcess.July2021.Test.ControllerTest
{
    public class UserControllerTest
    {
        private readonly Mock<ILogger<UserController>> _mockLogger;
        private readonly Mock<IUserManager> _userManager;
        private readonly UserController _userController;
        private UserVm singleUser;
        private Asset asset;
        private List<Asset> lstAssets = new List<Asset>();

        public UserControllerTest()
        {
            _mockLogger = new Mock<ILogger<UserController>>();
            _userManager = new Mock<IUserManager>();
            _userController = new UserController(_userManager.Object, _mockLogger.Object);
        }

        [SetUp]
        public void Setup()
        {

            Asset astBitcoin = new Asset()
            {
                AssetId = "bitcoin",
                Name = "Bitcoin",
                Symbol = "BTC"
            };
            lstAssets.Add(astBitcoin);

            Asset astEthereum = new Asset()
            {
                AssetId = "ethereum",
                Name = "Ethereum",
                Symbol = "ETH"
            };
            lstAssets.Add(astEthereum);

            Asset astBinancecoin = new Asset()
            {
                AssetId = "binance-coin",
                Name = "Binance Coin",
                Symbol = "BNB"
            };
            lstAssets.Add(astBinancecoin);

            Asset astTether = new Asset()
            {
                AssetId = "tether",
                Name = "Tether",
                Symbol = "USDT"
            };
            lstAssets.Add(astTether);

            Asset astSolana = new Asset()
            {
                AssetId = "solana",
                Name = "Solana",
                Symbol = "SOL"
            };
            lstAssets.Add(astSolana);

            singleUser = new UserVm()
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
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void CreateUser_ReturnsStatus201Created_When_NewObjectPassed()
        {
            // Arrange
            UserVm newsUser = new UserVm()
            {
                Id = 0,
                Address = "15/3, North Street",
                Age = 25,
                Email = "1@1.com",
                FirstName = "User1",
                LastName = "LUser1",
                Assets = lstAssets.Take(3).ToList<Asset>()
            };
            _userManager.Setup(x => x.CreateUser(newsUser));

            // Act
            IActionResult result = _userController.Post(newsUser);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(201, statusCodeResult.StatusCode);
        }

        [Test]
        public void CreateUser_ReturnsStatus500Created_When_ValidationFailed()
        {
            // Arrange
            UserVm newsUser = new UserVm()
            {
                Id = 0,
                Address = "15/3, North Street",
                Age = 25,
                Email = "1@1.com",
                FirstName = "U",
                LastName = "LUser1",
                Assets = lstAssets.Take(3).ToList<Asset>()
            };
            _userManager.Setup(x => x.CreateUser(newsUser)).Throws(new Exception("First Name should contains at least 3 Characters"));

            // Act
            IActionResult result = _userController.Post(newsUser);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [Test]
        public void UpdateUser_ReturnsStatus200_When_CorrectObjectPassed()
        {
            // Arrange
            UserVm newsUser = new UserVm()
            {
                Id = 1,
                Address = "15/3, North Street",
                Age = 25,
                Email = "1@1.com",
                FirstName = "User1",
                LastName = "LUser1",
                Assets = lstAssets.Take(3).ToList<Asset>()
            };
            _userManager.Setup(x => x.UpdateUser(newsUser));

            // Act
            IActionResult result = _userController.Put(newsUser);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [Test]
        public void UpdateUser_ReturnsStatus500_When_ValidationFailed()
        {
            // Arrange
            UserVm newsUser = new UserVm()
            {
                Id = 1,
                Address = "15/3, North Street",
                Age = 25,
                Email = "1@1.com",
                FirstName = "U",
                LastName = "LUser1",
                Assets = lstAssets.Take(3).ToList<Asset>()
            };
            _userManager.Setup(x => x.UpdateUser(newsUser)).Throws(new Exception("First Name should contains at least 3 Characters"));

            // Act
            IActionResult result = _userController.Put(newsUser);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
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

    }
}
