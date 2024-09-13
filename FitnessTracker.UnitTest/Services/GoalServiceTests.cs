using AutoFixture;
using Moq;
using FitnessTracker.Application.DTOs;
using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Interfaces;
using FitnessTracker.Common.Exceptions;
using AutoMapper;

public class GoalServiceTests
{
    private readonly GoalService _service;
    private readonly Mock<IGoalRepository> _goalRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IFixture _fixture;

    public GoalServiceTests()
    {
        _fixture = new Fixture();
        _goalRepositoryMock = new Mock<IGoalRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new GoalService(_goalRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task AddGoalAsync_ShouldReturnGoalDto_WhenGoalIsAdded()
    {
        // Arrange
        var goalCreateDto = _fixture.Create<GoalCreateDto>();
        var goal = _fixture.Create<Goal>();
        var goalDto = _fixture.Create<GoalDto>();

        _mapperMock.Setup(m => m.Map<Goal>(goalCreateDto)).Returns(goal);
        _goalRepositoryMock.Setup(repo => repo.AddAsync(goal)).ReturnsAsync(goal);
        _mapperMock.Setup(m => m.Map<GoalDto>(goal)).Returns(goalDto);

        // Act
        var result = await _service.AddGoalAsync(goalCreateDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<GoalDto>(result);
        _goalRepositoryMock.Verify(repo => repo.AddAsync(goal), Times.Once);
        _mapperMock.Verify(m => m.Map<Goal>(goalCreateDto), Times.Once);
        _mapperMock.Verify(m => m.Map<GoalDto>(goal), Times.Once);
    }

    [Fact]
    public async Task AddGoalAsync_ShouldThrowArgumentException_WhenDtoIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.AddGoalAsync(null));
    }

    [Fact]
    public async Task GetGoalByIdAsync_ShouldReturnGoalDto_WhenGoalExists()
    {
        // Arrange
        var id = _fixture.Create<int>();
        var goal = _fixture.Create<Goal>();
        var goalDto = _fixture.Create<GoalDto>();

        _goalRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(goal);
        _mapperMock.Setup(m => m.Map<GoalDto>(goal)).Returns(goalDto);

        // Act
        var result = await _service.GetGoalByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<GoalDto>(result);
        _goalRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        _mapperMock.Verify(m => m.Map<GoalDto>(goal), Times.Once);
    }

    [Fact]
    public async Task GetGoalByIdAsync_ShouldThrowGoalNotFoundException_WhenGoalDoesNotExist()
    {
        // Arrange
        var id = _fixture.Create<int>();

        _goalRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Goal)null);

        // Act & Assert
        await Assert.ThrowsAsync<GoalNotFoundException>(() => _service.GetGoalByIdAsync(id));
    }

    [Fact]
    public async Task UpdateGoalAsync_ShouldUpdateGoal_WhenGoalExists()
    {
        // Arrange
        var goalUpdateDto = _fixture.Create<GoalUpdateDto>();
        var goal = _fixture.Create<Goal>();

        _mapperMock.Setup(m => m.Map<Goal>(goalUpdateDto)).Returns(goal);

        // Act
        await _service.UpdateGoalAsync(goalUpdateDto);

        // Assert
        _goalRepositoryMock.Verify(repo => repo.UpdateAsync(goal), Times.Once);
    }

    [Fact]
    public async Task UpdateGoalAsync_ShouldThrowArgumentException_WhenDtoIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.UpdateGoalAsync(null));
    }
}