using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CodeCodeChallenge.Tests.Integration.Extensions;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class ReportingControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;
        private const string PATH = "api/reports/";
        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }
        // Happy Path
        [TestMethod]
        public void ShouldProvideNumberOfDirectReportsGreaterThanZero()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedFirstName = "John";
            var expectedLastName = "Lennon";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reports/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var employee = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(expectedFirstName, employee.Employee.FirstName);
            Assert.AreEqual(expectedLastName, employee.Employee.LastName);
            Assert.AreEqual(4, employee.NumberOfReports);
        }   




         
        [TestMethod]
        public void ShouldProvideNullResponse()
        {
            //Arrange
            var employeeId = "null";

            // Act
            var getRequestTask = _httpClient.GetAsync($"{PATH}{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}


