using System;
using System.Linq;
using BaganFitness.BL.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaganFitness.BL.Controller.Tests
{
    [TestClass()]
    public class EatingControllerTests
    {
        [TestMethod()]
        public void AddTest()
        {
            //Arrange
            Random random = new Random();
            string userName = Guid.NewGuid().ToString();
            string foodName = Guid.NewGuid().ToString();
            UserController userController = new UserController(userName);
            EatingController eatingController = new EatingController(userController.CurrentUser);
            Food food = new Food(foodName, random.Next(50, 500), random.Next(50, 500), random.Next(50, 500), random.Next(50, 500));

            //Act
            eatingController.Add(food, 100);

            //Assert
            Assert.AreEqual(food.Name, eatingController.Eating.Foods.LastOrDefault().Key.Name);
        }
    }
}