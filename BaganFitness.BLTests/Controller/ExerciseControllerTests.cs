using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaganFitness.BL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaganFitness.BL.Model;

namespace BaganFitness.BL.Controller.Tests
{
    [TestClass()]
    public class ExerciseControllerTests
    {
        [TestMethod()]
        public void AddTest()
        {
            //Arrange
            Random random = new Random();
            string userName = Guid.NewGuid().ToString();
            string activityName = Guid.NewGuid().ToString();

            UserController userController = new UserController(userName);
            ExerciseController exerciseController = new ExerciseController(userController.CurrentUser);

            Activity activity = new Activity(activityName, random.Next(10, 50));

            //Act
            exerciseController.Add(activity, DateTime.Now, DateTime.Now.AddHours(1));

            //Assert
            Assert.AreEqual(activityName, exerciseController.Activities.First().Name);
        }
    }
}