using FitnessTracker.Application.DTOs;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoalController : Controller
    {
        private readonly IGoalService _goalService;

        public GoalController(IGoalService goalService)
        {
            _goalService = goalService ?? throw new ArgumentNullException(nameof(goalService));
        }

        /// <summary>
        /// Creates a new goal.
        /// </summary>
        /// <param name="goalCreateDto">The goal data to create. Must be a valid goal data.</param>
        /// <response code="201">Returns the created goal.</response>
        /// <response code="400">If the goal data is invalid or missing.</response>
        [HttpPost]
        public async Task<IActionResult> AddGoal([FromBody] GoalCreateDto goalCreateDto)
        {
            if (goalCreateDto == null)
            {
                return BadRequest("Goal data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!goalCreateDto.Validate())
            {
                return BadRequest("Invalid goal data.");
            }

            var result = await _goalService.AddGoalAsync(goalCreateDto);
            return CreatedAtAction(nameof(GetGoalById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Retrieves a list of goals based on the provided query parameters.
        /// </summary>
        /// <param name="query">Query parameters to filter goals.</param>
        /// <response code="200">Returns a list of goals matching the query parameters.</response>
        [HttpGet]
        public async Task<IActionResult> GetGoals([FromQuery]GoalQuery query)
        {
            var goal = await _goalService.GetGoalsAsync(query);
            return Ok(goal);
        }

        /// <summary>
        /// Retrieves a specific goal by its ID.
        /// </summary>
        /// <param name="id">The ID of the goal to retrieve.</param>
        /// <response code="200">Returns the goal with the specified ID.</response>
        /// <response code="404">If no goal with the specified ID is found.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoalById(int id)
        {
            var goal = await _goalService.GetGoalByIdAsync(id);
            return Ok(goal);
        }

        /// <summary>
        /// Updates an existing goal.
        /// </summary>
        /// <param name="id">The ID of the goal to update.</param>
        /// <param name="goal">The goal data to update.</param>
        /// <response code="204">If the goal was updated successfully.</response>
        /// <response code="400">If the goal ID does not match or the data is invalid.</response>
        /// <response code="404">If no goal with the specified ID is found.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGoal(int id, [FromBody] GoalUpdateDto goal)
        {
            if (id != goal.Id)
            {
                return BadRequest("Goal ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!goal.Validate())
            {
                return BadRequest("Invalid goal data.");
            }

            await _goalService.UpdateGoalAsync(goal);
            return NoContent();
        }
    }
}
