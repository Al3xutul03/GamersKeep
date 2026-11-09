using BusinessLogic.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

/// <summary>
/// Controller listening for incoming payment events from Stripe.
/// Processes checkout completions, updates order status to Paid,
/// decreases stock atomically, and clears the user's cart.
/// </summary>
[ApiController]
[Route("api/stripe/webhook")]
[AllowAnonymous]
[IgnoreAntiforgeryToken]
public class StripeWebhookController(
    IStripeWebhookService webhookService,
    ILogger<StripeWebhookController> logger) : ControllerBase
{
    private readonly IStripeWebhookService _webhookService = webhookService;
    private readonly ILogger<StripeWebhookController> _logger = logger;

    [HttpPost]
    public async Task<IActionResult> Handle()
    {
        string json;
        using (var reader = new StreamReader(HttpContext.Request.Body))
        {
            json = await reader.ReadToEndAsync();
        }

        var signatureHeader = Request.Headers["Stripe-Signature"].FirstOrDefault();

        _logger.LogInformation("Received Stripe webhook call. Payload length: {Length}", json.Length);

        var result = await _webhookService.HandleWebhookEventAsync(json, signatureHeader);

        switch (result.Status)
        {
            case WebhookResultStatus.InvalidSignature:
                _logger.LogWarning("Rejecting Stripe webhook due to invalid signature: {Message}", result.Message);
                return BadRequest(new { error = result.Message });

            case WebhookResultStatus.Error:
                _logger.LogError("Stripe webhook processing error: {Message}", result.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = result.Message });

            default:
                _logger.LogInformation("Stripe webhook handled successfully with status {Status}: {Message}", result.Status, result.Message);
                return Ok(new { status = result.Status.ToString(), message = result.Message });
        }
    }
}
