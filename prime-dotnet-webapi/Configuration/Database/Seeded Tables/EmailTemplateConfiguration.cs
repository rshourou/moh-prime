using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class EmailTemplateConfiguration : SeededTable<EmailTemplate>
    {
        public static readonly DateTime EnrolleeRenewalRequiredUpdateAug17th2022 = new(2022, 8, 17, 8, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime MultipleEmailContentUpdateNov30th2022 = new(2022, 11, 30, 8, 0, 0, DateTimeKind.Utc);

        public override IEnumerable<EmailTemplate> SeedData
        {
            get
            {
                return new[] {
                    new EmailTemplate { Id = 1,  EmailType = EmailTemplateType.EnrolleeStatusChange,                   Template = "<p> Your PRIME application status has changed since you last viewed it. Please click <a href=\"@Model.Url\">here</a> to log into PRIME and view your status. </p>", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 2,  EmailType = EmailTemplateType.CommunityPracticeNotification,          Template = "<style> .underline { text-decoration: underline; } .list-item-mb { margin-bottom: 0.75rem; } </style>To: PharmaNet administrator (person responsible for coordinating PharmaNet access):<br><br>@Model.EnrolleeFullName has been approved for access to PharmaNet.<br><br><strong> To set up their access, you must forward this notification and the information below to <span class=\"underline\">whoever sets up PharmaNet user accounts. This is usually your PharmaNet software vendor but can also be someone like an IT department or authorized individual</span>. </strong><br><br><ol><li class=\"list-item-mb\"> Name of the community health practice: </li><li class=\"list-item-mb\"> Practice address: </li><li class=\"list-item-mb\"> PharmaNet site ID, if you have it:</li><li class=\"list-item-mb\"> If @Model.EnrolleeFullName accesses PharmaNet on behalf of another user, please ensure your PharmaNet software vendor knows who the other users are, and those users enrol in PRIME.<br><br></li></ol> Your software vendor can find @Model.EnrolleeFullName's details by following this link: <a href=\"@Model.TokenUrl\">@Model.TokenUrl</a> <strong>This link will expire in 10 days.</strong><br><br>Thank you,<br><br>PRIME Support <br>1-844-397-7463<br><a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>", ModifiedDate = MultipleEmailContentUpdateNov30th2022, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = MultipleEmailContentUpdateNov30th2022},
                    new EmailTemplate { Id = 3,  EmailType = EmailTemplateType.CommunityPharmacyNotification,          Template = "To: Whom it may concern, <br> <br> @Model.EnrolleeFullName has been approved for <strong>Community Pharmacy Access to PharmaNet.</strong> They can now be set up with their PharmaNet Access account in your local software. You must include their <strong>Global PharmaNet ID (GPID)</strong> on their account profile. You can access their GPID via this link below. <br> <br> <a href=\"@Model.TokenUrl\">@Model.TokenUrl</a> <br> <strong>This link will expire after @Model.ExpiresInDays days</strong>. <br> <br> Thank you.", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 4,  EmailType = EmailTemplateType.HealthAuthorityNotification,            Template = "To: PharmNet Access administrator <br> <br> @Model.EnrolleeFullName has been approved for <strong>Health Authority Access to PharmaNet.</strong> <br> They can now be set up with their PharmaNet Access account in your local software. You must include their <strong>Global PharmaNet ID (GPID)</strong> on their account profile. <br> You can access their GPID via this link here. <br> <a href=\"@Model.TokenUrl\">@Model.TokenUrl</a> <br> <strong>This link will expire after @Model.ExpiresInDays days</strong>.", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 5,  EmailType = EmailTemplateType.RemoteUserNotification,                 Template = "The Ministry of Health has been notified that you require Remote Access at <br> Organization Name: @Model.OrganizationName <br> Site Address: @Model.SiteStreetAddress, @Model.SiteCity <br> <br> To complete your approval for Remote Access, please ensure you have indicated you require Remote Access on your profile at <a href=\"@Model.PrimeUrl\">@Model.PrimeUrl</a>.", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 6,  EmailType = EmailTemplateType.RemoteUserUpdatedNotification,          Template = "@{ var pecText = string.IsNullOrWhiteSpace(Model.SitePec) ? \"Not Assigned\" : Model.SitePec; } <p> Notification: The list of Remote Practitioners at @Model.SiteStreetAddress of @Model.OrganizationName (PEC: @pecText) has been updated. The Remote Practitioners at this site are: </p> <h2 class=\"mb-2\">Remote Users</h2> @foreach (var name in Model.RemoteUserNames) { <div class=\"ml-2 mb-2\"> <h5>Name</h5> <span class=\"ml-2\">@name</span> </div> } <h2 class=\"mb-2\">Site Information</h2> <p> See the attached registration and organization agreement for more information. @if (!string.IsNullOrWhiteSpace(Model.DocumentUrl)) { @(\"To access the Business Licence, click this\") <a href=\"@Model.DocumentUrl\" target=\"_blank\">link</a>@(\".\") } </p>", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 7,  EmailType = EmailTemplateType.HIBCSiteSubmission,                     Template = "<p>@Model.DoingBusinessAs with PEC/SiteID @Model.Pec has been approved by the Ministry of Health for PharmaNet access. Please notify the PharmaNet software vendor for this site and complete any remaining tasks to activate the site.</p><p>Thank you.</p><p>PRIME Support Team.</p>", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 8,  EmailType = EmailTemplateType.EnrolleeRenewalRequired,                Template = "@{ var renewalDate = Model.RenewalDate.Date.ToShortDateString(); } <p>Dear @Model.EnrolleeName,</p><p>It is time to renew your PRIME enrolment. If you still need PharmaNet to care for patients, you must ensure that your PRIME profile is current by reviewing and updating every year. To renew, log in to your PRIME profile, update your information as needed, and read and sign the terms of access.</p><p>Please renew by @renewalDate.</p><ol><li>Log in to PRIME <a href='@Model.PrimeUrl'>@Model.PrimeUrl</a>.</li><li>Click <b>Renew or Update Information</b> (top of screen).</li><li>On the PRIME Profile screen, review your information.<ol style='list-style-type:lower-alpha;'><li>Select the edit button to revise sections that are out of date. Click <strong>Continue</strong> at the bottom of updated screens to save changes.</li><li>Once changes are saved, <strong>certify and submit</strong> at the bottom of the PRIME Profile page.</li></ol></li><li>If instructed to go on, click <b>Continue</b></li><li>Most renewals will be approved automatically. If you are notified that your renewal is approved, go to the next page to review and accept the PharmaNet user terms of access. This step completes renewal. Note that the terms of access may have changed, so please read carefully.</li><p>In some cases, renewals are sent for review. If your renewal is sent for review, you will either be contacted by the PRIME Support team, or you will be notified to log in to PRIME to complete the remaining steps.</p><li>You will next be prompted to share your renewal approval with the person or team in your workplace who sets up PharmaNet accounts. You do this by entering their email address(es) and sending. <strong>Only share the renewal approval if:</strong><ol style='list-style-type:lower-alpha;'><li>You changed workplaces or care setting (new clinic, health authority, etc.) since you last updated your account, or you did not share the approval notification with your PharmaNet administrator earlier, and/or</li><li>You previously enrolled as an RN, RPN, LPN or midwife, and have since been issued a PharmaNet ID by BCCNM. Your access type may have changed, and the people in your workplace who set up PharmaNet access need to know this.</li></ol></li></ol><p>Thank you for renewing your PRIME enrolment,</p><br>PRIME Support <br>1-844-397-7463<br><a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>", ModifiedDate = MultipleEmailContentUpdateNov30th2022, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = MultipleEmailContentUpdateNov30th2022},
                    new EmailTemplate { Id = 9,  EmailType = EmailTemplateType.BusinessLicenceUpload,                  Template = "<p> A user has uploaded business licence to their PharmaNet site registration. @if (!string.IsNullOrWhiteSpace(Model.Url)) { @(\"To access the Business Licence, click this\") <a href=\"@Model.Url\" target=\"_blank\">link</a>@(\".\") } </p>", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 10, EmailType = EmailTemplateType.EnrolleeRenewalPassed,                  Template = "Dear @Model.EnrolleeName,<br /><br /><p>Your PRIME renewal is overdue.</p><p><strong>Once a year, you must ensure that your PRIME profile is current.</strong> If you still need access to PharmaNet to care for patients, renew your PRIME enrolment now by logging in to PRIME at <a href='@Model.PrimeUrl'>@Model.PrimeUrl</a>. Update your information as needed, submit, and then read and sign the terms of access.</p><p>You may also wish to confirm that your email accepts messages from PRIME, particularly if they are going to your email junk folder (e.g., the previous notices to renew).</p><br>Thank you,<br><br>PRIME Support <br>1-844-397-7463<br><a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>", ModifiedDate = MultipleEmailContentUpdateNov30th2022, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = MultipleEmailContentUpdateNov30th2022},
                    new EmailTemplate { Id = 11, EmailType = EmailTemplateType.SiteApprovedPharmaNetAdministrator,     Template = "<p> Your site registration has been approved. The site must now be set up and activated in PharmaNet. Your PharmaNet software vendor will be notified when the site has been activated, and you will hear from them when you can start to use PharmaNet. </p> <p> Individuals who will be accessing PharmaNet at your site should enrol in PRIME now if they have not already done so. For more information, please visit <a href=\"https://www.gov.bc.ca/pharmanet/PRIME\" target=\"_blank\">https://www.gov.bc.ca/pharmanet/PRIME</a>. [for private community practice only: If you have registered any physicians or nurse practitioners for remote access to PharmaNet, they must enroll in PRIME before they use remote access, which they can do here: <a href=\"https://pharmanetenrolment.gov.bc.ca\" target=\"_blank\">https://pharmanetenrolment.gov.bc.ca</a>. You must not permit remote use of PharmaNet until these users are approved in PRIME.] </p> <p> If you have any questions or concerns, please phone 1-844-397-7463 or email <a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMESupport@gov.bc.ca</a>. </p> <p> Thank you. </p>", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 12, EmailType = EmailTemplateType.SiteApprovedSigningAuthority,           Template = "<p> The site you registered in PRIME, @Model.DoingBusinessAs, has been approved by the Ministry of Health. Your SiteID is @Model.Pec. </p> <p> Health Insurance BC has been notified of the site’s approval and will contact your software vendor. Your vendor will complete any remaining setup for your site and may contact you or the PharmaNet Administrator at your site. </p> <p> If you need to update any information in PRIME regarding your site, you may log in at any time using your mobile BC Services Card. If you have any questions or concerns, please phone 1-844-397-7463 or email PRIMESupport@gov.bc.ca. </p> <p> Thank you. </p>", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 13, EmailType = EmailTemplateType.SiteRegistrationSubmission,             Template = "<p> A new PharmaNet site registration has been received. See the attached registration and organization agreement for more information. @if (!string.IsNullOrWhiteSpace(Model.Url)) { @(\"To access the Business Licence, click this\") <a href=\"@Model.Url\" target=\"_blank\">link</a>@(\".\") } </p>", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 14, EmailType = EmailTemplateType.OrganizationClaimApprovalNotification,  Template = "Your claim of the organization @Model.OrganizationName, of which the site with site ID/PEC @Model.ProvidedSiteId is part of, has been approved.  You now have access to site registration for this organization.", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 15, EmailType = EmailTemplateType.SiteReviewedNotification,               Template = "A PRIME Admin has reviewed the site registration for PEC/SiteID# @Model.Pec. @(!string.IsNullOrWhiteSpace(Model.Note) ? $\"The following notes were added: {Model.Note}\" : \"\")", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 16, EmailType = EmailTemplateType.SiteActiveBeforeRegistrationSubmission, Template = "Thank you for registering your site (SiteID: @Model.Pec) in PRIME. If you need to update any site information in PRIME, you may log in at any time using your mobile BC Services Card. If you have any questions, please phone 1 - 844 - 397 - 7463 or email PRIMESupport@@gov.bc.ca. Thank you.", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 17, EmailType = EmailTemplateType.PaperEnrolleeSubmission,                Template = "Your request for PharmaNet access has been approved and recorded in PRIME. When it is possible for you to do so, you must enrol in PRIME using your mobile BC Services Card. <br> <br> <strong> Your temporary GPID is @Model.GPID. </strong> <br> <br> The first time you log into PRIME you should be asked if you have previously received permission to access PharmaNet via an offline process. If you do not see this prompt, please stop your enrollment and contact <a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 18, EmailType = EmailTemplateType.EnrolleeUnsignedToa,                    Template = "Dear @Model.EnrolleeName, <br> <br><p>Your PharmaNet Terms of Access must be accepted in PRIME before your enrolment is complete. Please log in to PRIME and accept your terms of access now.</p><p>You can access PRIME here <a href='@Model.PrimeUrl'>@Model.PrimeUrl</a>.  If you are not directed to the user terms of access page, you can reach it by selecting Terms of Access from the menu on the left-hand side of the PRIME Profile page.</p><br>Thank you,<br><br>PRIME Support <br>1-844-397-7463<br><a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>", ModifiedDate = MultipleEmailContentUpdateNov30th2022, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = MultipleEmailContentUpdateNov30th2022},
                    new EmailTemplate { Id = 19, EmailType = EmailTemplateType.EnrolleeAbsenceNotification,            Template = "This is an automated generated email from PRIME. <br> <br> @(Model.FirstName + \" \" +  Model.LastName + \" is going to be absent \") @if (Model.End.HasValue) {@(\"from \" + Model.Start.ToShortDateString() + \" to \" + Model.End.Value.ToShortDateString() + \".  Please consider deactivating the user during this period. Any access during this period by the user will be considered as an unauthorized access.\")} else {@(\"indefinitely, starting \" + Model.Start.ToShortDateString() + \". Please deactivate the user on the start date. Any access during this period by the user will be considered as an unauthorized access.\")}", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 20, EmailType = EmailTemplateType.ForcedRenewalNotification,              Template = "Hello @Model.EnrolleeName, <br> <br> You are receiving this message as your PharmaNet user status has changed from on-behalf-of user to independent user following changes to legislation governing access to PharmaNet. <br> <br> Being an independent PharmaNet user means you will now access PharmaNet as yourself instead of on behalf of another practitioner. <br> <br> Log back in to PRIME by @Model.RenewalDate.ToString(\"d MMMM yyyy\") to confirm your profile information and review and accept the user terms of access for independent users. These will automatically be presented to you. This is a requirement for you to maintain access to PharmaNet, as the new terms of access are quite different from those you may have accepted earlier. <br> <br> For information about PRIME, visit the <a href=\"@Model.PrimeUrl\">PRIME web page</a>.  If you have questions or difficulties using PRIME, please contact us at the email address below.<br> <br> Thank you, <br> <br> PRIME Support<br><a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new EmailTemplate { Id = 21, EmailType = EmailTemplateType.ForcedRenewalPassedNotification,        Template = "Hello @Model.EnrolleeName, <br> <br> You are receiving this message as your PharmaNet user status has changed from on-behalf-of user to independent user following changes to legislation governing access to PharmaNet. <br> <br> Being an independent user of PharmaNet means you now will access PharmaNet as yourself instead of on behalf of another practitioner. <br> <br> <b>This is your last day</b> to log back in to PRIME to confirm your profile information and review and accept the new user terms of access for independent users. These will automatically be presented to you. This is a requirement for you to maintain access to PharmaNet. <br> <br> For information about PRIME, visit the <a href=\"@Model.PrimeUrl\">PRIME web page</a>.  If you have questions or difficulties using PRIME, please contact us at the email address below.<br> <br> Thank you, <br> <br> PRIME Support<br><a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>", ModifiedDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                };
            }
        }
        public override void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder
                .HasIndex("EmailType")
                .IsUnique();

            builder.HasData(SeedData);
        }
    }
}
