using System.Globalization;
using System.Net;
using System.Text;
using BusinessLogic.Models.Email;
using BusinessLogic.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Repository.Entity;

namespace BusinessLogic.Services;

/// <summary>
/// High-level email service for generating and queuing domain transactional emails.
/// </summary>
public class EmailService(
    IEmailQueue emailQueue,
    ILogger<EmailService> logger) : IEmailService
{
    private readonly IEmailQueue _emailQueue = emailQueue;
    private readonly ILogger<EmailService> _logger = logger;

    private static string FormatMoney(decimal amount) => amount.ToString("0.00", CultureInfo.InvariantCulture);

    /// <inheritdoc />
    public async Task SendWelcomeEmailAsync(string userEmail, string userName)
    {
        var safeName = WebUtility.HtmlEncode(userName);
        var subject = "Bun venit la GamersKeep! Aventurile tale încep aici";
        var year = DateTime.UtcNow.Year;

        var html = $$"""
        <!DOCTYPE html>
        <html>
        <head>
          <meta charset="utf-8">
          <style>
            body { font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif; background-color: #121214; color: #f0f0f2; margin: 0; padding: 24px; }
            .container { max-width: 600px; margin: 0 auto; background: #1a1a1e; border: 1px solid #2d2d34; border-radius: 12px; padding: 32px; }
            .header { text-align: center; border-bottom: 1px solid #2d2d34; padding-bottom: 24px; margin-bottom: 24px; }
            .logo { font-size: 28px; font-weight: bold; color: #e5a93b; letter-spacing: 1px; }
            h1 { font-size: 22px; color: #ffffff; margin-bottom: 16px; }
            p { font-size: 15px; line-height: 1.6; color: #c4c4cc; }
            .btn { display: inline-block; background-color: #e5a93b; color: #121214; padding: 12px 24px; font-weight: 600; text-decoration: none; border-radius: 8px; margin: 20px 0; }
            .footer { margin-top: 32px; border-top: 1px solid #2d2d34; padding-top: 20px; font-size: 12px; color: #7c7c8a; text-align: center; }
          </style>
        </head>
        <body>
          <div class="container">
            <div class="header">
              <div class="logo">🎲 GAMERSKEEP</div>
            </div>
            <h1>Salut, {{safeName}}!</h1>
            <p>Ne bucurăm să te avem în comunitatea GamersKeep – refugiul pasionaților de board games, jocuri de strategie și TTRPG.</p>
            <p>Contul tău a fost creat cu succes. Poți explora catalogul nostru complet de jocuri, salva adresele preferate de livrare și plasa comenzi cu livrare rapidă.</p>
            <div style="text-align: center;">
              <a href="http://localhost:8080/Catalog" class="btn">Explorează Catalogul</a>
            </div>
            <p>Dacă ai orice întrebare sau vrei recomandări pentru următoarea seară de joc, suntem aici să te ajutăm!</p>
            <div class="footer">
              <p>&copy; {{year}} GamersKeep. Toate drepturile rezervate.</p>
            </div>
          </div>
        </body>
        </html>
        """;

        var text = $"Salut, {userName}!\n\nBun venit la GamersKeep! Contul tău a fost creat cu succes.\nExplorează catalogul nostru: http://localhost:8080/Catalog\n\nEchipa GamersKeep";

        var message = new EmailMessage(userEmail, userName, subject, html, text);
        await _emailQueue.EnqueueAsync(message);
        _logger.LogInformation("Enqueued welcome email for {UserEmail}.", userEmail);
    }

    /// <inheritdoc />
    public async Task SendOrderConfirmationEmailAsync(Order order, string userEmail)
    {
        var subject = $"Confirmare comandă {order.OrderNumber} – GamersKeep";
        var year = DateTime.UtcNow.Year;

        var itemsRows = new StringBuilder();
        foreach (var item in order.Items)
        {
            var lineTotal = FormatMoney(item.UnitPrice * item.Quantity);
            var unitPrice = FormatMoney(item.UnitPrice);
            itemsRows.Append($"""
            <tr>
              <td style="padding: 12px; border-bottom: 1px solid #2d2d34; color: #f0f0f2;">{WebUtility.HtmlEncode(item.ProductName)}</td>
              <td style="padding: 12px; border-bottom: 1px solid #2d2d34; text-align: center; color: #c4c4cc;">{item.Quantity}</td>
              <td style="padding: 12px; border-bottom: 1px solid #2d2d34; text-align: right; color: #c4c4cc;">{unitPrice} RON</td>
              <td style="padding: 12px; border-bottom: 1px solid #2d2d34; text-align: right; color: #f0f0f2; font-weight: 600;">{lineTotal} RON</td>
            </tr>
            """);
        }

        var safeFullName = WebUtility.HtmlEncode(order.ShippingFullName);
        var safeStreet = WebUtility.HtmlEncode(order.ShippingStreet);
        var safeCity = WebUtility.HtmlEncode(order.ShippingCity);
        var safeCounty = WebUtility.HtmlEncode(order.ShippingCounty);
        var safePostalCode = WebUtility.HtmlEncode(order.ShippingPostalCode);
        var safePhone = WebUtility.HtmlEncode(order.ShippingPhone);
        var safeDeliveryName = WebUtility.HtmlEncode(order.DeliveryMethodName);

        var formattedDeliveryCost = FormatMoney(order.DeliveryCost);
        var formattedSubtotal = FormatMoney(order.Subtotal);
        var formattedTotal = FormatMoney(order.Total);

        var html = $$"""
        <!DOCTYPE html>
        <html>
        <head>
          <meta charset="utf-8">
          <style>
            body { font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif; background-color: #121214; color: #f0f0f2; margin: 0; padding: 24px; }
            .container { max-width: 650px; margin: 0 auto; background: #1a1a1e; border: 1px solid #2d2d34; border-radius: 12px; padding: 32px; }
            .header { text-align: center; border-bottom: 1px solid #2d2d34; padding-bottom: 24px; margin-bottom: 24px; }
            .logo { font-size: 28px; font-weight: bold; color: #e5a93b; letter-spacing: 1px; }
            .badge { display: inline-block; background: #28a745; color: #ffffff; padding: 4px 12px; border-radius: 20px; font-size: 13px; font-weight: 600; margin-top: 8px; }
            h1 { font-size: 22px; color: #ffffff; margin-bottom: 8px; }
            p { font-size: 14px; line-height: 1.6; color: #c4c4cc; }
            table { width: 100%; border-collapse: collapse; margin: 20px 0; }
            th { background: #242429; color: #e5a93b; font-size: 13px; text-transform: uppercase; padding: 12px; text-align: left; }
            .summary-box { background: #242429; border-radius: 8px; padding: 16px; margin: 20px 0; }
            .details-grid { display: grid; grid-template-columns: 1fr 1fr; gap: 16px; margin: 20px 0; }
            .details-col { background: #242429; border-radius: 8px; padding: 16px; }
            .details-col h3 { font-size: 14px; color: #e5a93b; margin: 0 0 8px 0; text-transform: uppercase; }
            .footer { margin-top: 32px; border-top: 1px solid #2d2d34; padding-top: 20px; font-size: 12px; color: #7c7c8a; text-align: center; }
          </style>
        </head>
        <body>
          <div class="container">
            <div class="header">
              <div class="logo">🎲 GAMERSKEEP</div>
              <div class="badge">Plată Confirmată ✓</div>
            </div>

            <h1>Îți mulțumim pentru comandă!</h1>
            <p>Comanda ta cu numărul <strong style="color: #ffffff;">{{order.OrderNumber}}</strong> a fost confirmată și este pregătită pentru expediere.</p>

            <div class="details-grid">
              <div class="details-col">
                <h3>Adresă de livrare</h3>
                <p style="margin: 0;">
                  <strong>{{safeFullName}}</strong><br>
                  {{safeStreet}}<br>
                  {{safeCity}}, {{safeCounty}} {{safePostalCode}}<br>
                  Tel: {{safePhone}}
                </p>
              </div>
              <div class="details-col">
                <h3>Metodă de livrare</h3>
                <p style="margin: 0;">
                  <strong>{{safeDeliveryName}}</strong><br>
                  Cost: {{formattedDeliveryCost}} RON<br>
                  Status plată: Confirmată prin Stripe
                </p>
              </div>
            </div>

            <table>
              <thead>
                <tr>
                  <th>Produs</th>
                  <th style="text-align: center;">Cant.</th>
                  <th style="text-align: right;">Preț unitar</th>
                  <th style="text-align: right;">Total</th>
                </tr>
              </thead>
              <tbody>
                {{itemsRows}}
              </tbody>
            </table>

            <div class="summary-box">
              <table style="margin: 0; width: 100%;">
                <tr>
                  <td style="color: #c4c4cc; padding: 4px 0;">Subtotal produse:</td>
                  <td style="text-align: right; color: #f0f0f2; padding: 4px 0;">{{formattedSubtotal}} RON</td>
                </tr>
                <tr>
                  <td style="color: #c4c4cc; padding: 4px 0;">Taxă livrare:</td>
                  <td style="text-align: right; color: #f0f0f2; padding: 4px 0;">{{formattedDeliveryCost}} RON</td>
                </tr>
                <tr style="border-top: 1px solid #3d3d46;">
                  <td style="color: #e5a93b; font-weight: bold; font-size: 16px; padding: 8px 0 0 0;">Total achitat:</td>
                  <td style="text-align: right; color: #e5a93b; font-weight: bold; font-size: 16px; padding: 8px 0 0 0;">{{formattedTotal}} RON</td>
                </tr>
              </table>
            </div>

            <div class="footer">
              <p>Îți mulțumim că ai ales GamersKeep! Pentru detalii despre comenzi, vizitează <a href="http://localhost:8080/Orders" style="color: #e5a93b;">Istoricul Comenzilor</a>.</p>
              <p>&copy; {{year}} GamersKeep.</p>
            </div>
          </div>
        </body>
        </html>
        """;

        var text = $"Confirmare comanda {order.OrderNumber}\nTotal achitat: {formattedTotal} RON\nLivrare catre: {order.ShippingFullName}, {order.ShippingCity}\nMultumim, Echipa GamersKeep!";

        var message = new EmailMessage(userEmail, order.ShippingFullName, subject, html, text);
        await _emailQueue.EnqueueAsync(message);
        _logger.LogInformation("Enqueued order confirmation email for order {OrderNumber} to {UserEmail}.", order.OrderNumber, userEmail);
    }

    /// <inheritdoc />
    public async Task SendOrderCancellationEmailAsync(Order order, string userEmail)
    {
        var subject = $"Comanda {order.OrderNumber} a fost anulată – GamersKeep";
        var year = DateTime.UtcNow.Year;
        var formattedTotal = FormatMoney(order.Total);

        var html = $$"""
        <!DOCTYPE html>
        <html>
        <head>
          <meta charset="utf-8">
          <style>
            body { font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif; background-color: #121214; color: #f0f0f2; margin: 0; padding: 24px; }
            .container { max-width: 600px; margin: 0 auto; background: #1a1a1e; border: 1px solid #2d2d34; border-radius: 12px; padding: 32px; }
            .header { text-align: center; border-bottom: 1px solid #2d2d34; padding-bottom: 24px; margin-bottom: 24px; }
            .logo { font-size: 28px; font-weight: bold; color: #e5a93b; letter-spacing: 1px; }
            h1 { font-size: 22px; color: #e74c3c; margin-bottom: 16px; }
            p { font-size: 15px; line-height: 1.6; color: #c4c4cc; }
            .footer { margin-top: 32px; border-top: 1px solid #2d2d34; padding-top: 20px; font-size: 12px; color: #7c7c8a; text-align: center; }
          </style>
        </head>
        <body>
          <div class="container">
            <div class="header">
              <div class="logo">🎲 GAMERSKEEP</div>
            </div>
            <h1>Comanda {{order.OrderNumber}} a fost anulată</h1>
            <p>Comanda ta în valoare de <strong>{{formattedTotal}} RON</strong> a fost anulată cu succes.</p>
            <p>Dacă plata fusese confirmată, suma achitată va fi rambursată prin Stripe în contul tău conform termenelor bancare.</p>
            <p>Stocul produselor a fost deblocat automat.</p>
            <div class="footer">
              <p>&copy; {{year}} GamersKeep.</p>
            </div>
          </div>
        </body>
        </html>
        """;

        var text = $"Comanda {order.OrderNumber} a fost anulata. Daca ai achitat prin card, rambursarea va fi procesata automat. Echipa GamersKeep";

        var message = new EmailMessage(userEmail, order.ShippingFullName, subject, html, text);
        await _emailQueue.EnqueueAsync(message);
        _logger.LogInformation("Enqueued order cancellation email for order {OrderNumber} to {UserEmail}.", order.OrderNumber, userEmail);
    }
}
