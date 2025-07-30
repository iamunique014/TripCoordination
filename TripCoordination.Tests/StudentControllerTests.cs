using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;
using TripCoordination.Controllers;
using TripCoordination.Data.Repository;
using Xunit;

public class StudentControllerTests
{
    [Fact]
    public async Task StudentDashboard_ReturnsCorrectViewModel()
    {
        // Arrange
        var mockRepo = new Mock<IStudentDashboardRepository>();
        var userRepository = new Mock<IUserRepository>();
        var fakeUserId = "user123";

        var expectedTrip = new UpcomingTripViewModel
        {
            TripID = 1,
            DestinationName = "Bizana"
        };

        var expectedRequests = new List<TripRequestSummaryViewModel>
        {
            new TripRequestSummaryViewModel { TripRequestID = 1, FromLocation = "Gqeberha", ToLocation = "Bizana" }
        };

        mockRepo.Setup(r => r.GetNextUpcomingTrip(fakeUserId))
                .ReturnsAsync(expectedTrip);
        mockRepo.Setup(r => r.GetRecentTripRequests(fakeUserId))
                .ReturnsAsync(expectedRequests);

        var controller = new StudentController(mockRepo.Object, userRepository.Object);

        // Simulate authenticated user
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, fakeUserId),
            new Claim(ClaimTypes.Role, "Student")
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(claims, "mock"))
            }
        };

        // Act
        var result = await controller.StudentDashboard();

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<StudentDashboardViewModel>().Subject;

        model.UpcomingTrip.Should().BeEquivalentTo(expectedTrip);
        model.RecentTripRequests.Should().BeEquivalentTo(expectedRequests);
    }
    [Fact]
    public async Task StudentDashboard_NullUserId_RedirectsToLoginWithErrorMessage()
    {
        var studentDashboardRepository = new Mock<IStudentDashboardRepository>();
        var userRepository = new Mock<IUserRepository>();

        // Arrange
        var controller = new StudentController(studentDashboardRepository.Object, userRepository.Object);
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
        var result = await controller.StudentDashboard() as RedirectToPageResult;

        // Assert
        var redirect = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/Account/Login", result.PageName);
        Assert.Equal("You are not authorized to access this page. Please log in to continue.", controller.TempData["ErrorMessage"]);
    }
    [Fact]
    public async Task StudentDashboard_ExceptionThrown_ReturnsErrorView()
    {
        // Arrange
        var studentDashboardRepository = new Mock<IStudentDashboardRepository>();
        var userRepository = new Mock<IUserRepository>();
        var fakeUserId = "user123";

        // Setup repo to throw exception
        studentDashboardRepository
            .Setup(r => r.GetNextUpcomingTrip(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Simulated failure"));

        var controller = new StudentController(studentDashboardRepository.Object, userRepository.Object);

        // Setup fake authenticated user
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, fakeUserId),
            new Claim(ClaimTypes.Role, "Student")
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(claims, "mock"))
            }
        };

        // Act
        var result = await controller.StudentDashboard();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }


}
