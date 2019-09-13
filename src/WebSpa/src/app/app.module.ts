import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ActiveMissilesComponent } from './operation/activeMissiles.component';
import { OperationRepository } from './repositories/operation/operation.repository';
import { RestService } from './service/rest.service';
import { CommissionMissileComponent } from './operation/commissionMissile.component';
import { LaunchMissileComponent } from './operation/launchMissile.component';

@NgModule({
  declarations: [
    AppComponent,
    ActiveMissilesComponent,
    CommissionMissileComponent,
    LaunchMissileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [ OperationRepository, RestService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
