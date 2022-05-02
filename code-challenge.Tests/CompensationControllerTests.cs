using challenge.Controllers;
using challenge.Data;
using challenge.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using code_challenge.Tests.Integration.Extensions;

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using code_challenge.Tests.Integration.Helpers;
using System.Text;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var employee = new Employee()
            {
                Department = "Complaints",
                FirstName = "Debbie",
                LastName = "Downer",
                Position = "Receiver",
            };

            var requestContent = new JsonSerialization().ToJson(employee);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/employee",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;
            var newEmployee = response.DeserializeContent<Employee>();

            var compensation = new Compensation(newEmployee, (decimal)100000.00, DateTime.Now);

            var requestContentCompensation = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTaskCompensation = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContentCompensation, Encoding.UTF8, "application/json"));
            var responseCompensation = postRequestTaskCompensation.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, responseCompensation.StatusCode);

            var newCompensation = responseCompensation.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.Employee);
        }

        [TestMethod]
        public void GetCompensationById_Returns_k()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/employee/{employeeId}");
            var response = getRequestTask.Result;

            var employee = response.DeserializeContent<Employee>();

            var compensation = new Compensation(employee, (decimal)100000.00, DateTime.Now);

            var requestContentCompensation = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTaskCompensation = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContentCompensation, Encoding.UTF8, "application/json"));
            var responseCompensation = postRequestTaskCompensation.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, responseCompensation.StatusCode);

            var newCompensation = responseCompensation.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.Employee);








            // Execute
            var getRequestTaskCompensationTwo = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var responseCompensationTwo = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensationTwo = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(compensation.Employee);
        }
    }
}
