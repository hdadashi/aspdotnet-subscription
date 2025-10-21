using MediatR;
using Microsoft.AspNetCore.Mvc;
using Subscription.Application.Features.Commands.ActivateSubscription;
using Subscription.Application.Features.Commands.DeactivateSubscription;
using Subscription.Application.Features.Queries.GetPlans;
using Subscription.Application.Features.Queries.GetUserSubscription;
using Subscription.Domain.Common;

namespace Subscription.API.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class SubscriptionController(IMediator mediator) : ControllerBase
    {
        // GET v1/api/subscription/plans
        [HttpGet("plans")]
        public async Task<IActionResult> GetPlans()
        {
            var result = await mediator.Send(new GetPlansQuery());
            return Ok(result);
        }

        // GET v1/api/subscription/users/{userId}/subscription
        [HttpGet("users/{userId:guid}/subscription")]
        public async Task<IActionResult> GetUserSubscription(Guid userId)
        {
            var result = await mediator.Send(new GetUserSubscriptionQuery(userId));
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST v1/api/subscription/users/{userId}/subscription/activate
        [HttpPost("users/{userId:guid}/subscription/activate")]
        public async Task<IActionResult> ActivateSubscription(Guid userId, [FromBody] ActivateRequest request)
        {
            await mediator.Send(new ActivateSubscriptionCommand(userId, request.PlanId));
            return NoContent();
        }

        // POST v1/api/subscription/users/{userId}/subscription/deactivate
        [HttpPost("users/{userId:guid}/subscription/deactivate")]
        public async Task<IActionResult> DeactivateSubscription(Guid userId)
        {
            await mediator.Send(new DeactivateSubscriptionCommand(userId));
            return NoContent();
        }
    }
}