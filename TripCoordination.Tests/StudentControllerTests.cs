using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;


using TripCoordination.Data.Repository;
using TripCoordination.Common.ViewModel;
using TripCoordination.Controllers;

public class StudentControllerTests
{
    [Fact]
    public async Task StudentDashboard_ReturnsCorrectViewModel()
    {
        // Arrange
        var mockRepo = new Mock<IStudentDashboardRepository>();
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

        var controller = new StudentController(mockRepo.Object);

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
}
