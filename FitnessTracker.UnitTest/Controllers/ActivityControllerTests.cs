using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FitnessTracker.Controllers;
using FitnessTracker.Application.DTOs;
using FitnessTracker.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using FitnessTracker.Application.Queries;

public class ActivityControllerTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IActivityService> _activityServiceMock;
    private readonly ActivityController _controller;

    public ActivityControllerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _activityServiceMock = _fixture.Freeze<Mock<IActivityService>>();
        _controller = new ActivityController(_activityServiceMock.Object);
    }

    [Fact]
    public async Task GetById_ShouldReturnBadRequest_WhenIdIsInvalid()
    {
        // Arrange
        int invalidId = 0;

        // Act
        var result = await _controller.GetById(invalidId);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Id must be greater than zero.", actionResult.Value);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenActivityExists()
    {
        // Arrange
        var id = _fixture.Create<int>();
        var activity = _fixture.Create<ActivityDto>();

        _activityServiceMock.Setup(service => service.GetActivityByIdAsync(id))
                            .ReturnsAsync(activity);

        // Act
        var result = await _controller.GetById(id);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(activity, actionResult.Value);
    }

    [Fact]
    public async Task GetAll_ShouldReturnBadRequest_WhenNoQueryParametersAreProvided()
    {
        // Arrange
        var activityQuery = new ActivityQuery();

        // Act
        var result = await _controller.GetAll(activityQuery);

        // Assert

        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        var actualValue = actionResult.Value.GetType().GetProperty("message").GetValue(actionResult.Value, null);
        Assert.Equal("At least one query parameter must be provided.", actualValue);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WithFilteredActivities()
    {
        // Arrange
        var activityQuery = _fixture.Create<ActivityQuery>();

        var activities = _fixture.CreateMany<ActivityDto>();

        _activityServiceMock.Setup(service => service.GetActivitiesAsync(activityQuery))
                            .ReturnsAsync(activities);

        // Act
        var result = await _controller.GetAll(activityQuery);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(activities, actionResult.Value);
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        var newActivity = _fixture.Create<ActivityCreateDto>();
        _controller.ModelState.AddModelError("Title", "Required");

        // Act
        var result = await _controller.Create(newActivity);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedAtAction_WhenActivityIsCreated()
    {
        // Arrange
        var newActivity = _fixture.Create<ActivityCreateDto>();
        var createdActivity = _fixture.Create<ActivityDto>();

        _activityServiceMock.Setup(service => service.CreateActivityAsync(newActivity))
                            .ReturnsAsync(createdActivity);

        // Act
        var result = await _controller.Create(newActivity);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("GetAll", actionResult.ActionName);
        Assert.Equal(createdActivity, actionResult.Value);
    }

    [Fact]
    public async Task Update_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        var id = _fixture.Create<int>();
        var updatedActivity = _fixture.Create<ActivityUpdateDto>();
        _controller.ModelState.AddModelError("Title", "Required");

        // Act
        var result = await _controller.Update(id, updatedActivity);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
    }

    [Fact]
    public async Task Update_ShouldReturnNoContent_WhenActivityIsUpdated()
    {
        // Arrange
        var id = _fixture.Create<int>();
        var updatedActivity = _fixture.Build<ActivityUpdateDto>().With(x => x.Id, id).Create();

        // Setup the mock to expect a call to UpdateActivityAsync
        _activityServiceMock.Setup(service => service.UpdateActivityAsync(id, updatedActivity))
                            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Update(id, updatedActivity);

        // Assert
        Assert.IsType<NoContentResult>(result);

        _activityServiceMock.Verify(service => service.UpdateActivityAsync(id, updatedActivity), Times.Once);
    }

    [Fact]
    public async Task Delete_ShouldReturnBadRequest_WhenIdIsInvalid()
    {
        // Arrange
        int invalidId = 0;

        // Act
        var result = await _controller.Delete(invalidId);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        var actualValue = actionResult.Value.GetType().GetProperty("message").GetValue(actionResult.Value, null);
        Assert.Equal("Invalid Id.", actualValue);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenActivityIsDeleted()
    {
        // Arrange
        var id = _fixture.Create<int>();

        // Act
        var result = await _controller.Delete(id);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}