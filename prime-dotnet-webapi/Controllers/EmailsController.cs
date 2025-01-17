using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Configuration.Auth;
using Prime.Services;
using Prime.ViewModels.Emails;
using System.Collections.Generic;
using Prime.Models.Api;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : PrimeControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IBusinessEventService _businessEventService;

        public EmailsController(
            IEmailService emailService,
            IEmailTemplateService emailTemplateService,
            IBusinessEventService businessEventService)
        {
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
            _businessEventService = businessEventService;
        }

        // POST: api/Emails/management/statuses
        /// <summary>
        /// Update all logged email statuses sent using the CHES email service
        /// </summary>
        /// <param name="limit"></param>
        [HttpPost("management/statuses", Name = nameof(UpdateEmailLogStatuses))]
        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEmailLogStatuses([FromQuery] int limit = 10)
        {
            var total = await _emailService.UpdateEmailLogStatuses(limit);

            return Ok($"Updated {limit} of {total}.");
        }

        // POST: api/Emails/management/enrollees/renewal
        /// <summary>
        /// Send enrollee renewal reminder emails
        /// </summary>
        [HttpPost("management/enrollees/renewal", Name = nameof(SendEnrolleeRenewalEmails))]
        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SendEnrolleeRenewalEmails()
        {
            var enrolleesEmailed = await _emailService.SendEnrolleeRenewalEmails();
            int numEmailed = 0;
            foreach (var enrolleeId in enrolleesEmailed)
            {
                await _businessEventService.CreateEmailEventAsync(enrolleeId, "Notified enrollee to renew");
                numEmailed++;
            }

            return Ok($"Sent {numEmailed} renewal emails.");
        }

        // POST: api/Emails/management/enrollees/unsigned-toa
        /// <summary>
        /// Send enrollee unsigned TOA reminder emails
        /// </summary>
        [HttpPost("management/enrollees/unsigned-toa", Name = nameof(SendEnrolleeUnsignedToaReminderEmails))]
        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SendEnrolleeUnsignedToaReminderEmails()
        {
            var enrolleesEmailed = await _emailService.SendEnrolleeUnsignedToaReminderEmails();
            foreach (var enrolleeId in enrolleesEmailed)
            {
                await _businessEventService.CreateEmailEventAsync(enrolleeId, "Notified enrollee to sign TOA");
            }

            return NoContent();
        }


        // Email Templates

        // GET: api/emails/management/templates
        /// <summary>
        /// Get email templates
        /// </summary>
        [HttpGet("management/templates", Name = nameof(GetEmailTemplates))]
        [Authorize(Roles = Roles.ViewEnrollee + "," + Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<EmailTemplateListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEmailTemplates()
        {
            var templates = await _emailTemplateService.GetEmailTemplatesAsync();
            return Ok(templates);
        }

        // GET: api/emails/management/templates/1
        /// <summary>
        /// Get email template by ID
        /// </summary>
        /// <param name="emailTemplateId"></param>
        [HttpGet("management/templates/{emailTemplateId}", Name = nameof(GetEmailTemplate))]
        [Authorize(Roles = Roles.ViewEnrollee + "," + Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<EmailTemplateViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEmailTemplate(int emailTemplateId)
        {
            var template = await _emailTemplateService.GetEmailTemplateAsync(emailTemplateId);
            return Ok(template);
        }

        // PUT: api/emails/management/templates/1
        /// <summary>
        /// Update email template
        /// </summary>
        /// <param name="emailTemplateId"></param>
        /// <param name="template"></param>
        [HttpPut("management/templates/{emailTemplateId}", Name = nameof(UpdateEmailTemplate))]
        [Authorize(Roles = Roles.PrimeMaintenance)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EmailTemplateViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEmailTemplate(int emailTemplateId, FromBodyText template)
        {
            if (!await _emailTemplateService.EmailTemplateExistsAsync(emailTemplateId))
            {
                return NotFound($"Email Template not found with id {emailTemplateId}");
            }

            var emailTemplate = await _emailTemplateService.UpdateEmailTemplateAsync(emailTemplateId, template);
            return Ok(emailTemplate);
        }
    }
}
