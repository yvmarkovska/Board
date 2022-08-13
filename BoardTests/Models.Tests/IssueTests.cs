using Boarder;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardR.Tests.Models.Tests
{
    [TestClass]
    public class IssueTests
    {

        [TestMethod]
        public void Constructor_Should_AssignCorrectValues()
        {
            //Arrange
            string title = "Log in error";
            string description = "Cannot log in with correct input";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var sut = new Issue(title, description, dueDate);

            //Assert
            Assert.AreEqual(title, sut.Title);
            Assert.AreEqual(description, sut.Description);
            Assert.AreEqual(dueDate, sut.DueDate);
            Assert.AreEqual(Status.Open, sut.Status);
        }

        [TestMethod]
        public void Constructor_Should_ReturnCorrectType()
        {
            //Arrange & Act
            var sut = new Issue("Log in error",
                "Cannot log in with correct input",
                Convert.ToDateTime("01-01-2030"));

            //Assert
            Assert.IsInstanceOfType(sut, typeof(Issue));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AdvanceStatusMethod_Should_Throw_WhenAlreadyAtVerified()
        {
            //Arrange
            var sut = new Issue("Log in error",
                "Cannot log in with correct input",
                Convert.ToDateTime("01-01-2030"));

            //Act
            sut.AdvanceStatus();
            sut.AdvanceStatus();
        }

        [TestMethod]
        public void Method_Should_AdvanceStatus()
        {
            //Arrange
            var sut = new Issue("Log in error",
                "Cannot log in with correct input",
                DateTime.Now.AddDays(2));
            var prevStatus = sut.Status;

            //Act
            sut.AdvanceStatus();

            //Assert
            Assert.AreNotEqual(prevStatus, sut.Status);
            Assert.AreEqual(Status.Verified, sut.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RevertStatusMethod_Should_Throw_WhenAlreadyAtVerified()
        {
            //Arrange
            var sut = new Issue("Log in error",
                "Cannot log in with correct input",
                DateTime.Now.AddDays(2));

            //Act
            sut.RevertStatus();
            sut.RevertStatus();
        }

        [TestMethod]
        public void Method_Should_RevertStatus()
        {
            //Arrange
            var sut = new Issue("Log in error",
               "Cannot log in with correct input",
               DateTime.Now.AddDays(2));
            sut.AdvanceStatus();
            var currentStatus = sut.Status;

            //Act
            sut.RevertStatus();

            //Assert
            Assert.AreNotEqual(currentStatus, sut.Status);
            Assert.AreEqual(Status.Open, sut.Status);
        }

        [TestMethod]
        public void Issue_Should_OverRideToStringMethod()
        {
            //Arrange
            string title = "Log in error";
            string description = "Cannot log in with correct input";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var sut = new Issue(title, description, dueDate);
            string output = sut.ViewInfo();
            string expectedOutput = "'Log in error', [Open|01-01-2030] " +
                "Description: Cannot log in with correct input";

            //Assert
            Assert.AreEqual(expectedOutput, output);

        }

    }
}
