import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog, MatCheckboxChange } from '@angular/material';

import { map, exhaustMap } from 'rxjs/operators';
import { EMPTY, Subscription } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss']
})
export class ReviewComponent implements OnInit {
  public busy: Subscription;
  public enrolment: Enrolment;
  public EnrolmentRoutes = EnrolmentRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private enrolmentService: EnrolmentService,
    private enrolmentResource: EnrolmentResource,
    private enrolmentStateService: EnrolmentStateService,
    private toastService: ToastService,
    private dialog: MatDialog,
    private logger: LoggerService
  ) { }

  public onSubmit() {
    if (this.enrolmentStateService.isEnrolmentValid()) {
      const enrolment = this.enrolmentStateService.enrolment;
      const data: DialogOptions = {
        title: 'Submit Enrolment',
        message: 'When your enrolment has submitted for adjudication it can no longer be updated. Are you ready to submit your enrolment?',
        actionText: 'Submit Enrolment'
      };
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? this.enrolmentResource.updateEnrolmentStatus(enrolment.id, EnrolmentStatus.SUBMITTED)
              : EMPTY
          )
        )
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Enrolment has been submitted');
            this.router.navigate([EnrolmentRoutes.CONFIRMATION], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('Enrolment could not be submitted');
            this.logger.error('[Enrolment] Review::onSubmit error has occurred: ', error);
          });
    } else {
      console.log('PROFILE', this.enrolmentStateService.isProfileInfoValid());
      console.log('REGULATORY', this.enrolmentStateService.isRegulatoryValid());
      console.log('JOBS', this.enrolmentStateService.isJobsValid());
      console.log('DECLARATION', this.enrolmentStateService.isSelfDeclarationValid());
      console.log('ACCESS', this.enrolmentStateService.isOrganizationValid());
    }
  }

  public showYesNo(declared: boolean) {
    return (declared === null)
      ? 'N/A' : (declared)
        ? 'Yes' : 'No';
  }

  public onRoute(routePath: EnrolmentRoutes) {
    this.router.navigate([routePath], { relativeTo: this.route.parent });
  }

  public ngOnInit() {
    const enrolment = this.enrolmentService.enrolment;
    this.enrolment = enrolment;

    this.enrolmentStateService.enrolment = enrolment;
  }
}
