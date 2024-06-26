﻿using CodeChallenge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CodeCodeChallenge.Tests.Integration.Helpers;
using CodeCodeChallenge.Tests.Integration.Extensions;
using Newtonsoft.Json;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;
        private const string PATH = "api/compensation";
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
        
        [TestMethod]
        [DataRow("16a596ae-edd3-4847-99fe-c4518e82c86")]
        public void CompensationController_CreateCompensationRecord_ShouldCreateNewCompansionRecord(string id)
        {
            // Arrange
            var compansation = new Compensation()
            {
                Employee = id,
                Salary = 100000.00m,
                EffectiveDate = DateTime.Now.ToShortDateString(),
            };

            
            // Act
            var getRequestTask = _httpClient.PostAsync($"{PATH}", new StringContent(JsonConvert.SerializeObject(compansation), Encoding.UTF8, "application/json"));
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var result = response.DeserializeContent<Compensation>();

            Assert.IsNotNull( result );
            Assert.AreEqual(result.Salary, compansation.Salary);
            Assert.AreEqual(result.EffectiveDate, compansation.EffectiveDate);
            Assert.AreEqual(result.Employee, compansation.Employee);

        }

        
        [TestMethod]
        public void CompensationController_CreateCompensationRecord_ShouldReturnBadRequest()
        {

            // Act
            var postRequestTask = _httpClient.PostAsync($"{PATH}", new StringContent(JsonConvert.SerializeObject(""), Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }



        [TestMethod]
        public void CompensationController_GetCompensationRecord_ShouldReturnValidRecord()
        {
            // Arrange
            var employeeId = "62c1084e-6e34-4630-93fd-9153afb65309";

            // Act
            CompensationController_CreateCompensationRecord_ShouldCreateNewCompansionRecord(employeeId);
            var getRequestTask = _httpClient.GetAsync($"{PATH}/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var result = response.DeserializeContent<Compensation>();

            Assert.IsNotNull(result);


        }

        [TestMethod]
        public void CompensationController_GetCompensationRecord_ReturnsBadRequestError()
        {
           
            // Act
            var getRequestTask = _httpClient.GetAsync($"{PATH}/THISISNOTGOINGTOWORK");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}

