import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

import { EMPTY, of } from 'rxjs';
import { exhaustMap, startWith, tap } from 'rxjs/operators';

import moment from 'moment';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { Config, CollegeConfig, LicenseConfig, PracticeConfig, CollegeLicenseGroupingConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ViewportService } from '@core/services/viewport.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';
import { PrescriberIdTypeEnum } from '@shared/enums/prescriber-id-type.enum';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-college-certification-form',
  templateUrl: './college-certification-form.component.html',
  styleUrls: ['./college-certification-form.component.scss']
})
export class CollegeCertificationFormComponent implements OnInit {
  @Input() public form: FormGroup;
  @Input() public index: number;
  @Input() public total: number;
  @Input() public selectedColleges: number[];
  @Input() public collegeFilterPredicate: (collegeConfig: CollegeConfig) => boolean;
  @Input() public licenceFilterPredicate: (licenceConfig: LicenseConfig) => boolean;
  @Input() public condensed: boolean;
  @Input() public defaultOption: boolean;
  @Output() public remove: EventEmitter<number>;
  public isPrescribing: boolean;
  public colleges: CollegeConfig[];
  public licenses: LicenseConfig[];
  /**
   * @description
   * Indicates the licenceCode is validated by PharmaNet.
   */
  public practices: PracticeConfig[];
  public filteredLicenses: Config<number>[];
  public filteredPractices: Config<number>[];
  public nurseGroups: CollegeLicenseGroupingConfig[];
  public hasPractices: boolean;
  /**
   * @description
   * Indicates the prescriber ID (PharmaNet) type associated with a
   * chosen licence code.
   *
   * NOTE: Used to show the practitioner ID form element
   * when Mandatory.
   */
  public prescriberIdType: PrescriberIdTypeEnum;
  public minRenewalDate: moment.Moment;
  public CollegeLicenceClassEnum = CollegeLicenceClassEnum;
  public PrescriberIdTypeEnum = PrescriberIdTypeEnum;

  constructor(
    private configService: ConfigService,
    private viewportService: ViewportService,
    private formUtilsService: FormUtilsService,
    private enrolmentService: EnrolmentService
  ) {
    this.remove = new EventEmitter<number>();
    this.colleges = this.configService.colleges;
    this.licenses = this.configService.licenses;
    this.practices = this.configService.practices;
    this.nurseGroups = this.configService.collegeLicenseGroupings;
    this.minRenewalDate = (this.enrolmentService.isProfileComplete) ? null : moment();
    this.condensed = false;
    this.defaultOption = true;
  }

  public get isMobile() {
    return this.viewportService.isMobile;
  }

  public get collegeCode(): FormControl {
    return this.form.get('collegeCode') as FormControl;
  }

  public get nurseCategory(): FormControl {
    return this.form.get('nurseCategory') as FormControl;
  }

  public get licenseNumber(): FormControl {
    return this.form.get('licenseNumber') as FormControl;
  }

  public get licenseCode(): FormControl {
    return this.form.get('licenseCode') as FormControl;
  }

  /**
   * @description
   * ID of a practitioner, but also known as prescriberId
   * when applied to nurses.
   */
  public get practitionerId(): FormControl {
    return this.form.get('practitionerId') as FormControl;
  }

  public get renewalDate(): FormControl {
    return this.form.get('renewalDate') as FormControl;
  }

  public get practiceCode(): FormControl {
    return this.form.get('practiceCode') as FormControl;
  }

  public get filteredColleges(): CollegeConfig[] {
    return this.colleges.filter((college: CollegeConfig) =>
      // Allow the currently chosen value to persist
      this.collegeCode.value === college.code || !this.selectedColleges?.includes(college.code)
    );
  }

  public allowedColleges(): CollegeConfig[] {
    return (this.collegeFilterPredicate)
      ? this.filteredColleges.filter(this.collegeFilterPredicate)
      : this.filteredColleges;
  }

  public allowedLicenses() {
    return (this.licenceFilterPredicate)
      ? this.filteredLicenses.filter(this.licenceFilterPredicate)
      : this.filteredLicenses;
  }

  public removeCertification() {
    this.remove.emit(this.index);
  }

  public shouldShowPractices(): boolean {
    // Only display Advanced Practices for certain nursing licences
    return CollegeCertification.hasPractice(this.collegeCode.value, this.licenseCode.value);
  }

  public showLicenceClass(): boolean {
    return this.filteredLicenses.some(l => l.name !== 'Not Displayed');
  }

  /**
   * @description
   * Handle changes to prescriber opt-in/out, but will only ever
   * occur when the prescriberIDType is optional.
   */
  public onPrescribing({ checked: isPrescribing }: MatSlideToggleChange): void {
    (isPrescribing)
      ? this.setPractitionerIdStateAndValidators(this.prescriberIdType, isPrescribing)
      : this.resetPractitionerIdStateAndValidators();
  }

  public onPractiionerIdChange(event: any): void {
    this.practitionerId.patchValue(event.target.value.toUpperCase());
  }

  // TODO decouple default and condensed modes in controller and template
  public ngOnInit() {
    if (this.condensed) {
      this.formUtilsService.setValidators(this.collegeCode, [Validators.required]);
    }

    this.setCollegeCertification(this.collegeCode.value);
    this.collegeCode.valueChanges
      .subscribe((collegeCode: number) => {
        this.resetCollegeCertification();
        this.setCollegeCertification(collegeCode);
        if (!this.condensed) {
          this.loadPractices(collegeCode);
        }
      });

    if (!this.condensed) {
      const initialLicenceCode = +this.licenseCode?.value ?? null;
      this.licenseCode.valueChanges
        // Allow for initialization of the licence code when
        // the code already exists
        .pipe(startWith(initialLicenceCode))
        .subscribe((licenseCode: number) => {
          if (licenseCode) {
            this.setPractitionerInformation(licenseCode);
          }
        });

      const initialNursingCategory = +this.nurseCategory.value ?? null;
      this.nurseCategory.valueChanges
        .pipe(
          startWith(initialNursingCategory),
          tap((collegeLicenseGroupingCode: number | null) => (collegeLicenseGroupingCode) ? this.clearNursingCategoryValidators() : null),
          exhaustMap((collegeLicenseGroupingCode: number | null) =>
            (collegeLicenseGroupingCode)
              ? of(collegeLicenseGroupingCode)
              : EMPTY
          )
        )
        .subscribe((collegeLicenseGroupingCode: number) => {
          this.setNursingCategoryValidators();
          this.loadLicensesByNursingCategory(collegeLicenseGroupingCode);
        });
    } else {
      const prescriberIdType = this.enrolmentService.getPrescriberIdType(this.licenseCode.value);
      const isPrescribing = prescriberIdType === PrescriberIdTypeEnum.Optional && !!this.practitionerId.value;
      this.setPractitionerIdStateAndValidators(prescriberIdType, isPrescribing);
    }
  }

  private setCollegeCertification(collegeCode: number): void {
    if (!collegeCode) {
      this.removeValidations();
      return;
    }

    if (collegeCode === CollegeLicenceClassEnum.BCCNM && !this.condensed) {
      this.formUtilsService.setValidators(this.nurseCategory, [Validators.required]);
      return;
    }

    // In case previous selection was BCCNM, clear validators
    if (!this.condensed) {
      this.formUtilsService.setValidators(this.nurseCategory, []);
      this.clearNursingCategoryValidators();
    }

    // Initialize the validations when the college code is not
    // "None" to allow for submission when no college is selected
    this.setCollegeCertificationValidators();

    this.loadLicenses(collegeCode);
    if (this.filteredLicenses?.length === 1) {
      // Trigger licenceCode value changes to manage setting up
      // remaining parts of the form
      this.licenseCode.patchValue(this.filteredLicenses[0].code);
    }

    if (!this.condensed) {
      this.loadPractices(collegeCode);
    }
  }

  private setCollegeCertificationValidators() {
    this.formUtilsService.setValidators(this.licenseCode, [Validators.required]);
    const licenseNumberValidators = [Validators.required];
    if (this.collegeCode.value === CollegeLicenceClassEnum.CPSBC
      || this.collegeCode.value === CollegeLicenceClassEnum.CPBC
      || this.collegeCode.value === CollegeLicenceClassEnum.CDSBC
      || this.collegeCode.value === CollegeLicenceClassEnum.OptometryBC
    ) {
      licenseNumberValidators.push(FormControlValidators.numeric, FormControlValidators.requiredLength(5));
    } else {
      licenseNumberValidators.push(FormControlValidators.alphanumeric);
    }
    this.formUtilsService.setValidators(this.licenseNumber, licenseNumberValidators);

    if (!this.condensed) {
      this.formUtilsService.setValidators(this.renewalDate, [Validators.required, FormControlValidators.mustBeFutureDate]);
    }
  }

  private resetCollegeCertification() {
    this.licenseCode.reset(null);
    this.licenseNumber.reset(null);

    if (!this.condensed) {
      this.nurseCategory.reset(null);
      this.renewalDate.reset(null);
      this.practiceCode.reset(null);
      this.resetPractitionerIdStateAndValidators();
    }
  }

  private setNursingCategoryValidators(): void {
    this.formUtilsService.setValidators(this.licenseCode, [Validators.required]);
    this.formUtilsService.setValidators(this.licenseNumber, [Validators.required, FormControlValidators.alphanumeric]);

    if (!this.condensed) {
      this.formUtilsService.setValidators(this.renewalDate, [Validators.required, FormControlValidators.mustBeFutureDate]);
    }
  }

  private clearNursingCategoryValidators(): void {
    this.formUtilsService.setValidators(this.licenseCode, []);
    this.formUtilsService.setValidators(this.licenseNumber, []);

    if (!this.condensed) {
      this.formUtilsService.setValidators(this.renewalDate, []);
    }
  }

  private setPractitionerInformation(licenseCode: number) {
    const prescriberIdType = this.enrolmentService.getPrescriberIdType(licenseCode);
    let isPrescribing = this.isPrescribing;

    switch (prescriberIdType) {
      case PrescriberIdTypeEnum.NA:
        // Ensures validators are cleared and value reset to prevent
        // values persisting through to submission
        this.resetPractitionerIdStateAndValidators();
        break;
      case PrescriberIdTypeEnum.Optional:
        // Maintain validators only if the value exists
        if (!this.practitionerId.value) {
          this.resetPractitionerIdStateAndValidators();
        }
        // Ensures that changes in licence code from mandatory
        // to optional will show the input
        isPrescribing = this.practitionerId.value;
        break;
      case PrescriberIdTypeEnum.Mandatory:
        break; // NOOP
    }

    this.setPractitionerIdStateAndValidators(prescriberIdType, isPrescribing);
  }

  private setPractitionerIdStateAndValidators(prescriberIdType: PrescriberIdTypeEnum, isPrescribing: boolean) {
    // Always set prescriber related values, but don't
    // update the validations unless not condensed
    this.prescriberIdType = prescriberIdType;
    this.isPrescribing = isPrescribing;

    if (
      !this.condensed &&
      (prescriberIdType === PrescriberIdTypeEnum.Mandatory || (isPrescribing && prescriberIdType !== PrescriberIdTypeEnum.NA))
    ) {
      this.formUtilsService.setValidators(this.practitionerId, [
        Validators.required,
        FormControlValidators.alphanumeric,
        FormControlValidators.requiredLength(5)
      ]);
    }
  }

  private resetPractitionerIdStateAndValidators() {
    this.isPrescribing = false;
    this.formUtilsService.resetAndClearValidators(this.practitionerId);
  }

  private removeValidations() {
    this.formUtilsService.setValidators(this.licenseCode, []);
    this.formUtilsService.setValidators(this.licenseNumber, []);

    if (!this.condensed) {
      this.formUtilsService.setValidators(this.nurseCategory, []);
      this.formUtilsService.setValidators(this.renewalDate, []);
      this.formUtilsService.setValidators(this.practitionerId, []);
    }
  }

  private loadLicenses(collegeCode: number) {
    if (collegeCode !== CollegeLicenceClassEnum.BCCNM || this.condensed) {
      this.filteredLicenses = this.filterLicenses(collegeCode);
      this.licenseCode.patchValue(this.licenseCode.value || null, { emitEvent: false });
    }
  }

  private loadLicensesByNursingCategory(nursingCategory: number) {
    const collegeCode = this.collegeCode.value;
    if (collegeCode === CollegeLicenceClassEnum.BCCNM) {
      this.loadPractices(collegeCode);
      this.filteredLicenses = this.filterLicensesByGrouping(nursingCategory);
      this.licenseCode.patchValue(this.licenseCode.value || null, { emitEvent: false });
    }
  }

  private loadPractices(collegeCode: number) {
    this.filteredPractices = this.filterPractices(collegeCode);
    this.practiceCode.patchValue(this.practiceCode.value || null);
    this.hasPractices = !!this.filteredPractices.length;
  }

  private filterLicenses(collegeCode: number): LicenseConfig[] {
    return this.licenses.filter(l => l.collegeLicenses.map(cl => cl.collegeCode).includes(collegeCode));
  }

  private filterLicensesByGrouping(collegeLicenseGroupingCode: number): LicenseConfig[] {
    return this.licenses.filter(l => l.collegeLicenses.map(cl => cl.collegeLicenseGroupingCode).includes(collegeLicenseGroupingCode));
  }

  private filterPractices(collegeCode: number): PracticeConfig[] {
    return this.practices.filter(p => p.collegePractices.map(cl => cl.collegeCode).includes(collegeCode));
  }
}
