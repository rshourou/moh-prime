import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigResolver } from '@config/config-resolver';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { EnrolmentRoutes } from './enrolent.routes';
import { EnrolleeGuard } from './shared/guards/enrollee.guard';
import { EnrolmentGuard } from './shared/guards/enrolment.guard';

import { ProfileComponent } from './pages/profile/profile.component';
import { RegulatoryComponent } from './pages/regulatory/regulatory.component';
import { SelfDeclarationComponent } from './pages/self-declaration/self-declaration.component';
import { PharmanetAccessComponent } from './pages/pharmanet-access/pharmanet-access.component';
import { ReviewComponent } from './pages/review/review.component';
import { ConfirmationComponent } from './pages/confirmation/confirmation.component';
import { AccessAgreementComponent } from './pages/access-agreement/access-agreement.component';
import { SummaryComponent } from './pages/summary/summary.component';
import { DeviceProviderComponent } from './pages/device-provider/device-provider.component';
import { JobComponent } from './pages/job/job.component';


const routes: Routes = [
  {
    path: 'enrolment',
    component: DashboardComponent,
    canActivateChild: [
      AuthenticationGuard,
      EnrolleeGuard,
      EnrolmentGuard
    ],
    resolve: [ConfigResolver],
    children: [
      {
        path: EnrolmentRoutes.PROFILE,
        component: ProfileComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: EnrolmentRoutes.REGULATORY,
        component: RegulatoryComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: EnrolmentRoutes.DEVICE_PROVIDER,
        component: DeviceProviderComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: EnrolmentRoutes.JOB,
        component: JobComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: EnrolmentRoutes.SELF_DECLARATION,
        component: SelfDeclarationComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: EnrolmentRoutes.PHARMANET_ACCESS,
        component: PharmanetAccessComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: EnrolmentRoutes.REVIEW,
        component: ReviewComponent
      },
      {
        path: EnrolmentRoutes.CONFIRMATION,
        component: ConfirmationComponent
      },
      {
        path: EnrolmentRoutes.ACCESS_AGREEMENT,
        component: AccessAgreementComponent
      },
      {
        path: EnrolmentRoutes.SUMMARY,
        component: SummaryComponent
      },
      {
        path: '', // Equivalent to `/` and alias for `profile`
        redirectTo: EnrolmentRoutes.PROFILE,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EnrolmentRoutingModule { }
