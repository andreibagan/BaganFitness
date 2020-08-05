using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaganFitness.BL.Controller.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        [TestMethod()]
        public void SaveTest()
        {
            //Arrange
            string userName = Guid.NewGuid().ToString();

            //Act
            UserController controller = new UserController(userName);

            //Assert
            Assert.AreEqual(userName, controller.CurrentUser.Name);
        }

        [TestMethod()]
        public void SetNewUserDataTest()
        {
            //Arrange
            string userName = Guid.NewGuid().ToString();
            string gender = "man";
            DateTime birthdate = DateTime.Now.AddYears(-18);
            double weight = 90;
            double height = 180;
            UserController controller = new UserController(userName);

            //Act
            controller.SetNewUserData(gender, birthdate, weight, height);
            UserController controller2 = new UserController(userName);

            //Assert
            Assert.AreEqual(userName, controller2.CurrentUser.Name);
            Assert.AreEqual(gender, controller2.CurrentUser.Gender.Name);
            Assert.AreEqual(birthdate, controller2.CurrentUser.BirthDate);
            Assert.AreEqual(weight, controller2.CurrentUser.Weight);
            Assert.AreEqual(height, controller2.CurrentUser.Height);

            Assert.AreEqual(controller.CurrentUser.Name, controller2.CurrentUser.Name);
        }
    }
}