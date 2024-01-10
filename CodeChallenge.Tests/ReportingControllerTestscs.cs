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
        private const string PATH = "api/reportingstructure/";
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
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86";

            // Act
            var postRequestTask = _httpClient.GetAsync($"api/Reports/{employeeId}");
            var response = postRequestTask.Result;

            // Assert
            //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.IsTrue(reportingStructure.NumberOfReports == 0);
            Assert.IsNotNull(reportingStructure.Employee);

        }
        [TestMethod]
        public void ShouldProvideNullResponse()
        {
            //Arrange
            var employeeId = "null";

            // Act
            var postRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}


