using Boarder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace BoardR.Tests.Models
{
    [TestClass]
    public class TaskConstructor_Should
    {
        [TestMethod]
        public void Constructor_Should_AssignCorrectTitle()
        {
            //Arrange
            var title = "This is a test title";
            var assignee = "TestUser";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var sut = new Task(title, assignee, dueDate);

            //Assert
            Assert.AreEqual(title, sut.Title);
        }

        [TestMethod]
        public void Constructor_Should_ThrowException_WhenTitleIsEmptyOrNull()
        {
            //Arrange
            var title = "This is a test title";
            var assignee = "TestUser";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act & Assert
            var sut = new Task(title, assignee, dueDate);
            Assert.ThrowsException<ArgumentException>(() => sut.Title = null);
            Assert.ThrowsException<ArgumentException>(() => sut.Title = String.Empty);
        }
        [TestMethod]
        public void Constructor_Should_ThrowException_WhenTitleIsShorterThanExpected()
        {
            //Arrange
            var sut = new Task("This is a test title",
                "TestUser",
                Convert.ToDateTime("01-01-2030"));

            //Act
            var exception = Assert.ThrowsException<ArgumentException>(() => sut.Title = new string('a', 4));

            //Assert
            Assert.AreEqual("Please provide a value with length between 5 and 30 characters.", exception.Message);
        }

        [TestMethod]
        public void Constructor_Should_ThrowException_WhenTitleIsLongerThanExpected()
        {
            //Arrange
            var sut = new Task("This is a test title",
                "TestUser",
                Convert.ToDateTime("01-01-2030"));

            //Act
            var exception = Assert.ThrowsException<ArgumentException>(() => sut.Title = new string('a', 31));

            //Assert
            Assert.AreEqual("Please provide a value with length between 5 and 30 characters.", exception.Message);
        }

        [TestMethod]
        public void Setter_Should_SetNewTitle_WhenTitleNameIsDifferent()
        {
            //Arrange
            var sut = new Task("This is a test title",
                "TestUser",
                Convert.ToDateTime("01-01-2030"));
            var previuosTitle = "This is a test title";

            //Act
            sut.Title = new string("This is a brand new title");

            //Assert
            Assert.AreNotEqual(previuosTitle, sut.Title);
        }

        [TestMethod]
        public void Constructor_Should_AssignCorrectDueDate()
        {
            //Arrange & Act
            var title = "This is a test title";
            var assignee = "TestUser";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var sut = new Task(title, assignee, dueDate);

            //Assert
            Assert.AreEqual(dueDate, sut.DueDate);
        }

        [TestMethod]
        public void Constructor_Should_Throw_WhenDueDateIsInvalid()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            new Task("This is a test title",
            "TestUser",
            DateTime.Now.AddMilliseconds(-1)));
        }

        [TestMethod]
        public void Setter_Should_SetNewDueDate_WhenDateIsDifferent()
        {
            //Arrange
            var sut = new Task("This is a test title", "TestUser", Convert.ToDateTime("01-01-2030"));
            var previuosDueDate = Convert.ToDateTime("01-01-2030");

            //Act
            sut.DueDate = previuosDueDate.AddDays(5);

            //Assert
            Assert.AreNotEqual(previuosDueDate, sut.DueDate);
        }

        [TestMethod]
        public void Constructor_Should_AssignCorrectAssignee()
        {
            //Arrange
            var title = "This is a test title";
            var assignee = "TestUser";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var sut = new Task(title, assignee, dueDate);

            //Assert
            Assert.AreEqual(assignee, sut.Assignee);
        }

        [TestMethod]
        public void Setter_Should_SetNewAssignee_WhenAssigneeIsDifferent()
        {
            //Arrange
            var sut = new Task("This is a test title",
                "TestUser",
                Convert.ToDateTime("01-01-2030"));
            var previuosAssignee = "TestUser";

            //Act
            sut.Assignee = "Yoana";

            //Assert
            Assert.AreNotEqual(previuosAssignee, sut.Assignee);
        }

        [TestMethod]
        //I sure did think about it as much as I could. I decided to write a test that checks
        //if there is anything added to the list of EventLogs upon instantiating the object.
        //The test checks if the list is empty or not regardless of it's content and format.

        public void Constructor_Should_AddsNewEventLog_WhenAssigneeIsCreated()
        {
            //Arrange & Act
            var sut = new Task("This is a test title",
                "TestUser",
                Convert.ToDateTime("01-01-2030"));

            //Assert
            Assert.IsNotNull(sut.ViewHistory());
        }

        [TestMethod]
        public void Constructor_Should_ThrowException_WhenAssigneeIsNullOrEmpty()
        {
            //Arrange
            var sut = new Task("This is a test title",
                "TestUser",
                Convert.ToDateTime("01-01-2030"));

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Assignee = null);
            Assert.ThrowsException<ArgumentException>(() => sut.Assignee = String.Empty);
        }

        //we compare the messages we get from the exceptions
        [TestMethod]
        public void Constructor_Should_ThrowException_WhenAssigneesLenghtIsLessThanExpected()
        {
            //Arrange
            var sut = new Task("This is a test title",
                "TestUser",
                Convert.ToDateTime("01-01-2030"));

            //Act
            var exception = Assert.ThrowsException<ArgumentException>(() => sut.Assignee = "Yo");

            //Assert
            Assert.AreEqual("Please provide a value between 5 and 30 characters.", exception.Message);
        }

        //compare the exceptions without asserting
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_Should_ThrowException_WhenAssigneesLenghtIsLongerThanExpected()
        {
            //Arrange
            var sut = new Task("This is a test title",
                "TestUser",
                Convert.ToDateTime("01-01-2030"));

            //Act
            sut.Assignee = new string('a', 31);
        }

        [TestMethod]
        public void Constructor_Should_AssignCorrectStatus()
        {
            //Arrange
            var title = "This is a test title";
            var assignee = "TestUser";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var sut = new Task(title, assignee, dueDate);

            //Assert
            Assert.AreEqual(Status.Todo, sut.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AdvanceStatusMethod_Should_Throw_WhenAlreadyAtVerified()
        {
            //Arrange
            var sut = new Task("This is a test title", "TestUser", Convert.ToDateTime("01-01-2030"));
            sut.AdvanceStatus();
            sut.AdvanceStatus();
            sut.AdvanceStatus();
            sut.AdvanceStatus();

            //Act
            sut.AdvanceStatus();
        }

        [TestMethod]
        public void Method_Should_AdvanceStatus()
        {
            //Arrange
            var sut = new Task("This is a test title", "TestUser", Convert.ToDateTime("01-01-2030"));
            var prevStatus = sut.Status;

            //Act
            sut.AdvanceStatus();

            //Assert
            Assert.AreNotEqual(prevStatus, sut.Status);
            Assert.AreEqual(prevStatus, sut.Status - 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RevertStatusMethod_Should_Throw_WhenAlreadyAtTodo()
        {
            //Arrange
            var sut = new Task("This is a test title",
            "TestUser",
            Convert.ToDateTime("01-01-2030"));

            //Act
            sut.RevertStatus();
        }

        [TestMethod]
        public void Method_Should_RevertStatus()
        {
            //Arrange
            var sut = new Task("This is a test title", "TestUser", Convert.ToDateTime("01-01-2030"));
            sut.AdvanceStatus();
            var statusAfterAdvance = sut.Status;

            //Act
            sut.RevertStatus();

            //Assert
            Assert.AreNotEqual(statusAfterAdvance, sut.Status);
            Assert.AreEqual(statusAfterAdvance, sut.Status + 1);
        }

        [TestMethod]
        public void Constructor_Should_ReturnCorrectType()
        {
            //Arrange
            var title = "This is a test title";
            var assignee = "TestUser";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var sut = new Task(title, assignee, dueDate);

            //Assert
            Assert.IsInstanceOfType(sut, typeof(Task));
        }


    }
}
