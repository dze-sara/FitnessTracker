using AutoFixture;
using Moq;
using FitnessTracker.Application.DTOs;
using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Interfaces;
using FitnessTracker.Common.Exceptions;
using AutoMapper;

public class ActivityServiceTests
{
    private readonly ActivityService _service;
    private readonly Mock<IActivityRepository> _activityRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IFixture _fixture;

    public ActivityServiceTests()
    {
        _fixture = new Fixture();
        _activityRepositoryMock = new Mock<IActivityRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new ActivityService(_activityRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task CreateActivityAsync_ShouldReturnActivityDto_WhenActivityIsCreated()
    {
        // Arrange
        var activityCreateDto = _fixture.Create<ActivityCreateDto>();
        var activity = _fixture.Create<Activity>();
        var activityDto = _fixture.Create<ActivityDto>();

        _mapperMock.Setup(m => m.Map<Activity>(activityCreateDto)).Returns(activity);
        _activityRepositoryMock.Setup(repo => repo.AddAsync(activity)).ReturnsAsync(activity);
        _mapperMock.Setup(m => m.Map<ActivityDto>(activity)).Returns(activityDto);

        // Act
        var result = await _service.CreateActivityAsync(activityCreateDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ActivityDto>(result);
        _activityRepositoryMock.Verify(repo => repo.AddAsync(activity), Times.Once);
        _mapperMock.Verify(m => m.Map<Activity>(activityCreateDto), Times.Once);
        _mapperMock.Verify(m => m.Map<ActivityDto>(activity), Times.Once);
    }

    [Fact]
    public async Task CreateActivityAsync_ShouldThrowArgumentException_WhenDtoIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateActivityAsync(null));
    }

    [Fact]
    public async Task DeleteActivityAsync_ShouldCallDeleteAsync_WhenIdIsValid()
    {
        // Arrange
        var id = _fixture.Create<int>();

        // Act
        await _service.DeleteActivityAsync(id);

        // Assert
        _activityRepositoryMock.Verify(repo => repo.DeleteAsync(id), Times.Once);
    }

    [Fact]
    public async Task GetActivityByIdAsync_ShouldReturnActivityDto_WhenActivityExists()
    {
        // Arrange
        var id = _fixture.Create<int>();
        var activity = _fixture.Create<Activity>();
        var activityDto = _fixture.Create<ActivityDto>();

        _activityRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(activity);
        _mapperMock.Setup(m => m.Map<ActivityDto>(activity)).Returns(activityDto);

        // Act
        var result = await _service.GetActivityByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ActivityDto>(result);
        _mapperMock.Verify(m => m.Map<ActivityDto>(activity), Times.Once);
    }

    [Fact]
    public async Task GetActivityByIdAsync_ShouldThrowActivityNotFoundException_WhenActivityDoesNotExist()
    {
        // Arrange
        var id = _fixture.Create<int>();

        _activityRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Activity)null);

        // Act & Assert
        await Assert.ThrowsAsync<ActivityNotFoundException>(() => _service.GetActivityByIdAsync(id));
    }

    [Fact]
    public async Task UpdateActivityAsync_ShouldUpdateActivity_WhenActivityExists()
    {
        // Arrange
        var id = _fixture.Create<int>();
        var activityUpdateDto = _fixture.Create<ActivityUpdateDto>();
        var existingActivity = _fixture.Create<Activity>();
        var updatedActivity = _fixture.Create<Activity>();

        _activityRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingActivity);
        _mapperMock.Setup(m => m.Map<Activity>(activityUpdateDto)).Returns(updatedActivity);

        // Act
        await _service.UpdateActivityAsync(id, activityUpdateDto);

        // Assert
        _activityRepositoryMock.Verify(repo => repo.UpdateAsync(updatedActivity), Times.Once);
    }

    [Fact]
    public async Task UpdateActivityAsync_ShouldThrowArgumentException_WhenDtoIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.UpdateActivityAsync(_fixture.Create<int>(), null));
    }

    [Fact]
    public async Task UpdateActivityAsync_ShouldThrowActivityNotFoundException_WhenActivityDoesNotExist()
    {
        // Arrange
        var id = _fixture.Create<int>();
        var activityUpdateDto = _fixture.Create<ActivityUpdateDto>();

        _activityRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Activity)null);

        // Act & Assert
        await Assert.ThrowsAsync<ActivityNotFoundException>(() => _service.UpdateActivityAsync(id, activityUpdateDto));
    }
}