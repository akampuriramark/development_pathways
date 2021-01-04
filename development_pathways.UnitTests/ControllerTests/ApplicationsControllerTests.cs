using development_pathways.BusinessLogic;
using development_pathways.Controllers;
using development_pathways.Data;
using development_pathways.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace development_pathways.UnitTests.ControllerTests
{
    public class ApplicationsControllerTests
    {
        [Fact]
        public void GetAllApplicationsShouldNotWork()
        {
            var options = new DbContextOptionsBuilder<development_pathways_dbContext>()
                .UseInMemoryDatabase(databaseName: "getallpeople_2")
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = new development_pathways_dbContext(options))
            {
                ApplicationBusinessLogic applicationController = new ApplicationBusinessLogic(context);
                IEnumerable<Application> applications = applicationController.GetAllApplications();
                Assert.NotNull(applications);
                Assert.Empty(applications);
            }
        }
        [Fact]
        public async Task SaveApplicationShouldWorkAsync()
        {
            var options = new DbContextOptionsBuilder<development_pathways_dbContext>()
                .UseInMemoryDatabase(databaseName: "saveapplication_1")
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = new development_pathways_dbContext(options))
            {
                Application person = new Application { ApplicationId = 1, FullName = "Test Name", IdNumber = "XXXC", SubCounty = 1 };
                ApplicationBusinessLogic personController = new ApplicationBusinessLogic(context);
                int rows = await personController.SaveApplication(person);
                Assert.Equal(1, rows);
            }
        }
    }
}
