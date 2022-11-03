using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IEmailService
    {
        Task SendBusinessLicenceUploadedAsync(CommunitySite site);
        Task SendProvisionerLinkAsync(string[] recipientEmails, EnrolmentCertificateAccessToken token, int careSettingCode);
        Task SendReminderEmailAsync(int enrolleeId);
        Task SendRemoteUserNotificationsAsync(CommunitySite site, IEnumerable<RemoteUser> remoteUsers);
        Task SendRemoteUsersUpdatedAsync(CommunitySite site);
        Task SendSiteApprovedHIBCAsync(CommunitySite site);
        Task SendSiteApprovedPharmaNetAdministratorAsync(CommunitySite site);
        Task SendSiteApprovedSigningAuthorityAsync(CommunitySite site);
        Task SendSiteRegistrationSubmissionAsync(int siteId, int businessLicenceId, CareSettingType careSettingCode);
        Task SendHealthAuthoritySiteRegistrationSubmissionAsync(int siteId);
        Task SendSiteReviewedNotificationAsync(int siteId, string note);
        Task SendSiteActiveBeforeRegistrationAsync(int siteId, string signingAuthorityEmail);
        /// <summary>
        /// Return IDs of enrollees emailed
        /// </summary>
        Task<IEnumerable<int>> SendEnrolleeRenewalEmails();
        Task SendOrgClaimApprovalNotificationAsync(OrganizationClaim organizationClaim);
        Task<int> UpdateEmailLogStatuses(int limit);
        Task SendPaperEnrolmentSubmissionEmailAsync(int enrolleeId);
        /// <summary>
        /// Return IDs of enrollees emailed
        /// </summary>
        Task<IEnumerable<int>> SendEnrolleeUnsignedToaReminderEmails();
        Task SendEnrolleeAbsenceNotificationEmailAsync(int enrolleeId, EnrolleeAbsenceViewModel absence, string email);
    }
}
