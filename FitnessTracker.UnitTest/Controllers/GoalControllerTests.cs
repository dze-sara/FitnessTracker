using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FitnessTracker.API.Controllers;
using FitnessTracker.Application.DTOs;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Queries;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class GoalControllerTests
{
    private readonly GoalController _controller;
    private readonly Mock<IGoalService> _goalServiceMock;
    private readonly IFixture _fixture;

    public GoalControllerTests()
    {
        _fixture = new Fixture();
        _goalServiceMock = new Mock<IGoalService>();
        _controller = new GoalController(_goalServiceMock.Object);
    }

    [Fact]
    public async Task AddGoal_ShouldReturnCreatedAtAction_WhenGoalIsCreated()
    {
        // Arrange
        var goalCreateDto = _fixture.Create<GoalCreateDto>();
        var goalDto = _fixture.Create<GoalDto>();
        _goalServiceMock.Setup(service => service.AddGoalAsync(goalCreateDto))
                        .ReturnsAsync(goalDto);

        // Act
        var result = await _controller.AddGoal(goalCreateDto) as CreatedAtActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(GoalController.GetGoalById), result.ActionName);
        Assert.Equal(goalDto, result.Value);
    }

    [Fact]
    public async Task AddGoal_ShouldReturnBadRequest_WhenGoalDataIsNull()
    {
        // Act
        var result = await _controller.AddGoal(null) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Goal data is required.", result.Value);
    }

    [Fact]
    public async Task GetGoals_ShouldReturnOk_WhenGoalsAreRetrieved()
    {
        // Arrange
        var query = _fixture.Create<GoalQuery>();
        var goalDtos = _fixture.CreateMany<GoalDto>(5);
        _goalServiceMock.Setup(service => service.GetGoalsAsync(query))
                        .ReturnsAsync(goalDtos);

        // Act
        var result = await _controller.GetGoals(query) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(goalDtos, result.Value);
    }

    [Fact]
    public async Task GetGoalById_ShouldReturnOk_WhenGoalExists()
    {
        // Arrange
        var id = _fixture.Create<int>();
        var goalDto = _fixture.Create<GoalDto>();
        _goalServiceMock.Setup(service => service.GetGoalByIdAsync(id))
                        .ReturnsAsync(goalDto);

        // Act
        var result = await _controller.GetGoalById(id) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(goalDto, result.Value);
    }

    [Fact]
    public async Task UpdateGoal_ShouldReturnNoContent_WhenGoalIsUpdated()
    {
        // Arrange
        var id = _fixture.Create<int>();
        var goalUpdateDto = _fixture.Build<GoalUpdateDto>().With(x => x.Id, id).Create();

        // Act
        var result = await _controller.UpdateGoal(id, goalUpdateDto) as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateGoal_ShouldReturnBadRequest_WhenIdMismatch()
    {
        // Arrange
        var id = _fixture.Create<int>();
        var goalUpdateDto = _fixture.Create<GoalUpdateDto>();
        goalUpdateDto.Id = id + 1; // Ensure ID mismatch

        // Act
        var result = await _controller.UpdateGoal(id, goalUpdateDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Goal ID mismatch.", result.Value);
    }

    [Fact]
    public async Task UpdateGoal_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        var id = _fixture.Create<int>();
        var goalUpdateDto = _fixture.Build<GoalUpdateDto>().With(x => x.Id, id).Create();
        _controller.ModelState.AddModelError("Error", "Model state is invalid");

        // Act
        var result = await _controller.UpdateGoal(id, goalUpdateDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(ModelValidationState.Invalid, _controller.ModelState.ValidationState);
    }
}