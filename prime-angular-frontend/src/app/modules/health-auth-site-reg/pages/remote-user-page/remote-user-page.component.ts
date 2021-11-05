import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { noop, of } from 'rxjs';

import { CollegeConfig, LicenseConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AddressLine } from '@lib/models/address.model';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RemoteUser } from '@lib/models/remote-user.model';
import { RemoteUserCertification } from '@lib/models/remote-user-certification.model';
import { FormUtilsService } from '@core/services/form-utils.service';
import { NoContent } from '@core/resources/abstract-resource';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';

import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { SiteRoutes } from '@registration/site-registration.routes';

import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';
import { HealthAuthoritySiteFormStateService } from '@health-auth/shared/services/health-authority-site-form-state.service';
import { RemoteUsersFormState } from '../remote-users-page/remote-users-form-state.class';

@Component({
  selector: 'app-remote-user-page',
  templateUrl: './remote-user-page.component.html',
  styleUrls: ['./remote-user-page.component.scss']
})
export class RemoteUserPageComponent extends AbstractEnrolmentPage implements OnInit {
  /**
   * @description
   * FormState of the parent form, which has reuse in child form
   * with regards to helper methods.
   */
  public formState: RemoteUsersFormState;
  /**
   * @description
   * Local form for adding and updating a single model that is
   * not linked with the form state until submission where it
   * gets mirrored.
   */
  public form: FormGroup;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  /**
   * @description
   * URL parameter indicating the ID of the remote user, or
   * `new` when the user does not already exist.
   */
  public remoteUserIndex: string;
  public remoteUser: RemoteUser;
  public licenses: LicenseConfig[];
  public formControlNames: AddressLine[];
  public SiteRoutes = SiteRoutes;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private configService: ConfigService,
    private healthAuthoritySiteFormStateService: HealthAuthoritySiteFormStateService,
    private healthAuthoritySiteService: HealthAuthoritySiteService,
    route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.licenses = this.configService.licenses;
    this.remoteUserIndex = route.snapshot.params.index;
  }

  /**
   * @description
   * Remote user certifications specific to the local form.
   */
  public get remoteUserCertifications(): FormArray {
    return this.form.get('remoteUserCertifications') as FormArray;
  }

  public get selectedCollegeCodes(): number[] {
    return this.remoteUserCertifications.value
      .map((certification: RemoteUserCertification) => +certification.collegeCode);
  }

  public addCertification(): void {
    const newRemoteUserCertification = this.formState.remoteUserCertificationFormGroup();
    this.remoteUserCertifications.push(newRemoteUserCertification);
  }

  /**
   * @description
   * Removes a certification from the list in response to an
   * emitted event from college certifications. Does not allow
   * the list of certifications to empty.
   */
  public removeCertification(index: number): void {
    this.remoteUserCertifications.removeAt(index);
  }

  public collegeFilterPredicate(): (collegeConfig: CollegeConfig) => boolean {
    return (collegeConfig: CollegeConfig) =>
      (collegeConfig.code === CollegeLicenceClassEnum.CPSBC || collegeConfig.code === CollegeLicenceClassEnum.BCCNM);
  }

  public licenceFilterPredicate(): (licenceConfig: LicenseConfig) => boolean {
    return (licenceConfig: LicenseConfig) => this.hasAllowedRemoteAccessLicences(licenceConfig);
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(['./']);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance(): void {
    // Be aware that this is the parent form state and should only
    // be used for it's API and on submission
    this.formState = this.healthAuthoritySiteFormStateService.remoteUserFormState;
  }

  protected patchForm(): void {
    const site = this.healthAuthoritySiteService.site;
    this.isCompleted = site?.completed;

    // Attempt to patch if needed on a refresh, otherwise do not forcibly
    // update the form state as it will drop unsaved updates
    this.healthAuthoritySiteFormStateService.setForm(site);

    // Extract an existing remoteUser from the parent form for updates, otherwise new
    const remoteUser = this.formState.getRemoteUsers()[+this.remoteUserIndex] ?? null;

    // Remote user at index does not exist likely due to a browser
    // refresh on this page, and the URL param should be update
    if (this.remoteUserIndex !== 'new' && !remoteUser) {
      this.routeUtils.routeRelativeTo(['new']);
    }

    // Create a local form group for creating or updating remote users
    this.form = this.formState.createEmptyRemoteUserFormAndPatch(remoteUser);

    // Must always have at least one certification
    if (!this.remoteUserCertifications.length) {
      this.addCertification();
    }
  }

  protected checkValidity(): boolean {
    // Pass the local form for validation and submission instead
    // of using the default form from the form state
    return super.checkValidity(this.form);
  }

  protected onSubmitFormIsValid(): void {
    this.removeIncompleteCertifications(true);
  }

  protected performSubmission(): NoContent {
    // Set the parent form for updating on submission, but otherwise use the
    // local form group for all changes prior to submission
    const parent = this.formState.form;
    const remoteUsersFormArray = parent.get('remoteUsers') as FormArray;

    if (this.remoteUserIndex !== 'new') {
      const remoteUserFormGroup = remoteUsersFormArray.at(+this.remoteUserIndex);
      const certificationFormArray = remoteUserFormGroup.get('remoteUserCertifications') as FormArray;

      // Changes in the amount of certificates requires adjusting the number of
      // certificates in the parent, which is not handled automatically
      if (this.remoteUserCertifications.length !== certificationFormArray.length) {
        certificationFormArray.clear();

        Object.keys(this.remoteUserCertifications.controls)
          .map((_) => this.formState.remoteUserCertificationFormGroup())
          .forEach((certification: FormGroup) => certificationFormArray.push(certification));
      }

      // Replace the updated remote user in the parent form for submission
      remoteUserFormGroup.reset(this.form.getRawValue());
    } else {
      // Store the new remote user in the parent form for submission
      remoteUsersFormArray.push(this.form);
    }

    parent.markAsPristine();

    return of(noop());
  }

  protected afterSubmitIsSuccessful(): void {
    this.form.markAsPristine();

    // Inform the remote users view not to patch the form, otherwise updates will be lost
    this.routeUtils.routeRelativeTo(['./'], { queryParams: { fromRemoteUser: true } });
  }

  /**
   * @description
   * Removes incomplete certifications from the list in preparation
   * for submission, and allows for an empty list of certifications.
   */
  private removeIncompleteCertifications(noEmptyCert: boolean = false): void {
    this.remoteUserCertifications.controls
      .forEach((control: FormGroup, index: number) => {
        // Remove if college code is "None" or the group is invalid
        if (!control.get('collegeCode').value || control.invalid) {
          this.removeCertification(index);
        }
      });

    // Always have a single certification available, and it prevents
    // the page from jumping too much when routing
    if (!noEmptyCert && !this.remoteUserCertifications.controls.length) {
      this.addCertification();
    }
  }

  private hasAllowedRemoteAccessLicences(licenceConfig: LicenseConfig): boolean {
    return (licenceConfig.licensedToProvideCare && licenceConfig.namedInImReg);
  }
}
