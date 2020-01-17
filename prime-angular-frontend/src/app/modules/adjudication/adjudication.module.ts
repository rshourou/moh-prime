import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';
import { AdjudicationRoutingModule } from './adjudication-routing.module';

import { EnrolmentsComponent } from './pages/enrolments/enrolments.component';
import { EnrolmentComponent } from './pages/enrolment/enrolment.component';
import { AdjudicatorNotesComponent } from './pages/adjudicator-notes/adjudicator-notes.component';
import { LimitsConditionsClausesComponent } from './pages/limits-conditions-clauses/limits-conditions-clauses.component';
import { CertificateLimitsConditionsComponent } from './pages/certificate-limits-conditions/certificate-limits-conditions.component';

@NgModule({
  declarations: [
    EnrolmentsComponent,
    EnrolmentComponent,
    AdjudicatorNotesComponent,
    LimitsConditionsClausesComponent,
    CertificateLimitsConditionsComponent
  ],
  imports: [
    SharedModule,
    AdjudicationRoutingModule
  ]
})
export class AdjudicationModule { }
