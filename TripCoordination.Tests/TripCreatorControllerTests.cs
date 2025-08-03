using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;
using TripCoordination.Controllers;
using TripCoordination.Data.Repository;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TripCoordination.Tests
{
    public class TripCreatorControllerTests
    {
        // Placeholder for future tests for TripCreatorController
        // This class will contain unit tests for the TripCreatorController methods
        // such as Index, CreateTrip, OrganizerDashboard, and MyTrips.
        // Each test will mock dependencies and verify the expected behavior of the controller actions.

        [Fact]
        public async Task OrganizerDashboard_ReturnsCorrectViewModel()
        {
            //Arrange
            var mockRepo = new Mock<IOrganizerDashboardRepository>();
            var tripRepo = new Mock<ITripRepository>();
            var loggerRepo = new Mock<ILogger<TripCreatorController>>();

            var userID = "organizer123";

            var expectedTrip = new UpcomingTripViewModel
            {
                TripID = 1,
                DestinationName = "Flagstaff"
            };

            var expectedTripRequests = new List<TripRequestSummaryViewModel>
            {
                new TripRequestSummaryViewModel
                { 
                    TripRequestID = 1, 
                    FromLocation = "East London", 
                    ToLocation = "Bizana" 
                }
            };

            var expectedStats = new OrganizerTripStatsViewModel 
            { 
                PendingTripRequests = 1, 
                TotalTripsCreated = 17, 
                TripsThisMonth = 3 
            };

            mockRepo.Setup(r => r.GetUpcomingTrip(userID))
                .ReturnsAsync(expectedTrip);
            mockRepo.Setup(r => r.GetRecentTripRequests())
                    .ReturnsAsync(expectedTripRequests);
            mockRepo.Setup(r=> r.GetOrganizerTripStats(userID))
                    .ReturnsAsync(expectedStats);

            var controller = new TripCreatorController(loggerRepo.Object, tripRepo.Object, mockRepo.Object);
            // Simulate authenticated user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userID),
                new Claim(ClaimTypes.Role, "Organizer")
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(claims, "mock"))
                }
            };

            // Act
            var result = await controller.OrganizerDashboard();

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<OrganizerDashboardViewModel>().Subject;

            model.UpcomingTrip.Should().BeEquivalentTo(expectedTrip);
            model.TripStats.Should().BeEquivalentTo(expectedStats);
            model.RecentTripRequests.Should().BeEquivalentTo(expectedTripRequests);
        }
        [Fact]
        public async Task OrganizerDashboard_NullUserId_RedirectsToLoginWithErrorMessage()
        {
            var organizerDashboardRepository = new Mock<IOrganizerDashboardRepository>();
            var tripRepo = new Mock<ITripRepository>();
            var loggerRepo = new Mock<ILogger<TripCreatorController>>();

            // Arrange
            var controller = new TripCreatorController(loggerRepo.Object, tripRepo.Object, organizerDashboardRepository.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity())
                }
            };

            var tempData = new TempDataDictionary(controller.ControllerContext.HttpContext, Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            // Act
            var result = await controller.OrganizerDashboard() as RedirectToPageResult;

            // Assert
            var redirect = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/Account/Login", result.PageName);
            Assert.Equal("You are not authorized to access this page. Please log in to continue.", controller.TempData["ErrorMessage"]);
        }
        [Fact]
        public async Task OrganizerDashboard_ExceptionThrown_ReturnsView()
        {   // Arrange
            var organizerDashboardRepository = new Mock<IOrganizerDashboardRepository>();
            var tripRepo = new Mock<ITripRepository>();
            var loggerRepo = new Mock<ILogger<TripCreatorController>>();
            var fakeUserId = "user123";

            // Setup repo to throw exception
            organizerDashboardRepository
                .Setup(r => r.GetUpcomingTrip(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Simulated failure"));

            var controller = new TripCreatorController(loggerRepo.Object, tripRepo.Object, organizerDashboardRepository.Object);

            // Setup fake authenticated user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, fakeUserId),
                new Claim(ClaimTypes.Role, "Organizer")
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(claims, "mock"))
                }
            };

            var tempData = new TempDataDictionary(controller.ControllerContext.HttpContext, Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            // Act
            var result = await controller.OrganizerDashboard();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model.Should().BeOfType<OrganizerDashboardViewModel>().Subject;
            Assert.Equal("An error occurred while loading the dashboard.", controller.TempData["Info"]);
        }
    }
}
