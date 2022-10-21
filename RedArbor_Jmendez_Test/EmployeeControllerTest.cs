using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RedArbor_Jmendez.Controllers;
using RedArbor_Jmendez.Models;
using RedArbor_Jmendez_DAL.Entities;
using RedArbor_Jmendez_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;

namespace RedArbor_Jmendez_Test
{
    [TestClass]
    public class EmployeeControllerTest
    {
        [TestMethod]
        public void GetAllEmployeesOK()
        {
            //Arrange
            var mockRepEmployee = new Mock<IEmployeeRepository>();
            List<EmployeeDAO> listEmployee = new List<EmployeeDAO>();
            EmployeeDAO emp1 = new EmployeeDAO
            {
                Id = 1,
                CompanyId = 1,
                CreatedOn = DateTime.Now,
                DeleteOn = null,
                Email = "test1@gmail.com",
                Fax = "000.000.000",
                Name = "test",
                Lastlogin = null,
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "999.999.999",
                UpdateOn = null,
                Username = "test"
            };
            EmployeeDAO emp2 = new EmployeeDAO
            {
                Id = 2,
                CompanyId = 1,
                CreatedOn = DateTime.Now,
                DeleteOn = null,
                Email = "test2@gmail.com",
                Fax = "000.000.000",
                Name = "test2",
                Lastlogin = null,
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "999.999.999",
                UpdateOn = null,
                Username = "test2"
            };
            listEmployee.Add(emp1);
            listEmployee.Add(emp2);
            mockRepEmployee.Setup(x => x.GetAllEmployees()).Returns(listEmployee);
            var mockRepLogApi = new Mock<ILogApiCallsRepository>();
            var controller = new EmployeeController(mockRepEmployee.Object, mockRepLogApi.Object);
            //
            IHttpActionResult actionResult = controller.GetAllEmployees();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<EmployeeDAO>>;
            //Assert 
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count());

        }

        [TestMethod]
        public void GetEmployeeOK()
        {
            //Arrange
            EmployeeController controller = InstanceMockEmployee();
            // Act
            IHttpActionResult actionResult = controller.GetEmployee(1);
            var contentResult = actionResult as OkNegotiatedContentResult<EmployeeDAO>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1,contentResult.Content.Id);
        }

        [TestMethod]
        public void GetEmployeeBadRequest()
        {
            //Arrange
            EmployeeController controller = InstanceMockEmployee();
            // Act
            IHttpActionResult actionResult = controller.GetEmployee(-1);
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetEmployeeNotFound()
        {
            //Arrange
            EmployeeController controller = InstanceMockEmployee();
            // Act
            IHttpActionResult actionResult = controller.GetEmployee(1000);
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    
        [TestMethod]
        public void AddEmployeeBadRequest()
        {
            //Arrange
            EmployeeController controller = InstanceMock();
            // Act
            IHttpActionResult actionResult = controller.AddEmployee(null);
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void UpdateEmployeeOk()
        {
            //Arrange
            EmployeeController controller = InstanceMockEmployee();
            //Act
            Employee emp = EmpAddorUpdate(1);
            IHttpActionResult actionResult = controller.UpdateEmployee(1,emp);
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void UpdateEmployeeBadRequest()
        {
            //Arrange
            EmployeeController controller = InstanceMockEmployee();
            //Act
            Employee emp = EmpAddorUpdate(10);
            IHttpActionResult actionResult = controller.UpdateEmployee(1, emp);
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void UpdateEmployeeNotFound()
        {
            EmployeeController controller = InstanceMockEmployee();
            //Act
            Employee emp = EmpAddorUpdate(2);
            IHttpActionResult actionResult = controller.UpdateEmployee(2, emp);
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteEmployeeOk()
        {
            //Arrange
            EmployeeController controller = InstanceMockEmployee();
            // Act
            IHttpActionResult actionResult = controller.DeleteEmployee(1);
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteEmployeeBadrequest()
        {
            //Arrange
            EmployeeController controller = InstanceMockEmployee();
            // Act
            IHttpActionResult actionResult = controller.DeleteEmployee(-1);
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void DeleteEmployeeNotFound()
        {
            //Arrange
            EmployeeController controller = InstanceMockEmployee();
            // Act
            IHttpActionResult actionResult = controller.DeleteEmployee(1000);
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        private EmployeeController InstanceMockEmployee()
        {
            var mockRepEmployee = new Mock<IEmployeeRepository>();
            mockRepEmployee.Setup(x => x.GetEmployee(1)).Returns(
                new EmployeeDAO
                {
                    Id = 1,
                    CompanyId = 1,
                    CreatedOn = DateTime.Now,
                    DeleteOn = null,
                    Email = "test1@gmail.com",
                    Fax = "000.000.000",
                    Name = "test",
                    Lastlogin = null,
                    Password = "passTest",
                    PortalId = 1,
                    RoleId = 1,
                    StatusId = 1,
                    Telephone = "999.999.999",
                    UpdateOn = null,
                    Username = "test"
                }
            );
            var mockRepLogApi = new Mock<ILogApiCallsRepository>();
            return new EmployeeController(mockRepEmployee.Object, mockRepLogApi.Object);
        }

        private EmployeeController InstanceMock()
        {
            var mockRepEmployee = new Mock<IEmployeeRepository>();
            var mockRepLogApi = new Mock<ILogApiCallsRepository>();
            return new EmployeeController(mockRepEmployee.Object, mockRepLogApi.Object);
        }

        private Employee EmpAddorUpdate(int idEmp = 0)
        {
            Employee emp = new Employee
            {
                Id = idEmp,
                CompanyId = 1,
                CreatedOn = DateTime.Now,
                DeleteOn = null,
                Email = "test1Update@gmail.com",
                Fax = "000.000.000",
                Name = "test",
                Lastlogin = null,
                Password = "passtest",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "999.999.999",
                UpdateOn = null,
                Username = "testUpdate"
            };
            return emp;
        }
    }
}
