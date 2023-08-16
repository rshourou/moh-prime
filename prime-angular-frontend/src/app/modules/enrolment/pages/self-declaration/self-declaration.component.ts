import { Component, OnInit } from '@angular/core';
import { Validators, FormControl, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';
import { SelfDeclarationVersion } from '@shared/models/self-declaration-version.model';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { AuthService } from '@auth/shared/services/auth.service';
import moment from 'moment';

@Component({
  selector: 'app-self-declaration',
  templateUrl: './self-declaration.component.html',
  styleUrls: ['./self-declaration.component.scss']
})
export class SelfDeclarationComponent extends BaseEnrolmentProfilePage implements OnInit {
  public decisions: { code: boolean, name: string }[];
  public hasAttemptedFormSubmission: boolean;
  public showUnansweredQuestionsError: boolean;
  public isDeviceProvider: boolean;
  public SelfDeclarationTypeEnum = SelfDeclarationTypeEnum;
  public selfDeclarationQuestions = new Map<number, string>();
  public selfDeclarationVersions: SelfDeclarationVersion[];

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected enrolmentResource: EnrolmentResource,
    protected toastService: ToastService,
    protected logger: ConsoleLoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    protected authService: AuthService,
  ) {
    super(
      route,
      router,
      dialog,
      enrolmentService,
      enrolmentResource,
      enrolmentFormStateService,
      toastService,
      logger,
      utilService,
      formUtilsService,
      authService
    );

    this.decisions = [
      { code: false, name: 'No' },
      { code: true, name: 'Yes' }
    ];
    this.hasAttemptedFormSubmission = false;
    this.showUnansweredQuestionsError = false;
  }

  public get hasConviction(): FormControl {
    return this.form.get('hasConviction') as FormControl;
  }

  public get hasConvictionDetails(): FormControl {
    return this.form.get('hasConvictionDetails') as FormControl;
  }

  public get hasRegistrationSuspended(): FormControl {
    return this.form.get('hasRegistrationSuspended') as FormControl;
  }

  public get hasRegistrationSuspendedDetails(): FormControl {
    return this.form.get('hasRegistrationSuspendedDetails') as FormControl;
  }

  public get hasRegistrationSuspendedDeviceProvider(): FormControl {
    return this.form.get('hasRegistrationSuspendedDeviceProvider') as FormControl;
  }

  public get hasRegistrationSuspendedDeviceProviderDetails(): FormControl {
    return this.form.get('hasRegistrationSuspendedDeviceProviderDetails') as FormControl;
  }

  public get hasDisciplinaryAction(): FormControl {
    return this.form.get('hasDisciplinaryAction') as FormControl;
  }

  public get hasDisciplinaryActionDetails(): FormControl {
    return this.form.get('hasDisciplinaryActionDetails') as FormControl;
  }

  public get hasPharmaNetSuspended(): FormControl {
    return this.form.get('hasPharmaNetSuspended') as FormControl;
  }

  public get hasPharmaNetSuspendedDetails(): FormControl {
    return this.form.get('hasPharmaNetSuspendedDetails') as FormControl;
  }

  public onSubmit() {
    if (!this.isInitialEnrolment && this.form.valid) {
      this.enrolmentFormStateService.selfDeclarationCompletedDate = moment().format();
    }

    const hasBeenThroughTheWizard = true;
    this.hasAttemptedFormSubmission = true;
    super.onSubmit(hasBeenThroughTheWizard);
  }

  public onUpload(controlName: string, sdd: SelfDeclarationDocument) {
    this.addSelfDeclarationDocumentGuid(controlName, sdd.documentGuid);
  }

  public onRemove(controlName: string, documentGuid: string) {
    this.removeSelfDeclarationDocumentGuid(controlName, documentGuid);
  }

  public getSelfDeclarationDocuments(selfDeclarationType: SelfDeclarationTypeEnum): SelfDeclarationDocument[] {
    return this.enrolment?.selfDeclarationDocuments.filter((sdd: SelfDeclarationDocument) => sdd.selfDeclarationTypeCode === selfDeclarationType);
  }

  public downloadSelfDeclarationDocument({ documentId }: { documentId: number }): void {
    this.enrolmentResource.getDownloadTokenSelfDeclarationDocument(this.enrolment.id, documentId)
      .subscribe((token: string) => this.utilService.downloadToken(token));
  }

  public onBack() {
    const certifications = this.enrolmentFormStateService.regulatoryFormState.collegeCertifications;
    const careSettings = this.enrolmentFormStateService.careSettingsForm
      .get('careSettings').value as CareSetting[];
    const isDeviceProvider = this.enrolmentService.enrolment.careSettings.some((careSetting) =>
      careSetting.careSettingCode === CareSettingEnum.DEVICE_PROVIDER);
    const deviceProviderIdentifier = this.enrolmentFormStateService.regulatoryFormState.deviceProviderIdentifier.value;


    let backRoutePath = EnrolmentRoutes.OVERVIEW;
    if (!this.isProfileComplete) {
      backRoutePath = (this.enrolmentService.canRequestRemoteAccess(certifications, careSettings))
        ? EnrolmentRoutes.REMOTE_ACCESS
        : (!certifications.length && !isDeviceProvider)
          ? EnrolmentRoutes.OBO_SITES
          : EnrolmentRoutes.REGULATORY;
    }

    this.enrolmentFormStateService.reset();

    this.routeTo(backRoutePath);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm().subscribe(() => this.initForm());
  }

  protected createFormInstance() {
    this.form = this.enrolmentFormStateService.selfDeclarationForm;
  }

  protected initForm() {
    const careSettings = this.enrolmentFormStateService.careSettingsForm
      .get('careSettings').value as CareSetting[];
    this.isDeviceProvider = careSettings.some(cs => cs.careSettingCode === CareSettingEnum.DEVICE_PROVIDER);

    if (this.selfDeclarationQuestions.keys.length === 0) {
      // convert time zone to utc format
      this.busy = this.enrolmentResource.getSelfDeclarationVersion(moment().utc().format(), this.isDeviceProvider).subscribe((versions) => {
        this.selfDeclarationVersions = versions;
        versions.forEach(v => {
          this.selfDeclarationQuestions.set(v.selfDeclarationTypeCode, v.text);
        });
      });
    }

    this.hasConviction.valueChanges
      .subscribe((value: boolean) => {
        this.toggleSelfDeclarationValidators(value, this.hasConvictionDetails);
        this.showUnansweredQuestionsError = this.showUnansweredQuestions();
      });

    this.hasRegistrationSuspended.valueChanges
      .subscribe((value: boolean) => {
        this.toggleSelfDeclarationValidators(value, this.hasRegistrationSuspendedDetails);
        this.showUnansweredQuestionsError = this.showUnansweredQuestions();
      });

    if (this.isDeviceProvider) {
      this.hasRegistrationSuspendedDeviceProvider.valueChanges
        .subscribe((value: boolean) => {
          this.toggleSelfDeclarationValidators(value, this.hasRegistrationSuspendedDeviceProviderDetails);
          this.showUnansweredQuestionsError = this.showUnansweredQuestions();
        });
    } else {
      this.toggleSelfDeclarationValidators(false, this.hasRegistrationSuspendedDeviceProvider);
    }

    this.hasDisciplinaryAction.valueChanges
      .subscribe((value: boolean) => {
        this.toggleSelfDeclarationValidators(value, this.hasDisciplinaryActionDetails);
        this.showUnansweredQuestionsError = this.showUnansweredQuestions();
      });

    this.hasPharmaNetSuspended.valueChanges
      .subscribe((value: boolean) => {
        this.toggleSelfDeclarationValidators(value, this.hasPharmaNetSuspendedDetails);
        this.showUnansweredQuestionsError = this.showUnansweredQuestions();
      });
  }

  protected handleDeactivation(result: boolean): void {
    if (!result) {
      return;
    }

    const { requireRedoSelfDeclaration } = this.enrolmentService.enrolment;

    if (requireRedoSelfDeclaration) {
      this.form.reset();
    } else {
      // Replace previous values on deactivation so updates are discarded
      const { selfDeclarations, profileCompleted } = this.enrolmentService.enrolment;
      this.enrolmentFormStateService.patchSelfDeclarations({ selfDeclarations, profileCompleted });
    }
  }

  protected onSubmitFormIsInvalid() {
    this.showUnansweredQuestionsError = this.showUnansweredQuestions();
  }

  protected afterSubmitIsSuccessful(): void {
    this.enrolmentFormStateService.clearSelfDeclarationDocumentGuids();
  }

  private toggleSelfDeclarationValidators(value: boolean, control: FormControl) {
    if (!value) {
      this.formUtilsService.resetAndClearValidators(control);
    } else {
      this.formUtilsService.setValidators(control, [Validators.required]);
    }
  }

  private showUnansweredQuestions(): boolean {
    let shouldShowUnansweredQuestions = false;

    if (this.hasAttemptedFormSubmission) {
      const { careSettings } = this.enrolmentService.enrolment;
      if (careSettings.some(cs => cs.careSettingCode === CareSettingEnum.DEVICE_PROVIDER)) {
        shouldShowUnansweredQuestions = this.hasConviction.value === null
          || this.hasRegistrationSuspended.value === null
          || this.hasRegistrationSuspendedDeviceProvider.value === null
          || this.hasDisciplinaryAction.value === null
          || this.hasPharmaNetSuspended.value === null;
      } else {
        shouldShowUnansweredQuestions = this.hasConviction.value === null
          || this.hasRegistrationSuspended.value === null
          || this.hasDisciplinaryAction.value === null
          || this.hasPharmaNetSuspended.value === null;
      }
    }

    return shouldShowUnansweredQuestions;
  }

  private addSelfDeclarationDocumentGuid(controlName: string, documentGuid: string) {
    this.enrolmentFormStateService
      .addSelfDeclarationDocumentGuid(this.form.get(controlName) as FormArray, documentGuid);
  }

  private removeSelfDeclarationDocumentGuid(controlName: string, documentGuid: string) {
    this.enrolmentFormStateService
      .removeSelfDeclarationDocumentGuid(this.form.get(controlName) as FormArray, documentGuid);
  }
}
