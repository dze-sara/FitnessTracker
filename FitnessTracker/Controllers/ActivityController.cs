using FitnessTracker.Application.DTOs;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Queries;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be greater than zero.");
            }

            var activity = await _activityService.GetActivityByIdAsync(id);
            return Ok(activity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ActivityQuery query)
        {
            if (!query.IsValid())
            {
                return BadRequest(new { message = "At least one query parameter must be provided." });
            }

            var activities = await _activityService.GetActivitiesAsync(query);
            return Ok(activities);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ActivityCreateDto newActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activity = await _activityService.CreateActivityAsync(newActivity);
            return CreatedAtAction(nameof(GetAll), new { id = activity.Id }, activity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ActivityUpdateDto updatedActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedActivity.Id)
            {
                return BadRequest("Activity ID mismatch.");
            }

            await _activityService.UpdateActivityAsync(id, updatedActivity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid Id." });
            }

            await _activityService.DeleteActivityAsync(id);
            return NoContent();
        }
    }
}
